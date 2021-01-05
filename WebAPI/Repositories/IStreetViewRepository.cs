using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    interface IStreetViewRepository
    {
        /// <summary>
        /// 按照名称查询用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<UserEntity> QueryUserWithTaskByName(string name);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<int> UpdateUser(UserEntity user);

        /// <summary>
        /// 用户创建一个任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public Task<int> CreateTask(TaskEntity task);
    }
}
