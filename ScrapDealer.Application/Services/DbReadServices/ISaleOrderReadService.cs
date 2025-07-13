namespace ScrapDealer.Application.Services.DbReadServices
{
    public interface ISaleOrderReadService
    {
        Task<bool> ExistsBySubCategoryIdAsync(Guid id, Guid? excludedId = null);
    }
}
