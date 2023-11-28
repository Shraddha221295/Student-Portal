using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
namespace StudentAdminPortal.API.Repositories
{
    public class LoacalStorageImageRepository : IImageRepository
    {
       async Task<string> IImageRepository.Upload(IFormFile file, string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Resources/Images",fileName);

            using Stream filestream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(filestream);

            return GetServerRelativePath(fileName);
        }

        private string GetServerRelativePath(string fileName)
        {
            return Path.Combine(@"Resources/Images", fileName);
        }


    }
}
