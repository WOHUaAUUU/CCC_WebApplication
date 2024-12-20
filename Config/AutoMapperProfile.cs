using AutoMapper;
using Model.Dto.User;
using Model.Entitys;

namespace Shoping_WebAPI.Config
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() {
            //角色
 
            //用户
            CreateMap<UserAdd,Users>();
            CreateMap<UserEdit, Users>();



        }
    }
}
