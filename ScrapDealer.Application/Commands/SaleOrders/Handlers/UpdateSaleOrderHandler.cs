using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.SaleOrders.Handlers
{
    internal class UpdateSaleOrderHandler : ICommandHandler<UpdateSaleOrderCommand>
    {
        private readonly ISaleOrderFactory _factory;
        private readonly ISaleOrderRepository _saleOrderRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ISaleOrderReadService _readService;

        public UpdateSaleOrderHandler(ISaleOrderFactory factory, ISaleOrderRepository repository, ISaleOrderReadService readService,
            ISubCategoryRepository subCategoryRepository)
        {
            _factory = factory;
            _saleOrderRepository = repository;
            _readService = readService;
            _subCategoryRepository = subCategoryRepository;
        }

        public async Task Handle(UpdateSaleOrderCommand request, CancellationToken cancellationToken)
        {
            var saleOrder = await _saleOrderRepository.GetAsync(c => c.Id == request.Id, c => c.SubCategory);
            if (saleOrder is null)
                throw new BusinessException("سفارش مورد نظر یافت نشد.");

            if (request.SubCategoryId is not null && await _readService.ExistsBySubCategoryIdAsync(request.SubCategoryId.Value, request.Id))
                throw new BusinessException("قبلا در این دسته بندی سفارش ثبت شده است.");

            var category = await _subCategoryRepository.GetAsync(c => c.Id == request.SubCategoryId);
            if (request.SubCategoryId is not null && category is null)
                throw new BusinessException("دسته بندی مورد نظر یافت نشد.");

            saleOrder = _factory.Update(category, request.Address, request.Description, request.Type, request.images, saleOrder);

            await _saleOrderRepository.UpdateAsync(saleOrder);
        }
    }
}
