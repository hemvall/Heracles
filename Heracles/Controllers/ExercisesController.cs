using heracles_api.Context;
using Heracles.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exercices.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly HeraclesContext _heraclesContext;

        public ExercisesController(HeraclesContext heraclesContext)
        {
            _heraclesContext = heraclesContext;
        }
        
        [HttpGet("{id:int}", Name = "GetExercises")]
        public ActionResult<Exercise> Get(int id)
        {
            var categoria = _heraclesContext.Exercises?.AsNoTracking().FirstOrDefault(x => x.Id == id);
            return categoria is null ? NotFound() : categoria;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Exercise>> Get()
        {
            var exs = _heraclesContext.Exercises?.AsNoTracking().ToList();
            return exs is null ? NotFound() : exs;
        }

        [HttpPost]
        public ActionResult<Exercise> Post(Exercise ex)
        {
            if(ex is null) return BadRequest();

            _heraclesContext.Exercises?.Add(ex);
            _heraclesContext.SaveChanges();

            return new CreatedAtRouteResult("GetExercise", new { id = ex.Id, ex});
        }

        [HttpPut("{id:int}")]
        public ActionResult<Exercise> Put(int id, Exercise ex)
        {
            if(ex is null || ex.Id != id) return BadRequest();

            _heraclesContext.Entry<Exercise>(ex).State = EntityState.Modified;
            _heraclesContext.SaveChanges();

            return Ok(ex);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var categoria = _heraclesContext.Exercises?.FirstOrDefault(c => c.Id == id);

            if(categoria is null) return NotFound();

            _heraclesContext.Exercises?.Remove(categoria);
            _heraclesContext.SaveChanges();

            return Ok(categoria);
        }
    }
}