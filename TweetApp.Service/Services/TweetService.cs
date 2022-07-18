using AutoMapper;
using TweetApp.Model.Dto;
using TweetApp.Repository.Entities;
using TweetApp.Repository.Interfaces;
using TweetApp.Service.Services.Interfaces;

namespace TweetApp.Service.Services
{
    public class TweetService : ITweetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TweetService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TweetDetailsDto>> GetAllTweets()
        {
            var tweetList = await _unitOfWork.Tweet.GetAllAsync(includeProperties: "User");

            var tweets = new List<TweetDetailsDto>();

            foreach (var tweet in tweetList)
            {
                tweets.Add(_mapper.Map<TweetDetailsDto>(tweet));
            }

            return tweets;
        }

        public async Task<IEnumerable<TweetDetailsDto>> GetTweetsByUsername(string username)
        {
            var user = await _unitOfWork.User.GetFirstOrDefaultAsync(x => x.Email == username);

            var tweetList = await _unitOfWork.Tweet.GetAllAsync(x => x.UserId == user.LoginId, includeProperties: "User");

            var tweets = new List<TweetDetailsDto>();

            foreach (var tweet in tweetList)
            {
                tweets.Add(_mapper.Map<TweetDetailsDto>(tweet));
            }

            return tweets;
        }

        public async Task<TweetDetailsDto> PostTweet(TweetCreateDto tweetDto)
        {
            var tweet = _mapper.Map<Tweet>(tweetDto);
            await _unitOfWork.Tweet.AddAsync(tweet);
            await _unitOfWork.Save();
            return _mapper.Map<TweetDetailsDto>(tweet);
        }
    }
}
