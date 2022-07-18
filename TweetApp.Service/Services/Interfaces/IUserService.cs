using TweetApp.Model.Dto;
using TweetApp.Repository.Entities;

namespace TweetApp.Service.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDetailsDto>> GetAllUsers();
        Task<UserDetailsDto> Register(UserDto userDto);
        Task<bool> IsUniqueUser(string username);
        Task<User> Authenticate(string username,string password);
        Task<User> FindByUsername(string username);
        Task<bool> ResetPassword(string username,string password);
        void LogOut(string username);
    }
}
