using EventReview.Shared.Models;
using EventReview.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace EventReview.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IRepository<Event> _db;

        public EventController(IRepository<Event> db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<Event>> Get()
        {
            return await _db.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Event> Get(string id)
        {
            return await _db.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task Post([FromBody] Event entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            await _db.AddAsync(entity);
        }

        [HttpPut("{id}")]
        public async Task Put(string id, [FromBody] Event entity)
        {
            entity.Id = id;
            await _db.UpdateAsync(id, entity);
        }

        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _db.DeleteAsync(id);
        }
    }
}
