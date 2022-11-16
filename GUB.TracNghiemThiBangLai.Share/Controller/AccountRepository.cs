using GUB.TracNghiemThiBangLai.Entities;
using GUB.TracNghiemThiBangLai.Share.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUB.TracNghiemThiBangLai.Share.Controller
{
    public class AccountRepository
    {
        private readonly ServiceBase _service;

        public AccountRepository()
        {
            _service = new ServiceBase();
        }
        
        public async Task<Account> Login(Account account)
        {
            var data = new Account();
            data = await _service.CallAPIPost<Account>("Account/Login", account);
            return data;
        }

        public async Task<Account> Register(Account account)
        {
            var data = new Account();
            data = await _service.CallAPIPost<Account>("Account/Register", account);
            return data;
        }
    }
}
