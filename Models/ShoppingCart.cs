using System.ComponentModel.DataAnnotations;

namespace MusicShop.Models
{
    public class ShoppingCart
    {
        [Key]
        public required int RecordId { get; set; }
        public required string CartId { get; set; }
        public required int SongId { get; set; }
        public int Count { get; set; }
    }
}
