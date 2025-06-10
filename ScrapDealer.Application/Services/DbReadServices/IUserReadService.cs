using ScrapDealer.Domain.Entities;

namespace ScrapDealer.Application.Services.DbReadServices
{
    public interface IUserReadService
    {
        Task<bool> ExistsByUserNameAsync(string username);
        Task<Guid?> GetByPhoneAsync(string phone);
        Task<Guid?> ValidateUserCredentialByUsernameAsync(string username, string password);
        Task<bool> ValidateUserCredentialByUserIdAsync(Guid id, string password);
    }
}
