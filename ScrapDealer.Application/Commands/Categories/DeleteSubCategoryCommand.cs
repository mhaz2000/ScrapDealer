using ScrapDealer.Shared.Abstractions.Commands;

namespace ScrapDealer.Application.Commands.Categories
{
    internal record DeleteSubCategoryCommand(Guid Id) : ICommand;
}
