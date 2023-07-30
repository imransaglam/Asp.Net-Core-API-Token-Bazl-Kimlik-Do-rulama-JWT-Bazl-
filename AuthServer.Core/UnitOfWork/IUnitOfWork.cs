namespace AuthServer.Core.UnitOfWork
{
    //Tek seferde işlem gerçekleşsin
    public interface IUnitOfWork
    {
        Task CommmitAsync();//asenkron

        void Commit();//senkron
    }
}
