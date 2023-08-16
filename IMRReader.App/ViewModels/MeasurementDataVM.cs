namespace IMRReader.Application.ViewModels
{
    public record MeasurementDataVM
    {
        public required double[] XData { get; set; }

        public required double[] YData { get; set; }
    }
}
