using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicShop.Models
{
    public class ShoppingCart
    {
        [Key]
        public required int RecordId { get; set; }
        public required int CartId { get; set; }

        [Display(Name = "Song")]
        public required int SongId { get; set; }

        [ForeignKey("SongId")]
        public virtual Song? Song { get; set; }

        public int Count { get; set; }
    }
}
