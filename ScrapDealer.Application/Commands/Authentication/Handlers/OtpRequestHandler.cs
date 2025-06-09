using ScrapDealer.Application.Services;
using ScrapDealer.Domain.ValueObjects.Users;
using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Authentication.Handlers
{
    public class OtpRequestHandler : ICommandHandler<OtpRequestCommand>
    {
        private readonly IMemoryCacheService _cacheService;

        public OtpRequestHandler(IMemoryCacheService cacheService)
            => _cacheService = cacheService;

        public async Task Handle(OtpRequestCommand request, CancellationToken cancellationToken)
        {
            var random = new Random();
            var phone = Phone.Create(request.Phone); //For normalization

            var otpCode = random.Next(100000, 999999);
            _cacheService.Set(phone, otpCode, TimeSpan.FromMinutes(2));

            //Sms

            await Task.CompletedTask;
        }
    }
}
