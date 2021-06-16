using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Tamro.Entities;
using Tamro.Web.ApiModels;

namespace Tamro.Tests
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
