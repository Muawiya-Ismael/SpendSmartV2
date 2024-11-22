namespace SpendSmartV2.Interface
{
    public interface IUnitOfWork
    {
        IExpensesRepository expensesRepository { get; }

        Task CompleteAsync();

        void Dispose();
    }
}
