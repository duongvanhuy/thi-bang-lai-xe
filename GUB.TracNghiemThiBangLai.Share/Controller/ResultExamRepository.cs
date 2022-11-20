using GUB.TracNghiemThiBangLai.Entities;
using GUB.TracNghiemThiBangLai.Share.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUB.TracNghiemThiBangLai.Share.Controller
{
    public class ResultExamRepository
    {
        private readonly ServiceBase _service;

        public ResultExamRepository()
        {
            _service = new ServiceBase();
        }

        public async Task<ResultExam> GetResultExam(int id)
        {
            var idObject = new { id = id };
            var data = new ResultExam();
            data = await _service.CallAPIGet<ResultExam>("ResultExam/GetResultExam", idObject);
            return data;
        }

        public async Task<List<ResultExam>> GetResultExams()
        {
            var data = new List<ResultExam>();
            data = await _service.CallAPIGet<List<ResultExam>>("ResultExam/GetAllResultExam", null);
            return data;
        }

        public async Task<ResultExam> CreateResultExam(ResultExam resultExam)
        {
            var data = new ResultExam();
            data = await _service.CallAPIPost<ResultExam>("ResultExam/CreateResultExam", resultExam);
            return data;
        }

        public async Task<ResultExam> UpdateResultExam(ResultExam resultExam)
        {
            var data = new ResultExam();
            data = await _service.CallAPIPost<ResultExam>("ResultExam/UpdateResultExam", resultExam);
            return data;
        }

        public async Task<ResultExam> DeleteResultExam(int id)
        {
            var idObject = new { id = id };
            var data = new ResultExam();
            data = await _service.CallAPIGet<ResultExam>("ResultExam/DeleteResultExam", idObject);
            return data;
        }
    }
}
