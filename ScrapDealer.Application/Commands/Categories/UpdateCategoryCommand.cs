using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Categories
{
    public record UpdateCategoryCommand(Guid Id, string Name): ICommand;
}
