using AutoMapper;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Profiles.AfterMaps;
using DataModels = StudentAdminPortal.API.DataModels;


namespace StudentAdminPortal.API.Profiles
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<DataModels.Student, Student>().ReverseMap();


            CreateMap<DataModels.Gender, Gender>().ReverseMap();


            CreateMap<DataModels.Address, Address>().ReverseMap();

            CreateMap<UpdateStudentRequest, DataModels.Student>()
                .AfterMap<UpdatedStudentRequestAfterMap>();
                //.ForMember(dest => dest.Address.PhysicalAddress, opt => opt.MapFrom(src => src.PhysicalAddress))
                //.ForMember(dest => dest.Address.PostalAddress, opt => opt.MapFrom(src => src.PostalAddress));
            CreateMap<AddStudentRequest, DataModels.Student>()
                .AfterMap<AddedStudentRequestAfterMap>();

        }

    }
}
