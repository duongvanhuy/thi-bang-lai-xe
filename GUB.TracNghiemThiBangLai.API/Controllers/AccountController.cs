using GUB.TracNghiemThiBangLai.API.Model;
using GUB.TracNghiemThiBangLai.Entities;
using GUB.TracNghiThiBangLai.EntityFrameworkCore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GUB.TracNghiemThiBangLai.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly GUBDBContext _context;

        public AccountController(GUBDBContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Login(AccountDto a)
        {
            try
            {
                var account = await _context.Set<Account>().Where(x => x.UserName == a.UserName && x.Password == a.Password).FirstOrDefaultAsync();
                return Ok(account);
            }
           
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Register(AccountDto a)
        {
            try
            {
                var account = new Account()
                {
                    UserName = a.UserName,
                    Password = a.Password
                };
                _context.Set<Account>().Add(account);
                await _context.SaveChangesAsync();
                return Ok(account);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
           
    }
}
