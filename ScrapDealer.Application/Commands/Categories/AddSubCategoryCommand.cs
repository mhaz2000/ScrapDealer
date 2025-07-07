using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Categories
{
    public record AddSubCategoryCommand(string Name, decimal Price, Guid CategoryId) : ICommand;
}
