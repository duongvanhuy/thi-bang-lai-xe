using AutoMapper;
using GUB.TracNghiemThiBangLai.API.Model;
using GUB.TracNghiemThiBangLai.Entities;

namespace GUB.TracNghiemThiBangLai.API.Mapper
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountDto>();
            CreateMap<AccountDto, Account>();
        }  
    }
}
