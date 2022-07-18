using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetApp.Service.Services.Interfaces
{
    public interface IServices
    {
        IUserService UserService { get; }
        ITweetService TweetService { get; }
    }
}
