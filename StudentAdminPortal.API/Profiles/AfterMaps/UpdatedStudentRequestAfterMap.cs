using AutoMapper;
using StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.DomainModels;
using DataModels = StudentAdminPortal.API.DomainModels;

namespace StudentAdminPortal.API.Profiles.AfterMaps
{
    public class UpdatedStudentRequestAfterMap : IMappingAction<UpdateStudentRequest, DataModels.Student>
    {
       
        public void Process(UpdateStudentRequest source, DataModels.Student destination, ResolutionContext context)
        {   destination.Id = Guid.NewGuid();
            destination.Address = new DataModels.Address()
            {
               Id = Guid.NewGuid(),
               PhysicalAddress = source.PhysicalAddress,
               PostalAddress = source.PostalAddress,
            };
        }
    }
}   
