using GUB.TracNghiemThiBangLai.Entities;
using GUB.TracNghiThiBangLai.EntityFrameworkCore.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GUB.TracNghiemThiBangLai.API.Controllers
{
    //[EnableCors("MyPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly GUBDBContext _context;

        public QuestionController(GUBDBContext context)
        {
            _context = context;
        }
        //[DisableCors]
        [HttpGet]

        public async Task<IActionResult> GetAllQuestion()
        {
            try
            {
                // lấy ra 25 câu hỏi ngẫu nhiên
                var questions = await _context.Questions.OrderBy(x => Guid.NewGuid()).Take(25).ToListAsync();
                // var questions = await _context.Set<Question>().ToListAsync();
                return Ok(questions);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetByQuestion(int id)
        {
            try
            {
                var questions = await _context.Set<Question>().FindAsync(id);
                return Ok(questions);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateQuestion(Question question)
        {
            try
            {
                _context.Set<Question>().Add(question);
                await _context.SaveChangesAsync();
                return Ok(question);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateQuestion(Question question)
        {
            try
            {
                _context.Set<Question>().Update(question);
                await _context.SaveChangesAsync();
                return Ok(question);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            try
            {
                var question = await _context.Set<Question>().FindAsync(id);
                _context.Set<Question>().Remove(question);
                await _context.SaveChangesAsync();
                return Ok(question);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
