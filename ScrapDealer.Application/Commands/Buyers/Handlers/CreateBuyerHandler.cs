using ScrapDealer.Application.Commands.Sellers;
using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;

namespace ScrapDealer.Application.Commands.Buyers.Handlers
{
    internal class CreateBuyerHandler : ICommandHandler<CreateBuyerCommand>
    {
        private const string buyerRoleName = "Buyer";

        private readonly IBuyerFactory _factory;
        private readonly IBuyerRepository _repository;
        private readonly IBuyerReadService _readService;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        public CreateBuyerHandler(IBuyerFactory factory, IBuyerRepository repository, IBuyerReadService readService,
            IUserRepository userRepository, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository)
        {
            _factory = factory;
            _repository = repository;
            _readService = readService;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task Handle(CreateBuyerCommand request, CancellationToken cancellationToken)
        {
            if (await _readService.ExistsByUserIdAsync(request.UserId))
                return;

            var user = await _userRepository.GetAsync(u => u.Id == request.UserId);
            if (user is null)
                throw new BusinessException("کاربر یافت نشد.");

            var buyerRole = await _roleRepository.GetAsync(r => r.Name == buyerRoleName);

            var buyer = _factory.Create(request.FirstName, request.LastName, request.NationalCode, request.City, request.Province,
                request.PostalCode, request.AddressDescription, request.Email, request.Gender, request.UserId);

            var buyerUserRole = user.AddRole(buyerRole!);

            await _userRoleRepository.AddAsync(buyerUserRole);

            await _repository.AddAsync(buyer);
        }
    }
}
