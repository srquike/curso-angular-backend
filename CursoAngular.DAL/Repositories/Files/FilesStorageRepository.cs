using Azure.Storage.Blobs;
using CursoAngular.Repository.Files;

namespace CursoAngular.DAL.Repositories.Files
{
    public class FilesStorageRepository : IFilesStorageRepository
    {
        private readonly string _azureBlobStorageConnectionString;

        public FilesStorageRepository(string azureBlobStorageConnectionString)
        {
            _azureBlobStorageConnectionString = azureBlobStorageConnectionString;
        }

        public async Task<string> UploadFileAsync(string containerName, Stream fileStream, string fileName)
        {
            var containerClient = new BlobContainerClient(_azureBlobStorageConnectionString, containerName);
            var fileExtension = Path.GetExtension(fileName);
            var newFileName = string.Concat(Guid.NewGuid().ToString(), fileExtension);

            await containerClient.CreateIfNotExistsAsync();
            await containerClient.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

            var blobClient = containerClient.GetBlobClient(newFileName);

            await blobClient.UploadAsync(fileStream);

            return blobClient.Uri.ToString();
        }

        public async Task<bool> DeleteFileAsync(string fileUri, string containerName)
        {
            if (!string.IsNullOrEmpty(fileUri))
            {
                var containerClient = new BlobContainerClient(_azureBlobStorageConnectionString, containerName);
                var fileName = Path.GetFileName(fileUri);

                await containerClient.CreateIfNotExistsAsync();

                var blobClient = containerClient.GetBlobClient(fileName);

                var result = await blobClient.DeleteIfExistsAsync();

                return result.Value;
            }

            return false;
        }

        public async Task<string> UpdateFileAsync(string fileUri, string containerName, Stream fileStream, string fileName)
        {
            await DeleteFileAsync(fileUri, containerName);
            return await UploadFileAsync(containerName, fileStream, fileName);
        }
    }
}
