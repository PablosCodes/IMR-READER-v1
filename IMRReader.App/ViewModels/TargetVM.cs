using System.Collections.ObjectModel;

namespace IMRReader.Application.ViewModels
{
    public record TargetVM
    {
        public required int Id { get; set; }

        public ObservableCollection<MeasurementVM>? Measurements { get; set; }

        public required string Name { get; set; }
    }
}
