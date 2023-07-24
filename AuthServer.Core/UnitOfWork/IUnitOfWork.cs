namespace AuthServer.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        //Tek seferde işlem gerçekleşsin
        Task CommitAsync();//asenkron
        void Commit();  //senkronG
    }
}
