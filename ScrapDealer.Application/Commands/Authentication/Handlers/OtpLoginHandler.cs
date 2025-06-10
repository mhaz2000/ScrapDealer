using Microsoft.Identity.Client;
using ScrapDealer.Application.DTO;
using ScrapDealer.Application.Services;
using ScrapDealer.Application.Services.DbReadServices;
using ScrapDealer.Domain.Factories.interfaces;
using ScrapDealer.Domain.Repositories;
using ScrapDealer.Domain.ValueObjects.Users;
using ScrapDealer.Shared.Abstractions.Commands;
using ScrapDealer.Shared.Abstractions.Exceptions;
using System.Security.Cryptography;

namespace ScrapDealer.Application.Commands.Authentication.Handlers
{
    internal class OtpLoginHandler : ICommandHandler<OtpLoginCommand, AuthenticationDto>
    {
        private readonly IMemoryCacheService _memoryCacheService;
        private readonly IRedisCacheService _redisCacheService;
        private readonly IUserReadService _userReadService;
        private readonly IUserFactory _userFactory;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        public OtpLoginHandler(IMemoryCacheService memoryCacheService, IRedisCacheService redisCacheService, IUserReadService userReadService, IUserFactory userFactory,
            IUserRepository userRepository, ITokenService tokenService)
        {
            _memoryCacheService = memoryCacheService;
            _userReadService = userReadService;
            _userFactory = userFactory;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _redisCacheService = redisCacheService;
        }
        public async Task<AuthenticationDto> Handle(OtpLoginCommand request, CancellationToken cancellationToken)
        {
            var phone = Phone.Create(request.Phone); //For normalization
            var userId = await _userReadService.GetByPhoneAsync(phone);

            if (_memoryCacheService.Get<string>(phone) != request.Code)
                throw new BusinessException("کد تایید اشتباه است.");

            if (userId is null)
            {
                var newUser = _userFactory.Create(request.Phone, request.Phone);

                await _userRepository.AddAsync(newUser);

                userId = newUser.Id;
            }

            var token = _tokenService.GenerateToken(userId.Value.ToString());
            await _redisCacheService.SetAsync<Guid>($"refreshToken:{token.refreshToken}", userId.Value, TimeSpan.FromDays(7));

            return new AuthenticationDto() { Token = token.token, RefreshToken = token.refreshToken };
        }
    }
}