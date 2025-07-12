namespace ScrapDealer.Application.Services
{
    public interface IFileStorageService
    {
        Task<Guid> UploadAsync(Stream fileStream, string contentType, string bucketName);
        Task<(Stream Stream, string ContentType)> DownloadAsync(Guid fileId, string bucketName);
    }
}
