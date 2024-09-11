using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TwitterAPI.Entities
{
    public class Likes
    {
        [Key]
        public int LikesId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        [ForeignKey("Tweet")]
        public int TweetId { get; set; }
        [JsonIgnore]
        public Tweet? Tweet { get; set; }
    }
}

