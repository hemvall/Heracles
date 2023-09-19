using heracles_api.Context;
using Heracles.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
/*using BCrypt.Net;
*/
namespace Goals.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private readonly HeraclesContext _heraclesContext;

        public GoalsController(HeraclesContext heraclesContext)
        {
            _heraclesContext = heraclesContext;
        }
        
        [HttpGet("{id:int}", Name = "GetGoals")]
        public ActionResult<Goal> Get(int id)
        {
            var categoria = _heraclesContext.Goals?.AsNoTracking().FirstOrDefault(x => x.Id == id);
            return categoria is null ? NotFound() : categoria;
        }

        [HttpGet("/user/{userId:int}/Goals", Name = "GetUserGoals")]
        public ActionResult<IEnumerable<Goal>> GetUserGoals(int userId)
        {
            var exs = _heraclesContext.Goals?.Where(x => x.UserId == userId).AsNoTracking().ToList();
            return exs is null ? NotFound() : exs;
        }
        

        [HttpGet("/user/{userId:int}/goals/top3", Name = "Get3Goals")]
        public ActionResult<IEnumerable<Goal>> Get3Goals(int userId)
        {
            var exs = _heraclesContext.Goals?.Where(x => x.UserId == userId).OrderBy(x=> x.Deadline).Take(3).AsNoTracking().ToList();
            return exs is null ? NotFound() : exs;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Goal>> Get()
        {
            var exs = _heraclesContext.Goals?.AsNoTracking().ToList();
            return exs is null ? NotFound() : exs;
        }

        [HttpPost]
        public ActionResult<Goal> Post(Goal ex)
        {
            if(ex is null) return BadRequest();
            _heraclesContext.Goals?.Add(ex);
            _heraclesContext.SaveChanges();

            return new CreatedAtRouteResult("GetGoal", new { id = ex.Id, ex});
        }

        [HttpPut("{id:int}")]
        public ActionResult<Goal> Put(int id, Goal ex)
        {
            if(ex is null || ex.Id != id) return BadRequest();

            _heraclesContext.Entry<Goal>(ex).State = EntityState.Modified;
            _heraclesContext.SaveChanges();

            return Ok(ex);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var categoria = _heraclesContext.Goals?.FirstOrDefault(c => c.Id == id);

            if(categoria is null) return NotFound();

            _heraclesContext.Goals?.Remove(categoria);
            _heraclesContext.SaveChanges();

            return Ok(categoria);
        }
    }
}