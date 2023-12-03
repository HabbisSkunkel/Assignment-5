using System.ComponentModel.DataAnnotations;

namespace MusicShop.Models
{
    public class OnlineUser
    {
        [Key]
        public required int UserId { get; set; }
        public required int CartId { get; set; }
        public required string UserName { get; set; }
    }
}
