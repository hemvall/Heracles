using heracles_api.Context;
using Heracles.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Performances.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PerformancesController : ControllerBase
    {
        private readonly HeraclesContext _heraclesContext;

        public PerformancesController(HeraclesContext heraclesContext)
        {
            _heraclesContext = heraclesContext;
        }
        
        [HttpGet("{id:int}", Name = "GetPerformances")]
        public ActionResult<Performance> Get(int id)
        {
            var perf = _heraclesContext.Performances?.AsNoTracking().FirstOrDefault(x => x.Id == id);
            return perf is null ? NotFound() : perf;
        } 
        

        [HttpGet("/user/{userId:int}/Performances", Name = "GetUserPerformances")]
        public ActionResult<IEnumerable<Performance>> GetUserPerformances(int userId)
        {
            var perf = _heraclesContext.Performances?.Where(x => x.UserId == userId).AsNoTracking().ToList();
            return perf is null ? NotFound() : perf;
        }

        [HttpGet("/user/{userId:int}/TotalScore", Name = "GetUserTotalScore")]
        public ActionResult<int> GetUserTotalScore(int userId)
        {
            var totalScore = _heraclesContext.Performances
                .Where(x => x.UserId == userId)
                .Sum(x => x.Score);

            return totalScore;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Performance>> Get()
        {
            var perf = _heraclesContext.Performances?.AsNoTracking().ToList();
            return perf is null ? NotFound() : perf;
        }

        [HttpPost]
        public ActionResult<Performance> Post(Performance perf)
        {
            if(perf is null) return BadRequest();

            _heraclesContext.Performances?.Add(perf);
            _heraclesContext.SaveChanges();

            return new CreatedAtRouteResult("GePerformance", new { id = perf.Id, perf});
        }

        [HttpPut("{id:int}")]
        public ActionResult<Performance> Put(int id, Performance perf)
        {
            if(perf is null || perf.Id != id) return BadRequest();

            _heraclesContext.Entry<Performance>(perf).State = EntityState.Modified;
            _heraclesContext.SaveChanges();

            return Ok(perf);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var perf = _heraclesContext.Performances?.FirstOrDefault(c => c.Id == id);

            if(perf is null) return NotFound();

            _heraclesContext.Performances?.Remove(perf);
            _heraclesContext.SaveChanges();

            return Ok(perf);
        }
    }
}