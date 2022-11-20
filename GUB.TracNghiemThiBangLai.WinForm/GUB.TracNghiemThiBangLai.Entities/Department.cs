using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUB.TracNghiemThiBangLai.Entities
{
    public class Department : BaseEnitities
    {
        public string name { get; set; }

        public ICollection<Computer> Computers { get; set; }
    }
}
