using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUB.TracNghiemThiBangLai.Share.Service
{
    public interface IService
    {
        Task<T> CallAPIPost<T>(string endpoint, object data);
        Task<T> CallAPIGet<T>(string endpoint, object data);
    }
}
