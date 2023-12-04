using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicShop.Models
{
    public class ShoppingCart
    {
        [Key]
        public required int RecordId { get; set; }

        [Display(Name = "Cart")]
        public required int CartId { get; set; }

        [ForeignKey("CartId")]
        public virtual OnlineUser? OnlineUser { get; set; }

        [Display(Name = "Song")]
        public required int SongId { get; set; }

        [ForeignKey("SongId")]
        public virtual Song? Song { get; set; }

        public int Count { get; set; }
    }
}
