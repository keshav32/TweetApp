using System.ComponentModel.DataAnnotations;
#nullable disable

namespace TweetApp.Model.Dto
{
    public class TweetCreateDto
    {
        [MaxLength(50)]
        public string Tag { get; set; }

        [MaxLength(144)]
        [Required]
        public string Subject { get; set; }

        public int UserId { get; set; }

    }
}
