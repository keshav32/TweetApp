using TweetApp.Repository.Contexts;
using TweetApp.Repository.Interfaces;
using TweetApp.Repository.Entities;

namespace TweetApp.Repository.Repositories
{
    public class UserRepository : Repository<User>,IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(User user)
        {
            _db.Users.Update(user);
        }
    }
}
