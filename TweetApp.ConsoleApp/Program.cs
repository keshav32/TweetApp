using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TweetApp.Model.Dto;
using TweetApp.Repository.Contexts;
using TweetApp.Repository.Interfaces;
using TweetApp.Repository.Repositories;
using TweetApp.Service.Mapper;
using TweetApp.Service.Services;
using TweetApp.Service.Services.Interfaces;
#nullable disable
namespace TweetApp.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json").Build();

            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            serviceCollection.AddAutoMapper(typeof(Mappings));
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<IServices, Services>();
            serviceCollection.AddSingleton<TweetApp>();


            var serviceProvider = serviceCollection.BuildServiceProvider();
            var tweetApp = serviceProvider.GetService<TweetApp>();

            while (true)
            {
                Console.WriteLine("Welcome to tweet app...");
                Console.WriteLine("Press 1 to login");
                Console.WriteLine("Press 2 to register");
                Console.WriteLine("Press 3 to reset password");
                Console.WriteLine("Press 4 to exit app");
                Console.WriteLine("Enter choice:");
                string input;
                int option;

                while (true)
                {
                    try
                    {
                        input = Console.ReadLine();
                        option = Convert.ToInt32(input);
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Invalid input try again.\nError:"+ex.Message);
                    }
                }


                bool isLoggedIn = false;


                if (option == 1)//Login
                {
                    Console.WriteLine("Enter username:");
                    string username = Console.ReadLine();
                    Console.WriteLine("Enter password:");
                    string password = Console.ReadLine();

                    var user = tweetApp.Login(username, password).GetAwaiter().GetResult();
                    if (user == null)
                    {
                        Console.WriteLine("Username or password is incorrect");
                    }
                    else
                    {
                        isLoggedIn = true;
                        Console.WriteLine("Welcome " + username);
                    }

                    while (isLoggedIn)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Press 1 to post a tweet");
                        Console.WriteLine("Press 2 to view all tweets");
                        Console.WriteLine("Press 3 to view my tweets");
                        Console.WriteLine("Press 4 to view all users");
                        Console.WriteLine("Press 5 to reset password");
                        Console.WriteLine("Press 6 to Logout");

                        int choice = Convert.ToInt32(Console.ReadLine());

                        switch (choice)
                        {
                            case 1:

                                Console.WriteLine("Enter tag:");
                                string tag = Console.ReadLine();

                                Console.WriteLine("Enter body:");
                                string body = Console.ReadLine();

                                TweetCreateDto tweetDto = new TweetCreateDto()
                                {
                                    Tag = tag,
                                    Subject = body,
                                };

                                var post = tweetApp.PostATweet(tweetDto, username).GetAwaiter().GetResult();
                                if (post != null)
                                {
                                    Console.WriteLine("Tweet posted successfully");
                                }
                                break;

                            case 2:

                                var tweets = (List<TweetDetailsDto>)tweetApp.ViewAllTweets().GetAwaiter().GetResult();
                                Console.WriteLine("All Tweets: ");
                                Console.WriteLine();
                                foreach (var tweet in tweets)
                                {
                                    Console.WriteLine(tweet.User.FirstName + " " + tweet.User.LastName);
                                    Console.WriteLine("Tag " + tweet.Tag);
                                    Console.WriteLine("Body:" + tweet.Subject);
                                    Console.WriteLine();
                                }
                                break;

                            case 3:
                                var myTweets = (List<TweetDetailsDto>)tweetApp.ViewMyTweets(username).GetAwaiter().GetResult();
                                Console.WriteLine("All my tweets: ");
                                Console.WriteLine();
                                foreach (var tweet in myTweets)
                                {
                                    Console.WriteLine();
                                    //Console.WriteLine(tweet.User.FirstName + " " + tweet.User.LastName);
                                    Console.WriteLine("Tag " + tweet.Tag);
                                    Console.WriteLine("Body:" + tweet.Subject);
                                    Console.WriteLine();
                                }
                                break;

                            case 4:
                                List<UserDetailsDto> res = (List<UserDetailsDto>)tweetApp.ViewAllUsers().GetAwaiter().GetResult();

                                foreach (UserDetailsDto u in res)
                                {
                                    Console.WriteLine("Name: " + u.FirstName + " " + u.LastName);
                                    Console.WriteLine("Email: " + u.Email);
                                    Console.WriteLine("Contact number: " + u.ContactNumber);
                                    Console.WriteLine();
                                }

                                break;

                            case 5:
                                //Console.WriteLine("Enter userna");
                                //username = Console.ReadLine();

                                Console.WriteLine("Enter new password");
                                password = Console.ReadLine();

                                var status = tweetApp.ResetPassword(username, password).GetAwaiter().GetResult();

                                if (status)
                                    Console.WriteLine("password reset successful");
                                else
                                    Console.WriteLine("password reset failed, try again");
                                break;

                            case 6:
                                isLoggedIn = false;
                                tweetApp.LogOut(username);
                                Console.WriteLine("Logged out successfully");
                                Console.WriteLine();
                                break;
                            default:
                                Console.WriteLine("Enter correct input");
                                break;
                        }
                    }
                }
                else if (option == 2)//Register
                {
                    Console.WriteLine("Enter first name:");
                    string fname = Console.ReadLine();

                    Console.WriteLine("Enter last name:");
                    string lname = Console.ReadLine();

                    Console.WriteLine("Enter email:");
                    string email = Console.ReadLine();

                    Console.WriteLine("Enter password:");
                    string password = Console.ReadLine();

                    Console.WriteLine("Confirm password:");
                    string confirmPassword = Console.ReadLine();

                    if (confirmPassword != password)
                    {
                        Console.WriteLine("Confirm password and password do not match try again");
                        break;
                    }

                    Console.WriteLine("Enter contact number:");
                    string number = Console.ReadLine();

                    UserDto user = new UserDto()
                    {
                        FirstName = fname,
                        LastName = lname,
                        Email = email,
                        Password = password,
                        ConfirmPassword = confirmPassword,
                        ContactNumber = number,
                    };

                    var userObj = (UserDto)tweetApp.Register(user).GetAwaiter().GetResult();
                    if (userObj != null)
                    {
                        Console.WriteLine($"User {userObj.Email} registered  successfully");
                    }
                    else
                    {
                        Console.WriteLine("Something went wrong. Please try again");
                    }

                }
                else if (option == 3)
                {
                    Console.WriteLine("Enter username:");
                    string username = Console.ReadLine();

                    Console.WriteLine("Enter new password");
                    string password = Console.ReadLine();

                    var status = tweetApp.ResetPassword(username, password).GetAwaiter().GetResult();

                    if (status)
                        Console.WriteLine("password reset successful");
                    else
                        Console.WriteLine("password reset failed, try again");
                }
                else if (option == 4)
                {
                    Console.WriteLine("Exiting app. Goodbye......");
                    return;
                }
                else
                {
                    Console.WriteLine("Please provide correct input");
                }
            }
        }
    }
}