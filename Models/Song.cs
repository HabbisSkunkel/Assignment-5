namespace MusicShop.Models
{
    public class Song
    {
        public required int ID { get; set; }
        public required string Name { get; set; }
        public required string Artist { get; set; }
        public required string Format { get; set; }
        public required string Type { get; set; }
        public required string Genre { get; set; }
        public required int Price { get; set; }

    }
}
