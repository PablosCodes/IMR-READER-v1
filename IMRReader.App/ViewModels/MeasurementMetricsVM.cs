namespace IMRReader.Application.ViewModels
{
    public class MeasurementMetricsVM
    {
        public double[] XData { get; set; }

        public double[] YData { get; set; }

        public MeasurementMetricsVM()
        {
            XData = Array.Empty<double>();
            YData = Array.Empty<double>();
        }
    }
}
