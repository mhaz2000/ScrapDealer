using ScrapDealer.Shared.Abstractions.Queries;

namespace ScrapDealer.Application.Queries.Files
{
    public record DownloadFileQuery(Guid Id) : IQuery<FileStream>;
}
