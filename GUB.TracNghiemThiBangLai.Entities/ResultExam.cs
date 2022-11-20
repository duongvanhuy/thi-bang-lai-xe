using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUB.TracNghiemThiBangLai.Entities
{
    public  class ResultExam : BaseEnitities
    {
        public string CMND { get; set; }
        /// <summary>
        /// số câu đúng
        /// </summary>
        public int NumberOfCorrect { get; set; }
        /// <summary>
        /// đạt hay chưa đạt
        /// </summary>
        public string Status { get; set; }
        public int ComputerNum { get; set; }
    }
}
