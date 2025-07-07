using ScrapDealer.Application.Queries.Files;
using ScrapDealer.Shared.Abstractions.Exceptions;
using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Infrastructure.Queries.Handlers.Files
{
    internal class DownloadFileHandler : IQueryHandler<DownloadFileQuery, FileStream>
    {
        public async Task<FileStream> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
        {
            var path = Directory.GetCurrentDirectory() + "/FileStorage";
            var filePath = Path.Combine(path, $"{request.Id}.dat");

            if (!File.Exists(filePath))
                throw new BusinessException("فایل مورد نظر یافت نشد.");

            return await Task.FromResult(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read));

        }
    }
}
