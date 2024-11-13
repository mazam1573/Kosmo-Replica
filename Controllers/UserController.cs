using EduConsultant.Data;
using EduConsultant.DTOs;
using EduConsultant.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduConsultant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly EduConsultantContext _context;
        public UserController(EduConsultantContext context)
        {
            _context = context;
        }
        // GET: api/users
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.User.ToListAsync();
        }

        // GET: api/userById/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(long id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/user
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        //PUT: api/user/id
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(long id, User user)
        {
            if(id != user.Id) return BadRequest();

            _context.Entry(user).State = EntityState.Modified;
            var userData = await _context.User.FindAsync(id);
            if (userData == null) 
                return NotFound($"User with id {id} not found");

            userData.First_Name = user.First_Name;
            userData.Last_Name = user.Last_Name;
            userData.Email = user.Email;
            userData.Phone = user.Phone;
            userData.Status = user.Status;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) {
                if (!_context.User.Any(u => u.Id == id))
                    return NotFound();
                throw;
            }
            return Ok(userData);
        }

        // DELETE: api/users/1
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null) return NotFound();

            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return Ok($"User with id {id} is deleted!");
        }
    }
}
