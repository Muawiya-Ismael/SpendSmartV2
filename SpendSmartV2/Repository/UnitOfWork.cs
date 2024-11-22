using SpendSmartV2.Data;
using SpendSmartV2.Interface;

namespace SpendSmartV2.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IExpensesRepository expensesRepository { get; private set; }

        private readonly SpendSmartDbContext _context;

        public UnitOfWork(SpendSmartDbContext context)
        {
            _context = context;
            expensesRepository = new ExpensesRepository(_context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose() 
        {
            _context.Dispose();
        }
    }   
}
