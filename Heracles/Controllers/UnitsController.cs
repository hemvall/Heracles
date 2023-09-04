using heracles_api.Context;
using Heracles.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Users.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly HeraclesContext _heraclesContext;

        public UnitsController(HeraclesContext heraclesContext)
        {
            _heraclesContext = heraclesContext;
        }
        
        [HttpGet("{id:int}", Name = "GetUnits")]
        public ActionResult<Unit> Get(int id)
        {
            var categoria = _heraclesContext.Units?.AsNoTracking().FirstOrDefault(x => x.Id == id);
            return categoria is null ? NotFound() : categoria;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Unit>> Get()
        {
            var exs = _heraclesContext.Units?.AsNoTracking().ToList();
            return exs is null ? NotFound() : exs;
        }
    }
}