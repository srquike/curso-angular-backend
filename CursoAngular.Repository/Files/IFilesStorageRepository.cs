namespace CursoAngular.Repository.Files
{
    public interface IFilesStorageRepository
    {
        Task<bool> DeleteFileAsync(string fileUri, string containerName);
        Task<string> UpdateFileAsync(string fileUri, string containerName, Stream fileStream, string fileName);
        Task<string> UploadFileAsync(string containerName, Stream fileStream, string fileName);
    }
}
