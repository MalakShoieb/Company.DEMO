using AutoMapper;
using Company.DEMO.DAL.Entities;
using Company.DEMO.PL.Models;

namespace Company.DEMO.PL.NewFolder
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeDTO, Employee>();
        }
    }
}
