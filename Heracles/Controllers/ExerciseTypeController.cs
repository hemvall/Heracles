using heracles_api.Context;
using Heracles.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exercices.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExerciseTypeController : ControllerBase
    {
        private readonly HeraclesContext _heraclesContext;

        public ExerciseTypeController(HeraclesContext heraclesContext)
        {
            _heraclesContext = heraclesContext;
        }
        
        [HttpGet("{id:int}", Name = "GetExerciseTypes")]
        public ActionResult<ExerciseType> Get(int id)
        {
            var categoria = _heraclesContext.ExerciseType?.AsNoTracking().FirstOrDefault(x => x.Id == id);
            return categoria is null ? NotFound() : categoria;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ExerciseType>> Get()
        {
            var exs = _heraclesContext.ExerciseType?.AsNoTracking().ToList();
            return exs is null ? NotFound() : exs;
        }

        [HttpPost]
        public ActionResult<ExerciseType> Post(ExerciseType ex)
        {
            if(ex is null) return BadRequest();

            _heraclesContext.ExerciseType?.Add(ex);
            _heraclesContext.SaveChanges();

            return new CreatedAtRouteResult("GetExerciseTypes", new { id = ex.Id, ex});
        }

        [HttpPut("{id:int}")]
        public ActionResult<ExerciseType> Put(int id, ExerciseType ex)
        {
            if(ex is null || ex.Id != id) return BadRequest();

            _heraclesContext.Entry<ExerciseType>(ex).State = EntityState.Modified;
            _heraclesContext.SaveChanges();

            return Ok(ex);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var categoria = _heraclesContext.ExerciseType?.FirstOrDefault(c => c.Id == id);

            if(categoria is null) return NotFound();

            _heraclesContext.ExerciseType?.Remove(categoria);
            _heraclesContext.SaveChanges();

            return Ok(categoria);
        }
    }
}