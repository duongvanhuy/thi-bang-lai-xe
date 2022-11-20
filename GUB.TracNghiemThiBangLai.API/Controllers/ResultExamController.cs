using GUB.TracNghiemThiBangLai.Entities;
using GUB.TracNghiThiBangLai.EntityFrameworkCore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GUB.TracNghiemThiBangLai.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ResultExamController : ControllerBase
    {
        private readonly GUBDBContext _context;

        public ResultExamController(GUBDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetResultExam(int id)
        {
            try
            {
                var resultExam = await _context.Set<ResultExam>().FindAsync(id);
                return Ok(resultExam);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllResultExam()
        {

            try
            {
                var x = DateTime.Now.AddMinutes(-20);
                // lấy ra danh sách các bản ghi trong 20 phút vừa qua
                //  var resultExams = await _context.Set<ResultExam>().Where(x => x.CreatedDate >= DateTime.Now.AddMinutes(-20)).ToListAsync();
                 var resultExams = await _context.Set<ResultExam>().ToListAsync();
                return Ok(resultExams);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateResultExam(ResultExam resultExam)
        {
            try
            {
                _context.Set<ResultExam>().Add(resultExam);
                await _context.SaveChangesAsync();
                return Ok(resultExam);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateResultExam(ResultExam resultExam)
        {
            try
            {
                _context.Set<ResultExam>().Update(resultExam);
                await _context.SaveChangesAsync();
                return Ok(resultExam);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteResultExam(int id)
        {
            try
            {
                var resultExam = await _context.Set<ResultExam>().FindAsync(id);
                _context.Set<ResultExam>().Remove(resultExam);
                await _context.SaveChangesAsync();
                return Ok(resultExam);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

}
