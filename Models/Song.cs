using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicShop.Models
{
    public class Song
    {
        public required int SongId { get; set; }

        [Display(Name = "Artist")]
        public int ArtistId { get; set; }

        [ForeignKey("ArtistId")]
        public virtual Artist? Artist { get; set; }

        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        [ForeignKey("GenreId")]
        public virtual Genre? Genre { get; set; }

        public required string Title { get; set; }
        public required string Type { get; set; }
        public required string Format { get; set; }
        [Range(0, 100)]
        public required decimal Price { get; set; }
    }
}
