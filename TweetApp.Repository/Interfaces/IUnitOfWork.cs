namespace TweetApp.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        ITweetRepository Tweet { get; }
        Task Save();
    }
}
