using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUB.TracNghiemThiBangLai.Entities
{
    public class User :BaseEnitities
    {
        public string FullName { get; set; }
        public string CCCD { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
    }
}
