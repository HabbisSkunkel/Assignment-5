namespace MusicShop.Models
{
    public class Song
    {
        public required int SongId { get; set; }
        public required int ArtistId { get; set; }
        public required int GenreId { get; set; }
        public required string Title { get; set; }
        public required string Type { get; set; }
        public required string Format { get; set; }
        public required int Price { get; set; }
    }
}
