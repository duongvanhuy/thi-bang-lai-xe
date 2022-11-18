using GUB.TracNghiemThiBangLai.Entities;
using GUB.TracNghiThiBangLai.EntityFrameworkCore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GUB.TracNghiemThiBangLai.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly GUBDBContext _context;

        public UserController(GUBDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var users = await _context.Set<User>().ToListAsync();
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var users = await _context.Set<User>().FindAsync(id);
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            try
            {
                _context.Set<User>().Add(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser(User user)
        {
            try
            {
                _context.Set<User>().Update(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _context.Set<User>().FindAsync(id);
                _context.Set<User>().Remove(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
