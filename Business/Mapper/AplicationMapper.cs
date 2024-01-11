using AutoMapper;
using Business.DTO;
using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mapper
{
    public class AplicationMapper : Profile
    {
        public AplicationMapper()
        {
            MapUser();
        }
        public void MapUser() 
        {
            CreateMap<UserDTO,Usuario>();
            CreateMap<UserUpdateDTO, Usuario>();
        }
    }
}
