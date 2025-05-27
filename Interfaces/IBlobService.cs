namespace EduSyncAPI.Interfaces
{
    public interface IBlobService
    {
        Task<string> UploadFileAsync(IFormFile file, string containerName);
    }

}
