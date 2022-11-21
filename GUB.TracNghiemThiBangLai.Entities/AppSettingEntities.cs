using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUB.TracNghiemThiBangLai.Entities
{
    public class AppSettingEntities : BaseEnitities
    {
        /// <summary>
        /// type key
        /// </summary>
        public int Key { get; set; }
        /// <summary>
        /// tên key
        /// </summary>
        public string? NameKey { get; set; }
        /// <summary>
        /// giá trị của key
        /// </summary>
        public int valueKey { get; set; }
    }
}
