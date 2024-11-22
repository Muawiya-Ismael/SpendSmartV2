using SpendSmartV2.Models;

namespace SpendSmartV2.Interface
{
    public interface IExpensesServices
    {
        Task<List<Expenses>> GetAllExpenses(string userId);
        Task<Expenses> GetExpense(string userId, int id);
        Task<bool> AddExpense(Expenses model);
        Task<bool> UpdateExpense(Expenses model);
        Task<bool> DeleteExpense(int id);
        Task Commit();
    }
}
