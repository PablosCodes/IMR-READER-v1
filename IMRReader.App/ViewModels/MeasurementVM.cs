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

        private MeasurementDataVM? _measurementDataVM;
        public MeasurementDataVM? MeasurementDataVM
        {
            get
            {
                return _measurementDataVM;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _measurementDataVM, value);
                this.RaisePropertyChanged(nameof(MeasurementDataVM.XData));
                this.RaisePropertyChanged(nameof(MeasurementDataVM.YData));
            }
        }
        public MeasurementVM() { }
    }
}
