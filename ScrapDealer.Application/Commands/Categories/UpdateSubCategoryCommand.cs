using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Categories
{
    public record UpdateSubCategoryCommand(Guid Id, string Name, decimal Price): ICommand;
}
