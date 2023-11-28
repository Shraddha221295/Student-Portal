using Microsoft.AspNetCore.Mvc;
using StudentAdminPortal.API.DataModels;
namespace StudentAdminPortal.API.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudentsAsync();
        Task<Student> GetStudentAsync(Guid studentId);
        Task<List<Gender>> GetAllGenderList();
        Task<bool>Exist(Guid studentId);
        Task<Student> UpdateStudentDataAsync(Guid studentId, Student request);
        Task<Student> DeleteStundentAsync(Guid studentId);
        Task<Student> AddStudnetAsync(Student student);
        Task<bool> UpdateProfileImage(Guid studentId, string profilrImage);

    }
}
