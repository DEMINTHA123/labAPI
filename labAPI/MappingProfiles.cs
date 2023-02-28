using AutoMapper;
using labAPI.DTOs;
using labAPI.Entities;

namespace labAPI
{
    public class MappingProfiles : Profile  
    {
        public MappingProfiles()
        {
            CreateMap<Lab, LabOutputDTO>();
            CreateMap<LabInputDTO, Lab>();
            CreateMap<AcademicInputDTO, Academic>();
            CreateMap<Academic, AcademicOutputDTO>();

        }
    }
}
