using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TwitterAPI.Entities
{
    public class Follow
    {
        [Key]
        [StringLength(25)]
        public int FollowId { get; set; }
        [ForeignKey("Follower")]
        [StringLength(25)]
        public string UserId { get; set; }
        [ForeignKey("Following")]
        [StringLength(25)]
        public string FollowingId { get; set; }

        //Navigation Property
        [JsonIgnore]
        public User? Follower { get; set; }
        [JsonIgnore]
        public User? Following { get; set; }
    }
}
