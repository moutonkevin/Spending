using System.Threading.Tasks;
using Spending.Database.Entities;

namespace Spending.Api.Services
{
    public interface ITransactionCategoryService
    {
        Task CategorizeAndSaveTransactionAsync(Transaction transaction, string descriptionContent, int categoryId, int userId);
    }
}