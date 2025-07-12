using Minio;
using Minio.DataModel.Args;
using ScrapDealer.Application.Services;

public class MinioFileStorageService : IFileStorageService
{
    private readonly MinioClient _client;

    public MinioFileStorageService(MinioClient client)
    {
        _client = client;
    }

    private async Task EnsureBucketExists(string bucketName)
    {
        var exists = await _client.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
        if (!exists)
        {
            await _client.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
        }
    }

    public async Task<Guid> UploadAsync(Stream fileStream, string contentType, string bucketName)
    {
        await EnsureBucketExists(bucketName);

        var fileId = Guid.NewGuid();
        var fileName = $"{fileId}.dat";

        await _client.PutObjectAsync(new PutObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileName)
            .WithStreamData(fileStream)
            .WithObjectSize(fileStream.Length)
            .WithContentType(contentType));

        return fileId;
    }

    public async Task<(Stream Stream, string ContentType)> DownloadAsync(Guid fileId, string bucketName)
    {
        var fileName = $"{fileId}.dat";
        var memoryStream = new MemoryStream();

        var stat = await _client.StatObjectAsync(new StatObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileName));

        await _client.GetObjectAsync(new GetObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileName)
            .WithCallbackStream(stream => stream.CopyTo(memoryStream)));

        memoryStream.Position = 0;
        return (memoryStream, stat.ContentType);
    }
}
