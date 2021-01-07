using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebAPI.Models;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private StreetViewRepository _repository;
        public ImageController(StreetViewRepository streetViewRepository)
        {
            _repository = streetViewRepository;
        }

        // GET: api/Image/5
        //根据任务ID查询任务
        [HttpGet("{num}", Name = "Get")]
        public TaskEntity Get(int num)
        {
            var task = _repository.QueryTaskWithInfoByID(num).Result;

            return task;
        }

        // POST: api/image
        //提交任务
        [HttpPost]
        public object CommitTask([FromBody] TaskEntity task)
        {
            try
            {
                var i = _repository.CompleteTask(task).Result;
                var j = _repository.UserAddEXP(task.UserID, task.TaskCount).Result;
                return new { success = i > 0 && j > 0, reason = "成功对象数量：" + i };
            }
            catch (Exception e)
            {
                return new { success = false, reason = e.Message };
            }
        }

        // PUT: api/Image/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
