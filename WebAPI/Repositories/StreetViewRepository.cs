﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<UserEntity> QueryUserWithTaskByName(string name)
        {
            var user = await dbContext.Users.Include(o => o.Tasks)
                .SingleOrDefaultAsync(user => user.Name == name);
            Console.WriteLine("能查到有" + user?.Tasks?.Count);
            return user;
        }

        public async Task<int> UpdateUser(UserEntity user)
        {
            dbContext.Update(user);
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
                (p => p.District == task.Area||p.City==task.Area).ToList<ImageInfoEntity>();

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

            //创建Task
            dbContext.Add(task);
            return dbContext.SaveChangesAsync();
        }

        public async Task<TaskEntity> QueryTaskWithInfoByID(int taskID)
        {
            //1、按照taskID查找出Task
            //2、加载task对应的List<Rate>
            //3、加载每个Rate对应的ImageInfo
            var task =await dbContext.Tasks.Include(o => o.Rates).ThenInclude(o=>o.ImageInfo)
                .SingleOrDefaultAsync(task => task.IDtask == taskID);

            //4、返回Task
            for (int i = 0; i < task.Rates.Count; i++)
            {
                task.Rates[i].IDinList = i;
            }
            
            return task; 
        }
    }
}