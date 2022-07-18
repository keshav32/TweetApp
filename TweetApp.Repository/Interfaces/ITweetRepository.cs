using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetApp.Repository.Entities;

namespace TweetApp.Repository.Interfaces
{
    public interface ITweetRepository:IRepository<Tweet>
    {
        void Update(Tweet tweet);
    }
}
