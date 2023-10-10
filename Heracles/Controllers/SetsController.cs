using heracles_api.Context;
using Heracles.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Sets.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SetsController : ControllerBase
    {
        private readonly HeraclesContext _heraclesContext;

        public SetsController(HeraclesContext heraclesContext)
        {
            _heraclesContext = heraclesContext;
        }
        
        [HttpGet("{id:int}", Name = "GetSets")]
        public ActionResult<Set> Get(int id)
        {
            var perf = _heraclesContext.Sets?.AsNoTracking().FirstOrDefault(x => x.Id == id);
            return perf is null ? NotFound() : perf;
        } 
        

        [HttpGet("/user/{userId:int}/Sets", Name = "GetUserSets")]
        public ActionResult<IEnumerable<Set>> GetUserSets(int userId)
        {
            var perf = _heraclesContext.Sets?.Where(x => x.UserId == userId).AsNoTracking().ToList();
            return perf is null ? NotFound() : perf;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Performance>> Get()
        {
            var perf = _heraclesContext.Performances?.AsNoTracking().ToList();
            return perf is null ? NotFound() : perf;
        }

        [HttpPost]
        public ActionResult<Set> Post(Set set)
        {
            if(set is null) return BadRequest();

            _heraclesContext.Sets?.Add(set);
            _heraclesContext.SaveChanges();

            return new CreatedAtRouteResult("GetPerformance", new { id = set.Id, set});
        }

        [HttpPut("{id:int}")]
        public ActionResult<Set> Put(int id, Set set)
        {
            if(set is null || set.Id != id) return BadRequest();

            _heraclesContext.Entry<Set>(set).State = EntityState.Modified;
            _heraclesContext.SaveChanges();

            return Ok(set);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var set = _heraclesContext.Sets?.FirstOrDefault(c => c.Id == id);

            if(set is null) return NotFound();

            _heraclesContext.Sets?.Remove(set);
            _heraclesContext.SaveChanges();

            return Ok(set);
        }
    }
}