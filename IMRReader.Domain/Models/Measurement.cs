namespace IMRReader.Domain.Models
{
    public record Measurement
    {
        public required int Id { get; set; }
        public required DateTime Date { get; set; }
        public required string Method { get; set; }
        public required string Results { get; set; }
        public string? Comment { get; set; }
    }
}
