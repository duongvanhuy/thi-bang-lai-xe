using GUB.TracNghiemThiBangLai.Entities;
using GUB.TracNghiThiBangLai.EntityFrameworkCore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GUB.TracNghiemThiBangLai.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppSettingEn : ControllerBase
    {
        private readonly GUBDBContext _context;

        public AppSettingEn(GUBDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAppSetting()
        {
            try
            {
                var appSettings = await _context.Set<AppSettingEntities>().ToListAsync();
                return Ok(appSettings);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAppSetting(int key)
        {
            try
            {
                var appSettings = await _context.Set<AppSettingEntities>().FirstOrDefaultAsync(x => x.Key == key);
                return Ok(appSettings);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAppSettingKey(AppSettingEntities app)
        {
            try
            {
               
                _context.Set<AppSettingEntities>().Add(app);
                await _context.SaveChangesAsync();
                return Ok(app);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateAppSetting(AppSettingEntities app)
        {
            try
            {
                var appSetting = await _context.Set<AppSettingEntities>().FirstOrDefaultAsync(x => x.Key == app.Key);
                appSetting.valueKey = app.valueKey;
                _context.Set<AppSettingEntities>().Update(appSetting);
                await _context.SaveChangesAsync();
                return Ok(appSetting);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //[HttpDelete]
        //public async Task<IActionResult> DeleteAppSetting(int id)
        //{
        //    try
        //    {
        //        var appSetting = await _context.Set<AppSetting>().FindAsync(id);
        //        _context.Set<AppSetting>().Remove(appSetting);
        //        await _context.SaveChangesAsync();
        //        return Ok(appSetting);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}
    }
}
