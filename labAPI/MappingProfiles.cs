using AutoMapper;
using labAPI.DTOs;
using labAPI.DTOs.ChemicalsDTO;
using labAPI.DTOs.EquipmentDTO;
using labAPI.DTOs.NonAccademic_DTO;
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
                
            CreateMap<NonAcademic, NonAcademicOutputDTO>();
            CreateMap<NonAcademicInputDTO, NonAcademic>();

            CreateMap<Chemicals, ChemicalsOutputDTO>();
            CreateMap<ChemicalsInputDTO, Chemicals>();

            CreateMap<Equipment, EquipmentOutputDTO>().ForMember(dest => dest.Photo, sour => sour.MapFrom<int?>(src => src.PhotoId));
            CreateMap<EquipmentInputDTO, Equipment>();



        }
    }
}
