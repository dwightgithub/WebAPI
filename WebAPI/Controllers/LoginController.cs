using Microsoft.AspNetCore.Mvc;
using System;
using WebAPI.Models;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public StreetViewRepository _repository { get; }

        public LoginController(StreetViewRepository repository)
        {
            this._repository = repository;
        }

        // POST: api/Login
        //登录，登录成功则反馈用户信息，包括默认的任务
        [HttpPost]
        [ActionName("login")]
        public object Login([FromBody] UserLoginInfo userLoginInfo)
        {
            var msgToRet = "登录成功";
            try
            {
                if (userLoginInfo == null)
                {
                    msgToRet = "输入信息错误";
                    return new { success = false, reason = msgToRet };
                }

                var userInfoMatch = _repository.QueryUserWithTaskByName(userLoginInfo.userName).Result;

                if (userInfoMatch == null)
                {
                    msgToRet = "该用户不存在";
                    return new { success = false, reason = msgToRet };
                }
                if (userInfoMatch.Password != userLoginInfo.password)
                {
                    msgToRet = "密码错误";
                    return new { success = false, reason = msgToRet };
                }

                //更新用户最新登录时间
                userInfoMatch.LastLoginDate = DateTime.Now;
                var i = _repository.UpdateUser(userInfoMatch).Result;

                var taskID = -1;
                var defaultTask = userInfoMatch.Tasks?.Find(p => p.TaskComplete == false);
                if (defaultTask != null)
                {
                    taskID = defaultTask.IDtask;
                }

                return new { success = true, defaultTaskID = taskID, reason = msgToRet, userInfo = userInfoMatch };
            }
            catch (Exception e)
            {
                return new { success = false, reason = e.Message, userInfo = "" };
            }
        }

        // POST: api/Update
        //修改用户信息
        [HttpPost]
        [ActionName("update")]
        public object Update([FromBody] UserEntity userInfo)
        {
            //修改用户相关信息
            try
            {
                var i = _repository.UpdateUser(userInfo).Result;
                return new { success = i > 0, reason = "成功对象数量：" + i };
            }
            catch (Exception e)
            {
                return new { success = false, reason = e.Message };
            }
        }

        // POST: api/Register
        [HttpPost]
        [ActionName("register")]
        public object Register([FromBody] UserLoginInfo userInfo)
        {
            //新增用户
            try
            {
                if (_repository.QueryUserWithTaskByName(userInfo.userName).Result != null)
                    return new { success = false, reason = "该用户已经存在" };
                var user = new UserEntity
                {
                    Name = userInfo.userName,
                    Password = userInfo.password,
                    CreateDate = DateTime.Now,
                    FaceID =1,                    
                };
                var i = _repository.CreateUser(user).Result;
                return new { success = (i == 1), reason = "成功对象数量：" + i };
            }
            catch (Exception e)
            {
                return new { success = false, reason = e.Message };
            }

        }

        // POST: api/CreateTask
        [HttpPost]
        [ActionName("createTask")]
        public object CreateTask([FromBody] TaskEntity task)
        {
            try
            {
                var i = _repository.CreateTask(task).Result;
                return new { success = i > 0, taskID = task.IDtask, reason = "i=" + i };

            }
            catch (Exception e)
            {
                return new { success = false, reason = e.Message };
            }
        }

    }
}
