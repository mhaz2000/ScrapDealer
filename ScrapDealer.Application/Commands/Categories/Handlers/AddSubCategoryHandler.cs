using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Categories.Handlers
{
    internal class AddSubCategoryHandler : ICommandHandler<AddSubCategoryCommand>
    {
        private readonly ISubCategoryFactory _factory;
        private readonly ISubCategoryRepository _repository;
        private readonly ISubCategoryReadService _readService;
        private readonly ICategoryRepository _categoryRepository;

        public AddSubCategoryHandler(ISubCategoryFactory factory, ISubCategoryRepository repository,
            ISubCategoryReadService readService, ICategoryRepository categoryRepository)
        {
            _factory = factory;
            _repository = repository;
            _readService = readService;
            _categoryRepository = categoryRepository;
        }

        public async Task Handle(AddSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetAsync(c => c.Id == request.CategoryId);
            if(category is null)
                throw new BusinessException("دسته بندی مورد نظر وجود ندارد.");

            if (await _readService.ExistsByNameAsync(request.Name))
                throw new BusinessException("نام دسته بندی تکراری است.");

            var subCategory = _factory.Create(request.Name, request.Price, category);

            await _repository.AddAsync(subCategory);
        }
    }
}
