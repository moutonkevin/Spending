using System.Threading.Tasks;
using Spending.Database.Entities;

namespace Spending.Api.DataAccess
{
    public interface ITransactionCategoryDatabaseDataAccess
    {
        Task<bool> SaveTransactionCategory(Transaction transaction, string descriptionContent, int categoryId, int userId);
    }
}