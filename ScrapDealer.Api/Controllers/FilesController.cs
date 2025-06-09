using ScrapDealer.Application.Commands.Files;
using ScrapDealer.Application.Queries.Files;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace ScrapDealer.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : BaseController
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public FilesController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Upload([FromForm] IFormFile file)
        {
            using var stream = new MemoryStream();

            file.CopyTo(stream);
            var fileId = await _commandDispatcher.DispatchAsync<UploadFileCommand, Guid>(new UploadFileCommand(stream));

            return OkOrNotFound(fileId);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Download(Guid id)
        {
            var fileName = "image.jpg";
            var file = await _queryDispatcher.QueryAsync(new DownloadFileQuery(id));

            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            Response.Headers.Append("Access-Control-Allow-Headers", "Content-Disposition");
            Response.Headers.Append("X-Content-Type-Options", "nosniff");

            return File(file, contentType, fileName);
        }
    }
}
