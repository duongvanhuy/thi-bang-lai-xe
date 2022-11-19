using GUB.TracNghiemThiBangLai.Entities;
using GUB.TracNghiemThiBangLai.Share.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUB.TracNghiemThiBangLai.Share.Controller
{
    public class ComputerRepository
    {
        private readonly ServiceBase _service;

        public ComputerRepository()
        {
            _service = new ServiceBase();
        }
        
        public async Task<List<Computer>> GetComputers()
        {
            var data = new List<Computer>();
            data = await _service.CallAPIGet<List<Computer>>("Computer/GetAllComputer", null);
            return data;
        }

        public async Task<Computer> UpdateComputer(int id, string CCCD)
        {
            var obj = new { id = id, CCCD = CCCD };
            var data = new Computer();
            data = await _service.CallAPIGet<Computer>("Computer/UpdateComputer", obj);
            return data;
        }
    }
}
