using GUB.TracNghiemThiBangLai.Entities;
using GUB.TracNghiemThiBangLai.Share.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUB.TracNghiemThiBangLai.Share.Controller
{
    public class AppSettingRepository
    {
        private readonly ServiceBase _service;

        public AppSettingRepository()
        {
            _service = new ServiceBase();
        }

        public async Task<AppSettingEntities> GetAppSettingKey(int key)
        {
            var idObject = new { key = key };
            var data = new AppSettingEntities();
            data = await _service.CallAPIGet<AppSettingEntities>("AppSettingEn/GetAppSetting", idObject);
            return data;
        }

        public async Task<AppSettingEntities> UpdateAppSettingKey(AppSettingEntities app)
        {
           
            var data = new AppSettingEntities();
            data = await _service.CallAPIPost<AppSettingEntities>("AppSettingEn/UpdateAppSetting", app);
            return data;
        }
    }
}
