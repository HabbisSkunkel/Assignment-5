using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicShop.Models
{
    public class OnlineUser
    {
        [Key]
        public required int UserId { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
