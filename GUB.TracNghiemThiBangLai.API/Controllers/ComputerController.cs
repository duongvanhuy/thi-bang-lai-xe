using GUB.TracNghiemThiBangLai.Entities;
using GUB.TracNghiThiBangLai.EntityFrameworkCore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GUB.TracNghiemThiBangLai.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ComputerController : ControllerBase
    {
        private readonly GUBDBContext _context;

        public ComputerController(GUBDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComputer()
        {
            try
            {
                var computers = await _context.Set<Computer>().ToListAsync();
                return Ok(computers);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateComputer(int id, string CCCD)
        {
            try
            {
                var computer = await _context.Set<Computer>().FindAsync(id);
                if (computer == null)
                {
                    return NotFound();
                }
                computer.CCCD = CCCD;
                _context.Set<Computer>().Update(computer);
                await _context.SaveChangesAsync();
                return Ok(computer);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
