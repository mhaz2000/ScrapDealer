using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Categories.Handlers
{
    internal class DeleteSubCategoryHandler : ICommandHandler<DeleteSubCategoryCommand>
    {
        private readonly ISubCategoryRepository _repository;
        public DeleteSubCategoryHandler(ISubCategoryRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(DeleteSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var subCategory = await _repository.GetAsync(c => c.Id == request.Id);
            if (subCategory is null)
                throw new BusinessException("دسته بندی یافت نشد.");

            await _repository.DeleteAsync(request.Id);
        }
    }
}
