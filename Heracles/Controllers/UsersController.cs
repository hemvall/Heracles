using heracles_api.Context;
using Heracles.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
/*using BCrypt.Net;
*/
namespace Users.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly HeraclesContext _heraclesContext;

        public UsersController(HeraclesContext heraclesContext)
        {
            _heraclesContext = heraclesContext;
        }
        
        [HttpGet("{id:int}", Name = "GetUsers")]
        public ActionResult<User> Get(int id)
        {
            var categoria = _heraclesContext.Users?.AsNoTracking().FirstOrDefault(x => x.Id == id);
            return categoria is null ? NotFound() : categoria;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            var exs = _heraclesContext.Users?.AsNoTracking().ToList();
            return exs is null ? NotFound() : exs;
        }

        [HttpPost("Authentification")]
        public ActionResult<User> Authentification(User ex)
        {
            if (ex is null) return BadRequest();

            User usr = _heraclesContext.Users.FirstOrDefault(x => x.Mail == ex.Mail && x.Password == ex.Password);

            if (usr is null) return NotFound();

            return usr;
        }

        [HttpPost]
        public ActionResult<User> Post(User ex)
        {
            if(ex is null) return BadRequest();
            
            _heraclesContext.SaveChanges();

            return new CreatedAtRouteResult("GetUser", new { id = ex.Id, ex});
        }

        [HttpPut("{id:int}")]
        public ActionResult<User> Put(int id, User ex)
        {
            if(ex is null || ex.Id != id) return BadRequest();

            _heraclesContext.Entry<User>(ex).State = EntityState.Modified;
            _heraclesContext.SaveChanges();

            return Ok(ex);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var categoria = _heraclesContext.Users?.FirstOrDefault(c => c.Id == id);

            if(categoria is null) return NotFound();

            _heraclesContext.Users?.Remove(categoria);
            _heraclesContext.SaveChanges();

            return Ok(categoria);
        }
    }
}