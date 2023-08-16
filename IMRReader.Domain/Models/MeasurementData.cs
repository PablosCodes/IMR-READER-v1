namespace IMRReader.Domain.Models
{
    public record MeasurementData
    {
        public required double[] XData { get; set; }
        public required double[] YData { get; set; }
    }
}
