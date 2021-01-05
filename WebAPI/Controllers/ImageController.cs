using Microsoft.AspNetCore.Mvc;
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

        // POST: api/Image
        [HttpPost]
        public void Post([FromBody] string value)
        {
            
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
