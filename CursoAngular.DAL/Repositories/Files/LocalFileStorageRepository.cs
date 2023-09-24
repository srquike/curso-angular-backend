using CursoAngular.Repository.Files;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CursoAngular.DAL.Repositories.Files
{
    public class LocalFileStorageRepository : IFilesStorageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LocalFileStorageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task<bool> DeleteFileAsync(string fileUri, string containerName)
        {
            var fileName = Path.GetFileName(fileUri);
            var filePath = Path.Combine(webHostEnvironment.WebRootPath, containerName, fileName);

            if (!File.Exists(filePath))
            {
                File.Delete(filePath);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public async Task<string> UpdateFileAsync(string fileUri, string containerName, Stream fileStream, string fileName)
        {
            await DeleteFileAsync(fileUri, containerName);
            return await UploadFileAsync(containerName, fileStream, fileName);
        }

        public async Task<string> UploadFileAsync(string containerName, Stream fileStream, string fileName)
        {
            var fileExtension = Path.GetExtension(fileName);
            var newFileName = string.Concat(Guid.NewGuid().ToString(), fileExtension);
            var directoryPath = Path.Combine(webHostEnvironment.WebRootPath, containerName);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var filePath = Path.Combine(directoryPath, newFileName);
            using var memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream);
            var content = memoryStream.ToArray();
            await File.WriteAllBytesAsync(filePath, content, default);
            var currentPath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
            var databasePath = Path.Combine(currentPath, containerName, newFileName).Replace("\\", "/");

            return databasePath;
        }
    }
}
