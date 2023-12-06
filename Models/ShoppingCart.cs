using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicShop.Models
{
    public class ShoppingCart
    {
        [Key]
        public int RecordId { get; set; }

        [Display(Name = "User")]
        public required int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual OnlineUser? OnlineUser { get; set; }

        [Display(Name = "Song")]
        public required int SongId { get; set; }

        [ForeignKey("SongId")]
        public virtual Song? Song { get; set; }

        public int Count { get; set; }
    }
}
