using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUB.TracNghiemThiBangLai.Host.Model
{
    public class SheetInfor
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime? Birthday { get; set; }
        public string CCCD { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        
        /// <summary>
        /// vắng mặt hay không
        /// </summary>
        public int? Status { get; set; }
        /// <summary>
        /// Đã thi hay chưa
        /// </summary>
        public int? IsExamed { get; set; }
        
        /// <summary>
        ///  điểm thi
        /// </summary>
        public int? Score { get; set; }

    }
}
