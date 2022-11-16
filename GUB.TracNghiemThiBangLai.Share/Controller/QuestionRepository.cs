using GUB.TracNghiemThiBangLai.Entities;
using GUB.TracNghiemThiBangLai.Share.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUB.TracNghiemThiBangLai.Share.Controller
{
    public  class QuestionRepository
    {
        private readonly ServiceBase _service;
        public QuestionRepository()
        {
            _service = new ServiceBase();
        }
        public  async Task<List<Question>> GetQuestions()
        {
            var data = new List<Question>();
            data = await _service.CallAPIGet<List<Question>>("Question/GetAllQuestion", null);
            return data;
        }

        public async Task<Question> GetByQuestion(int id)
        {
            var idObject = new { id = id };
            var data = new Question();
            data = await _service.CallAPIGet<Question>("Question/GetByQuestion", idObject);
            return data;
        }
    }
}
