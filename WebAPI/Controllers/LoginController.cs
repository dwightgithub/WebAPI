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
        //查询用户信息
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

                var taskID = 0;
                if (userInfoMatch.Tasks.Count > 0)
                {
                    taskID = userInfoMatch.Tasks[0].IDtask;
                }

                return new { success = true, defaultTaskID = taskID, reason = msgToRet, userInfo = userInfoMatch };
            }
            catch (Exception e)
            {
                return new { success = false, reason = e.Message, userInfo = "" };
            }
        }

        ///

        [HttpPost]
        [ActionName("update")]

        public object Update([FromBody] UserEntity userInfo)
        {
            //修改用户相关信息
            try
            {
                var i = _repository.UpdateUser(userInfo).Result;
                return new { success = i > 0, reason = "i=" + i };
            }
            catch (Exception e)
            {
                return new { success = false, reason = e.Message };
            }
        }


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
