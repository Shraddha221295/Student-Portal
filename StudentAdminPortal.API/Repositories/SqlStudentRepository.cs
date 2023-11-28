using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.DataModels;

namespace StudentAdminPortal.API.Repositories
{
    public class SqlStudentRepository : IStudentRepository
    {
        private readonly IStudentRepository _studentRepository;
        private readonly StudentAdminContext context;
        public SqlStudentRepository(StudentAdminContext context)
        {
            this.context = context;
        }


        public async Task<List<Student>> GetStudentsAsync()
        {
            return await context.Student.Include(nameof(Address)).Include(nameof(Gender)).ToListAsync();
        }
        public async Task<Student> GetStudentAsync(Guid studentId)
        {
            return await context.Student.Include(nameof(Address)).Include(nameof(Gender)).FirstOrDefaultAsync(x => x.Id == studentId);
        }
        public async Task<List<Gender>> GetAllGenderList()
        {
            return await context.Gender.ToListAsync();
        }
        public async Task<bool> Exist(Guid studentId)
        {
            return await context.Student.AnyAsync(x => x.Id == studentId);
            
        }
        public async Task<Student> UpdateStudentDataAsync(Guid studentId, Student request)
        {
           var existingStudent = await GetStudentAsync(studentId);
            if(existingStudent != null)
            {
                existingStudent.FirstName = request.FirstName;
                existingStudent.LastName = request.LastName;
                existingStudent.DateOfBirth = request.DateOfBirth;
                existingStudent.Email = request.Email;
                existingStudent.GenderId = request.GenderId;
                existingStudent.Address.PhysicalAddress = request.Address.PhysicalAddress;
                existingStudent.Address.PostalAddress = request.Address.PostalAddress;

                await context.SaveChangesAsync();
                return existingStudent;
            }
            else
            {
                return null;
            }
              
           
        }
        public async Task<Student> DeleteStundentAsync(Guid studentId)
        {
            var student = await GetStudentAsync(studentId);
            if(student != null)
            {
                context.Student.Remove(student);
                await context.SaveChangesAsync();
                return student;
            }
            else
            {
                return null;
            }
        }
       public async Task<Student> AddStudnetAsync(Student student)
       {
            var studnet = await context.Student.AddAsync(student);
            await context.SaveChangesAsync();
            return studnet.Entity;
       }

        public async Task<bool> UpdateProfileImage(Guid studentId, string profileImage)
        {
            var student = await GetStudentAsync(studentId);
            if(student != null)
            {
                student.ProfileImageUrl = profileImage.ToString();
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        //Task<bool> IStudentRepository.UpdateProfileImage(Guid studentId, Task<string> profilrImage)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
