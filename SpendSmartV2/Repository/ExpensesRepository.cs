using Microsoft.EntityFrameworkCore;
using SpendSmartV2.Data;
using SpendSmartV2.Interface;
using SpendSmartV2.Models;


namespace SpendSmartV2.Repository
{
    public class ExpensesRepository : GenericRepository<Expenses>, IExpensesRepository
    {
        public ExpensesRepository(SpendSmartDbContext context) : base(context)
        {
        }

        public override async Task<bool> AddEntity(Expenses model)
        {
            try 
            {
                await DbSet.AddAsync(model);
                return true;
            }
            catch(Exception e) 
            {
                throw e;
            }

            
        }

        public override  Task<Expenses> GetAsync(string userId, int id)
        {
            return DbSet.FirstOrDefaultAsync(e => e.UserId == userId && e.Id == id);
        }

        public override async Task<bool> UpdateEntity(Expenses model)
        {
            try
            {
                var existingExpense = await DbSet.FirstOrDefaultAsync(e => e.Id == model.Id);
                if (existingExpense != null)
                {
                    existingExpense.Value = model.Value;
                    existingExpense.Discerption = model.Discerption;
                    return true;
                }
                else 
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override async Task<bool> DeleteEntity(int id)
        {
            try
            {
                var existingExpense = await DbSet.FirstOrDefaultAsync(e => e.Id == id);
                if (existingExpense != null)
                {
                    DbSet.Remove(existingExpense);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override async Task<List<Expenses>> GetAllAsync(string userId)
        {
            return await DbSet.Where(e => e.UserId == userId).ToListAsync();
        }
    }
}
