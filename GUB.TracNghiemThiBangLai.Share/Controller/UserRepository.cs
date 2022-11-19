using GUB.TracNghiemThiBangLai.Entities;
using GUB.TracNghiemThiBangLai.Share.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUB.TracNghiemThiBangLai.Share.Controller
{
    public class UserRepository
    {
        private readonly ServiceBase _service;
        public UserRepository()
        {
            _service = new ServiceBase();
        }

        public async Task<List<User>> GetUsers()
        {
            var data = new List<User>();
            data = await _service.CallAPIGet<List<User>>("User/GetAllUser", null);
            return data;
        }

        public async Task<User> GetByUser(int id)
        {
            var idObject = new { id = id };
            var data = new User();
            data = await _service.CallAPIGet<User>("User/GetByUser", idObject);
            return data;
        }

        public async Task<User> AddUser(User user)
        {
            var data = new User();
            data = await _service.CallAPIPost<User>("User/CreateUser", user);
            return data;
        }

        public async Task<User> UpdateUser(User user)
        {
            var data = new User();
            data = await _service.CallAPIPost<User>("User/UpdateUser", user);
            return data;
        }

        public async Task<User> DeleteUser(int id)
        {
            var idObject = new { id = id };
            var data = new User();
            data = await _service.CallAPIGet<User>("User/DeleteUser", idObject);
            return data;
        }

        public async Task<List<User>> SearchUser(string name)
        {
            var data = new List<User>();
            data = await _service.CallAPIGet<List<User>>("User/SearchUser", name);
            return data;
        }

        public async Task<User> ResetPassword(int id)
        {
            var idObject = new { id = id };
            var data = new User();
            data = await _service.CallAPIGet<User>("User/ResetPassword", idObject);
            return data;
        }


    }
}
