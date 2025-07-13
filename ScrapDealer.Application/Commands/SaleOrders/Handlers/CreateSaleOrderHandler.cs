using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.SaleOrders.Handlers
{
    internal class CreateSaleOrderHandler : ICommandHandler<CreateSaleOrderCommand>
    {
        private readonly ISaleOrderFactory _factory;
        private readonly ISaleOrderRepository _saleOrderRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ISellerRepository _sellerRepository;
        private readonly ISaleOrderReadService _readService;

        public CreateSaleOrderHandler(ISaleOrderFactory factory, ISaleOrderRepository repository, ISaleOrderReadService readService,
            ISubCategoryRepository subCategoryRepository, ISellerRepository sellerRepository)
        {
            _factory = factory;
            _saleOrderRepository = repository;
            _readService = readService;
            _sellerRepository = sellerRepository;
            _subCategoryRepository = subCategoryRepository;
        }

        public async Task Handle(CreateSaleOrderCommand request, CancellationToken cancellationToken)
        {
            if (request.SubCategoryId is not null && await _readService.ExistsBySubCategoryIdAsync(request.SubCategoryId.Value))
                throw new BusinessException("قبلا در این دسته بندی سفارش ثبت شده است.");

            var category = await _subCategoryRepository.GetAsync(c => c.Id == request.SubCategoryId);
            if (request.SubCategoryId is not null && category is null)
                throw new BusinessException("دسته بندی مورد نظر یافت نشد.");

            var seller = await _sellerRepository.GetAsync(c => c.UserId == request.UserId);
            if(seller is null)
                throw new BusinessException("فروشنده یافت نشد.");

            if(!seller.Verified)
                throw new BusinessException("ابتدا فرایند احراز هویت خود را تکمیل کنید.");

            var saleOrder = _factory.Create(category, seller, request.Address, request.Description, request.Type, request.images);

            await _saleOrderRepository.AddAsync(saleOrder);
        }
    }
}
