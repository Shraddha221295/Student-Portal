using AutoMapper;
using Microsoft.AspNetCore.Mvc;
//using StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.DomainModels;
using StudentAdminPortal.API.Repositories;

namespace StudentAdminPortal.API.Controllers
{
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;

        public StudentsController(IStudentRepository studentRepository, IMapper mapper, IImageRepository imageRepository)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
            this.imageRepository = imageRepository;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await studentRepository.GetStudentsAsync();
            return Ok(mapper.Map<List<Student>>(students));
        }

        [HttpGet]
        [Route("[controller]/{studentId:guid}"), ActionName("GetStudent")]
        public async Task<IActionResult> GetStudent([FromRoute]Guid studentId)
        {
            var student = await studentRepository.GetStudentAsync(studentId);
            if(student == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<Student>(student));
        }
        [HttpPut]
        [Route("[controller]/{studentId:Guid}")]
        public async Task<IActionResult> UpdateStudentDataAsync([FromRoute] Guid studentId, [FromBody] UpdateStudentRequest request)
        {
            if (await studentRepository.Exist(studentId))
            {
                var updatedStudent = await studentRepository.UpdateStudentDataAsync(studentId, mapper.Map<DataModels.Student>(request));
                if (updatedStudent.Id != null)
                {
                    return Ok(mapper.Map<Student>(updatedStudent));
                }
            }
            
              return NotFound();
        }
        [HttpDelete]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] Guid studentId)
        {
            if (await studentRepository.Exist(studentId))
            { 
             var deletedStudent = await studentRepository.DeleteStundentAsync(studentId);
                if(deletedStudent != null)
                {
                    return Ok(mapper.Map<Student>(deletedStudent));
                }
            }
            return NotFound();
        }
        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddStudentAsyn([FromBody] AddStudentRequest request)
        {

            var student = await studentRepository.AddStudnetAsync(mapper.Map<DataModels.Student>(request));
            return CreatedAtAction(nameof(GetStudent), new { studentId = student.Id }, mapper.Map<Student>(student));

        }
        [HttpPost]
        [Route("[controller]/{studentId:guid}/upload-image")]
        public async Task<IActionResult> UploadProfileImage([FromRoute] Guid studentId, IFormFile profileFileImage)
        {
            // check the student is exist or not.
            if(await studentRepository.Exist(studentId))
            {
                // upload the image to local storage.
                //update the profile image path in the database.
                var fileName = Guid.NewGuid() + Path.GetExtension(profileFileImage.FileName);

               var imagePath = await  imageRepository.Upload(profileFileImage,fileName);
                if(await studentRepository.UpdateProfileImage(studentId, imagePath))
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,"Error Uploading Image");
                }
            }
            return NotFound();
        }
    }
}
