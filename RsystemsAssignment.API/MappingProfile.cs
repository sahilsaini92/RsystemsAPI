using AutoMapper;
using RSystemsAssignment.Data.DTO;
using RSystemsAssignment.Data.Entities;

namespace RsystemsAssignment.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountDTO, Account>().ReverseMap();
            CreateMap<ClientDTO, Client>().ReverseMap();
            CreateMap<AppointmentDTO, Appointment>().ReverseMap();
        }
    }
}
