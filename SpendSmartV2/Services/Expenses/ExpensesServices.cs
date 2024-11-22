using Microsoft.EntityFrameworkCore;
using SpendSmartV2.Interface;
using SpendSmartV2.Models;
using SpendSmartV2.Repository;

namespace SpendSmartV2.Services.Expenses
{
    public class ExpensesServices : IExpensesServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExpensesServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddExpense(Models.Expenses model)
        {
            return await _unitOfWork.expensesRepository.AddEntity(model);
        }

        public async Task<bool> DeleteExpense(int id)
        {
            return await _unitOfWork.expensesRepository.DeleteEntity(id);
        }

        public async Task<List<Models.Expenses>> GetAllExpenses(string userId)
        {
            return await _unitOfWork.expensesRepository.GetAllAsync(userId);
        }

        public async Task<Models.Expenses> GetExpense(string userId, int id)
        {
            return await _unitOfWork.expensesRepository.GetAsync(userId, id);
        }

        public async Task<bool> UpdateExpense(Models.Expenses model)
        {
            return await _unitOfWork.expensesRepository.UpdateEntity(model);
        }

        public async Task Commit()
        {
            await _unitOfWork.CompleteAsync();
        }
    }
}
