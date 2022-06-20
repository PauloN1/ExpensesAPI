using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using API.DTOs;
using API.Entities;

namespace API.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Expenses, ExpensesDTO>(); //Map from Developer Object to DeveloperDTO Object
        }
    }
}