using heracles_api.Context;
using Heracles.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;


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

        static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
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

        [HttpGet("/Leaderboard")]
        public ActionResult<IEnumerable<string>> Leaderboard()
        {
            var emails = _heraclesContext.Users
                ?.OrderByDescending(x => x.TotalScore)
                .Select(x => x.Username)
                .AsNoTracking()
                .ToList();

            return emails is null ? NotFound() : emails;
        }

        [HttpPost("Authentification")]
        public ActionResult<User> Authenticate(User loginUser)
        {
            if (loginUser == null)
            {
                return BadRequest("Invalid login data.");
            }

            // Find the user by email (or username) in the database
            User user = _heraclesContext.Users.FirstOrDefault(x => x.Mail == loginUser.Mail);

            if (user == null)
            {
                // User not found
                return NotFound("User not found.");
            }

            // Verify the entered password against the stored hashed password
            if (BCrypt.Net.BCrypt.Verify(loginUser.Password, user.Password))
            {
                // Password is correct, you can return the user's data here
                return user;
            }
            else
            {
                // Password is incorrect
                return Unauthorized("Incorrect password.");
            }
        }



        [HttpPost("Register")]
        public ActionResult<User> Register(User newUser)
        {
            if (newUser == null)
            {
                return BadRequest("Invalid user data.");
            }

            // Hash the user's password before saving it to the database
            newUser.Password = HashPassword(newUser.Password);

            _heraclesContext.Users.Add(newUser);
            _heraclesContext.SaveChanges();

            // Return the newly created user
            return new CreatedAtRouteResult("GetUser", new { id = newUser.Id }, newUser);
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