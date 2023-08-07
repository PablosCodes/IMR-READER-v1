namespace IMRReader.Domain.Models
{
    public record Target
    {
        public required int Id { get; set; }

        public required string Name { get; set; }
    }
}
