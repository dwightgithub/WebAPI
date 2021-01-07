using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    public class StreetViewRepository : IStreetViewRepository
    {
        static Random rand = new Random();
        private StreetViewDbContext dbContext { get; }

        public StreetViewRepository(StreetViewDbContext dbContext)
        {
            //在构造函数中注入DbContext
            this.dbContext = dbContext;
        }

        /// <summary>
        /// 根据用户名查询用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<UserEntity> QueryUserWithTaskByName(string name)
        {
            var user = await dbContext.Users.Include(o => o.Tasks)
                .SingleOrDefaultAsync(user => user.Name == name);
            Console.WriteLine("能查到有" + user?.Tasks?.Count);
            return user;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<int> UpdateUser(UserEntity user)
        {
            dbContext.Update(user);
            return await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 用户完成任务，增长经验值
        /// </summary>
        /// <returns></returns>
        public async Task<int> UserAddEXP(int userID,int exp)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.IdUserInfo == userID);
            user.Experience += exp;
            dbContext.Update(user);
            return await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 新建用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<int> CreateUser(UserEntity user)
        {
            dbContext.Add(user);
            return await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 创建任务的接口
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public Task<int> CreateTask(TaskEntity task)
        {
            //满足该task区域的ImageInfo
            var lstImageInfo = dbContext.ImageInfos.Where<ImageInfoEntity>
                (p => p.District == task.Area || p.City == task.Area).ToList<ImageInfoEntity>();

            if (task.TaskType == 0)
            {
                task.Rates = new List<RateEntity>();
                //为task创建对应数量的Rate
                for (int i = 0; i < task.TaskCount; i++)
                {
                    int imageNO = rand.Next(lstImageInfo.Count);
                    var rateEntity = new RateEntity()
                    {
                        PicID = lstImageInfo[imageNO].ImageInfoID,
                    };
                    task.Rates.Add(rateEntity);
                }
            }
            else if (task.TaskType == 1)
            {
                task.Pairwises = new List<PairwiseEntity>();
                //为task创建对应数量的Pairwise
                for (int i = 0; i < task.TaskCount; i++)
                {
                    int picANO = rand.Next(lstImageInfo.Count);
                    int picBNO = rand.Next(lstImageInfo.Count);
                    var pairwiseEntity = new PairwiseEntity()
                    {
                        PicAID = lstImageInfo[picANO].ImageInfoID,
                        PicBID = lstImageInfo[picBNO].ImageInfoID,
                    };
                    task.Pairwises.Add(pairwiseEntity);
                }
            }
            else
            {
                throw new Exception("任务类型不对");
            }

            //创建Task
            dbContext.Add(task);
            return dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 完成任务
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<int> CompleteTask(TaskEntity task)
        {
            task.CompleteTime = DateTime.Now;
            task.TaskComplete = true;
            task.Pairwises?.ForEach(p => { p.ImageAInfo = null;p.ImageBInfo = null; });
            task.Rates?.ForEach(p => p.ImageInfo = null);
            dbContext.Update(task);
            return await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 根据任务号查询任务信息
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public async Task<TaskEntity> QueryTaskWithInfoByID(int taskID)
        {
            //1、按照taskID查找出Task
            //2、加载task对应的List<Rate>
            //3、加载每个Rate对应的ImageInfo
            TaskEntity task;
            var taskType = dbContext.Tasks.SingleOrDefault(task => task.IDtask == taskID)?.TaskType;
            if (taskType == 0)//Rate
            {
                task = await dbContext.Tasks.Include(o => o.Rates).ThenInclude(o => o.ImageInfo)
                .SingleOrDefaultAsync(task => task.IDtask == taskID);

                for (int i = 0; i < task.Rates.Count; i++)
                {
                    task.Rates[i].IDinList = i;
                }
            }
            else
            {
                task = await dbContext.Tasks
                    .Include(o => o.Pairwises).ThenInclude(o=>o.ImageAInfo)
                    .Include(o => o.Pairwises).ThenInclude(o => o.ImageBInfo)
                .SingleOrDefaultAsync(task => task.IDtask == taskID);
                for (int i = 0; i < task.Pairwises.Count; i++)
                {
                    task.Pairwises[i].IDinList = i;
                }
            }
            

            return task;
        }
    }
}
