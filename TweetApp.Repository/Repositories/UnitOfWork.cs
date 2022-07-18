using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetApp.Repository.Contexts;
using TweetApp.Repository.Interfaces;

namespace TweetApp.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IUserRepository User { get; private set; }

        public ITweetRepository Tweet { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            User = new UserRepository(_db);
            Tweet = new TweetRepository(_db);
        }
        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
