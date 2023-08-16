using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.FrameworkDialogs;
using IMRReader.Application.Abstract;
using IMRReader.Application.Common;
using IMRReader.Application.ViewModels;
using IMRReader.Common;
using IMRReader.Domain.Abstract;
using ReactiveUI;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace IMRReader.DataContextes
{
    public class MainWindowVM : ViewModelBase
    {
        private readonly IDialogService? _dialogService;
        private readonly IMeasurementDataService _measurementDataService;
        private readonly ITargetInfoLoader? _targetInfoLoader;

        public ApperanceMenuVM ApperanceMenuVM { get; set; }

        private ObservableCollection<TargetVM> _targets;
        public ObservableCollection<TargetVM> Targets
        {
            get => _targets;
            private set => this.RaiseAndSetIfChanged(ref _targets, value);
        }

        private TargetVM? _selectedTarget;
        public TargetVM? SelectedTarget
        {
            get => _selectedTarget;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedTarget, value);
                this.RaisePropertyChanged(nameof(SelectedTarget.Measurements));
                this.RaisePropertyChanged(nameof(SelectedTarget.Name));
            }
        }

        private MeasurementVM _selectedMeasurement;
        public MeasurementVM SelectedMeasurement
        {
            get { return _selectedMeasurement; }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedMeasurement, value);
                this.RaisePropertyChanged(nameof(SelectedMeasurement.Comment));
                this.RaisePropertyChanged(nameof(SelectedMeasurement.MeasurementDataVM.XData));
                this.RaisePropertyChanged(nameof(SelectedMeasurement.MeasurementDataVM.YData));
            }
        }

        private bool _shouldMeasurementBePresented = true;
        public bool ShouldMeasurementBePresented
        {
            get => _shouldMeasurementBePresented;
            set
            {
                this.RaiseAndSetIfChanged(ref _shouldMeasurementBePresented, value);
            }
        }

        public ReactiveCommand<Unit, Task> OpenFileDialogCommand { get; private set; }

        public ReactiveCommand<TargetVM, Task> TargetSelectedCommand { get; private set; }

        public ReactiveCommand<MeasurementVM, Task> MeasurementSelectedCommand { get; private set; }

        public ReactiveCommand<Unit, Unit> ExitAppCommand { get; private set; }

        public MainWindowVM(IDialogService dialogService, ITargetInfoLoader targetInfoLoader, IMeasurementDataService measurementDataService) : this()
        {
            _targetInfoLoader = targetInfoLoader;
            _dialogService = dialogService;
            _measurementDataService = measurementDataService;
        }

        /// <summary>
        /// For axaml use only
        /// </summary>
        public MainWindowVM()
        {
            _targets = new ObservableCollection<TargetVM>();
            ApperanceMenuVM = new ApperanceMenuVM();
            InitCommands();
            InitObservators();
#if DEBUG
            SeedData();
#endif
        }

        private void InitCommands()
        {
            OpenFileDialogCommand = ReactiveCommand.Create(OpenFileDialog);
            TargetSelectedCommand = ReactiveCommand.Create<TargetVM, Task>(HandleTargetSelection);
            MeasurementSelectedCommand = ReactiveCommand.Create<MeasurementVM, Task>(HandleMeasurementSelection);
            ExitAppCommand = ReactiveCommand.Create(ExitApp);
        }

        private void InitObservators()
        {
            this.WhenAnyValue(x => x.SelectedTarget).InvokeCommand(TargetSelectedCommand!);
            this.WhenAnyValue(x => x.SelectedMeasurement).InvokeCommand(MeasurementSelectedCommand!);
        }

        private async Task OpenFileDialog()
        {
            OpenFileDialogSettings openFileDialogSettings = new()
            {
                AllowMultiple = false,
                Filters = new List<FileFilter>()
                {
                    new("Database file", "db")
                },
                Title = "Select database file"
            };
            var dialog = await _dialogService.ShowOpenFileDialogAsync(this, openFileDialogSettings);

            // TODO: Add selected path validation
            string filePath = dialog.Path.AbsoluteUri;
            _targetInfoLoader.OpenFile(filePath);

            var loadTask = LoadTargetsFromFile();
        }

        private async Task LoadTargetsFromFile()
        {
            Targets.Clear();
            var targets = _targetInfoLoader.GetTargets();

            await foreach (var target in targets)
            {
                Targets.Add(target.GetVM());
            }
        }

        private async Task HandleTargetSelection(TargetVM selectedTarget)
        {
            if (selectedTarget is not null)
            {
                await LoadMeasurements(selectedTarget);
            }
        }

        private async Task HandleMeasurementSelection(MeasurementVM selectedMeasurement)
        {
            if (selectedMeasurement is not null)
            {
                selectedMeasurement.MeasurementDataVM = (await _measurementDataService.LoadMeasurementInfo(selectedMeasurement)).GetVM();
            }
        }

        private async Task LoadMeasurements(TargetVM measurementsTarget)
        {
            if (measurementsTarget is null)
                return;

            var measurements = _targetInfoLoader.GetMeasurementsForTarget(measurementsTarget.Id);

            await foreach (var measurement in measurements)
            {
                measurementsTarget.Measurements.Add(measurement.GetVM());
            }
        }

        private void ExitApp()
        {
            Environment.Exit(0);
        }

        private void SeedData()
        {
            Targets.Add(
                new()
                {
                    Id = 0,
                    Name = "Nieznany",
                    Measurements = new() {
                    new() { Id = 0, Date = DateTime.Now, Method = "2P", Results = "2.2 Ohm", Comment = "przekroczony" },
                    new() { Id = 1, Date = DateTime.Now, Method = "1P", Results = "1.2 Ohm" }
                }
                });
            Targets.Add(
                new()
                {
                    Id = 1,
                    Name = "skalowanie",
                    Measurements = new(){
                    new(){Id=0, Date=DateTime.Now, Method="3P", Results="2.2 Ohm", Comment="przekroczony" },
                }
                });
        }
    }
}