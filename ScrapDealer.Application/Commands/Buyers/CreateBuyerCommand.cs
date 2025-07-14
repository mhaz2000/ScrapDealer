using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.ValueObjects.Profiles;
using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Buyers
{
    public record CreateBuyerCommand(string FirstName, string LastName, string NationalCode, string City, string Province,
            string PostalCode, string AddressDescription, string Email, Gender Gender, Guid UserId) : ICommand;

}
