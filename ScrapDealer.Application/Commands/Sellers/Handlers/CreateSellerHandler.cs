using ScrapDealer.Application.Commands.Buyers;
using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapDealer.Application.Commands.Sellers.Handlers
{
    internal class CreateSellerHandler : ICommandHandler<CreateSellerCommand>
    {
        private const string sellerRoleName = "Seller";

        private readonly ISellerFactory _factory;
        private readonly ISellerRepository _repository;
        private readonly ISellerReadService _readService;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        public CreateSellerHandler(ISellerFactory factory, ISellerRepository repository, ISellerReadService readService,
            IUserRepository userRepository, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository)
        {
            _factory = factory;
            _repository = repository;
            _readService = readService;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task Handle(CreateSellerCommand request, CancellationToken cancellationToken)
        {
            if (await _readService.ExistsByUserIdAsync(request.UserId))
                return;

            var user = await _userRepository.GetAsync(u => u.Id == request.UserId);
            if (user is null)
                throw new BusinessException("کاربر یافت نشد.");

            var sellerRole = await _roleRepository.GetAsync(r => r.Name == sellerRoleName);

            var seller = _factory.Create(request.FirstName, request.LastName, request.NationalCode, request.City, request.Province,
                request.PostalCode, request.AddressDescription, request.Email, request.Gender, request.PersonType, request.UserId);

            var sellerUserRole = user.AddRole(sellerRole!);

            await _userRoleRepository.AddAsync(sellerUserRole);

            await _repository.AddAsync(seller);
        }
    }
}
