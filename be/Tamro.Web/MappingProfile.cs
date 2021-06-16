using AutoMapper;
using Tamro.Entities;
using Tamro.Web.ApiModels;

namespace Tamro.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserEntity, UserModel>();
            CreateMap<UserModel, UserEntity>();
        }
    }
}
