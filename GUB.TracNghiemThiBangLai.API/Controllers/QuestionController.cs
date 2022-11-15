using GUB.TracNghiemThiBangLai.Entities;
using GUB.TracNghiThiBangLai.EntityFrameworkCore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GUB.TracNghiemThiBangLai.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly GUBDBContext _context;

        public QuestionController(GUBDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> getAllQuestion()
        {
            try
            {
                var questions = await _context.Set<Question>().ToListAsync();
                return Ok(questions);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> getByQuestion(int id)
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
        public async Task<IActionResult> createQuestion(Question question)
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
        public async Task<IActionResult> updateQuestion(Question question)
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
        public async Task<IActionResult> deleteQuestion(int id)
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
