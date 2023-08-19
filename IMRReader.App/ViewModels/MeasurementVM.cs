using ReactiveUI;

namespace IMRReader.Application.ViewModels
{
    public class MeasurementVM : ReactiveObject
    {
        public required int Id { get; set; }
        public required DateTime Date { get; set; }
        public required string Method { get; set; }
        public required string Results { get; set; }
        public string? Comment { get; set; }
        public MeasurementVM() { }
    }
}
