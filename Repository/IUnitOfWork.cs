namespace Repository
{
    public interface IUnitOfWork
    {
        IProductRepository Product { get; }


        void Save();
    }
}
