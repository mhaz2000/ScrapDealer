using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Categories.Handlers
{
    internal class AddCategoryHandler : ICommandHandler<AddCategoryCommand>
    {
        private readonly ICategoryFactory _factory;
        private readonly ICategoryRepository _repository;
        private readonly ICategoryReadService _readService;

        public AddCategoryHandler(ICategoryFactory factory, ICategoryRepository repository, ICategoryReadService readService)
        {
            _factory = factory;
            _repository = repository;
            _readService = readService;
        }

        public async Task Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            if (await _readService.ExistsByNameAsync(request.Name))
                throw new BusinessException("نام دسته بندی تکراری است.");

            var category = _factory.Create(request.Name);

            await _repository.AddAsync(category);
        }
    }
}
