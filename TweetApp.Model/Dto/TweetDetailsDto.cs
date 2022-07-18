using System.ComponentModel.DataAnnotations;

#nullable disable
namespace TweetApp.Model.Dto
{
    public class TweetDetailsDto
    {

        [MaxLength(50)]
        public string Tag { get; set; }

        [MaxLength(144)]
        public string Subject { get; set; }

        public int UserId { get; set; }
        public UserDetailsDto User { get; set; }
    }
}
