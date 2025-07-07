using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Categories.Handlers
{
    internal class UpdateCategoryHandler : ICommandHandler<UpdateCategoryCommand>
    {
        private readonly ICategoryFactory _factory;
        private readonly ICategoryRepository _repository;
        private readonly ICategoryReadService _readService;
        public UpdateCategoryHandler(ICategoryFactory factory, ICategoryRepository repository, ICategoryReadService readService)
        {
            _repository = repository;
            _factory = factory;
            _readService = readService;
        }
        public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetAsync(c => c.Id == request.Id);
            if (category is null)
                throw new BusinessException("دسته بندی یافت نشد.");

            if(await _readService.ExistsByNameAsync(request.Name, request.Id))
                throw new BusinessException("عنوان دسته بندی تکراری است.");

            _factory.Update(request.Name, category);
            await _repository.UpdateAsync(category);
        }
    }
}