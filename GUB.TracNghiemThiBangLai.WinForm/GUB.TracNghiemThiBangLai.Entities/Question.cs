using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUB.TracNghiemThiBangLai.Entities
{
    public class Question : BaseEnitities
    {
        public string Content { get; set; }
        public string Image { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }
        public string CorrectAnswer { get; set; }
      
    }
}
