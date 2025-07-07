using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Categories.Handlers
{
    internal class DeleteCategoryHandler : ICommandHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _repository;
        public DeleteCategoryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetAsync(c => c.Id == request.Id, c => c.SubCategories);
            if (category is null)
                throw new BusinessException("دسته بندی یافت نشد.");

            if (category.SubCategories.Any())
                throw new BusinessException("برای این دسته بندی، زیر مجموعه ایجاد شده است، ابتدا آن ها را پاک نمایید.");

            await _repository.DeleteAsync(request.Id);
        }
    }
}
