
using Microsoft.Extensions.Configuration;
using TweetApp.Model.Dto;
using TweetApp.Repository.Interfaces;
using TweetApp.Service.Services.Interfaces;
#nullable disable
namespace TweetApp.ConsoleApp
{
    public class TweetApp
    {
        private readonly IServices _services;
        public TweetApp(IServices services)
        {
            _services = services;
        }

        public async Task<object> ViewAllUsers()
        {
            var user = await _services.UserService.GetAllUsers();
            return user;
        }


        public async Task<object> Login(string username, string password)
        {
            var user = await _services.UserService.Authenticate(username, password);

            return user;
        }

        public async Task<object> ViewAllTweets()
        {
            var tweets = await _services.TweetService.GetAllTweets();
            return tweets;
        }

        public async Task<IEnumerable<TweetDetailsDto>> ViewMyTweets(string username)
        {
            var tweets = await _services.TweetService.GetTweetsByUsername(username);
            return tweets;
        }

        public async Task<object> Register(UserDto userDto)
        {
            if (!await _services.UserService.IsUniqueUser(userDto.Email))
            {
                var user = await _services.UserService.Register(userDto);
                return user;
            }
            return null;
        }

        public async Task<TweetDetailsDto> PostATweet(TweetCreateDto tweetDto, string username)
        {
            var user = await _services.UserService.FindByUsername(username);
            tweetDto.UserId = user.LoginId;

            var tweet = await _services.TweetService.PostTweet(tweetDto);

            return tweet;
        }

        public async Task<bool> ResetPassword(string username, string password)
        {
            var status = await _services.UserService.ResetPassword(username, password);
            return status;
        }

        public void LogOut(string username)
        {
             _services.UserService.LogOut(username);
        }
    }
}
