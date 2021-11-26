using AutoMapper;
using EmployeeAPI.Models;
using EmployeeAPI.Models.Dtos;

namespace EmployeeAPI.EmployeeMapper
{
    public class EmployeeMappings : Profile
    {
        public EmployeeMappings()
        {
            CreateMap<Basic,BasicDto>().ReverseMap();
            //it will map Basic to BasicDto and viceversa
        }
    }
}
