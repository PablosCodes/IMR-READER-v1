using HanumanInstitute.MvvmDialogs;
using HanumanInstitute.MvvmDialogs.FrameworkDialogs;
using IMRReader.Application.Abstract;
using IMRReader.Application.Common;
using IMRReader.Application.ViewModels;
using IMRReader.Common;
using IMRReader.Domain.Abstract;
using ReactiveUI;
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
        private readonly MessageBuilder _messageBuilder;

        public IMessageBusService MessageBusService { get; private set; }

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
                if (_selectedTarget is not null)
                    _selectedTarget.Measurements?.Clear();
                this.RaiseAndSetIfChanged(ref _selectedTarget, value);
                this.RaisePropertyChanged(nameof(SelectedTarget.Measurements));
                this.RaisePropertyChanged(nameof(SelectedTarget.Name));
            }
        }

        private MeasurementVM? _selectedMeasurement;
        public MeasurementVM? SelectedMeasurement
        {
            get { return _selectedMeasurement; }
            set
            {
                if (_selectedMeasurement is not null && MeasurementMetricsVM is not null)
                {
                    Array.Clear(MeasurementMetricsVM.XData);
                    Array.Clear(MeasurementMetricsVM.YData);
                    //_selectedMeasurement.MeasurementDataVM = default;
                }

                this.RaiseAndSetIfChanged(ref _selectedMeasurement, value);
                this.RaisePropertyChanged(nameof(SelectedMeasurement.Comment));
                this.RaisePropertyChanged(nameof(ShouldShowMeasurementMetrics));
            }
        }

        private MeasurementMetricsVM? _measurementMetricsVM;
        public MeasurementMetricsVM? MeasurementMetricsVM
        {
            get
            {
                return _measurementMetricsVM;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _measurementMetricsVM, value);
                this.RaisePropertyChanged(nameof(MeasurementMetricsVM.XData));
                this.RaisePropertyChanged(nameof(MeasurementMetricsVM.YData));
            }
        }

        public bool ShouldShowMeasurementMetrics
        {
            get { return SelectedMeasurement is not null; }
        }


        public ReactiveCommand<Unit, Task> OpenFileDialogCommand { get; private set; }

        public ReactiveCommand<TargetVM, Task> TargetSelectedCommand { get; private set; }

        public ReactiveCommand<MeasurementVM, Task> MeasurementSelectedCommand { get; private set; }

        public ReactiveCommand<Unit, Unit> ExitAppCommand { get; private set; }

        public MainWindowVM(IDialogService dialogService, ITargetInfoLoader targetInfoLoader, IMeasurementDataService measurementDataService, IMessageBusService messageBusService) : this()
        {
            _targetInfoLoader = targetInfoLoader;
            _dialogService = dialogService;
            _measurementDataService = measurementDataService;
            MessageBusService = messageBusService;

            _messageBuilder = new MessageBuilder();
            _targets = new ObservableCollection<TargetVM>();
            ApperanceMenuVM = new ApperanceMenuVM();
            MeasurementMetricsVM = new MeasurementMetricsVM();

            InitCommands();
            InitObservators();
        }

        /// <summary>
        /// For axaml use only
        /// </summary>
        public MainWindowVM()
        {
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
            if (dialog?.Path is not null)
            {
                string filePath = dialog.Path.AbsoluteUri;
                _targetInfoLoader.OpenFile(filePath);

                var loadTask = LoadTargetsFromFile();
            }
        }

        private async Task LoadTargetsFromFile()
        {
            string statusLoadingMessage = _messageBuilder.GetLoadingTargetsMsg(_targetInfoLoader?.FilePath);
            EnqueueMessage(statusLoadingMessage);

            Targets.Clear();
            var targets = _targetInfoLoader.GetTargets();

            await foreach (var target in targets)
            {
                Targets.Add(target.GetVM());
            }
            await targets.LastAsync();

            int targetsCount = Targets.Count();

            string statusLoadedMessage = _messageBuilder.GetLoadedTargetsMsg(_targetInfoLoader?.FilePath, targetsCount);
            EnqueueMessage(statusLoadedMessage, shouldStayIfLast: false, force: true);
        }

        private async Task HandleTargetSelection(TargetVM selectedTarget)
        {
            if (selectedTarget is not null)
            {
                await LoadMeasurements(selectedTarget);
            }
        }

        // TODO: Add file support
        private async Task HandleMeasurementSelection(MeasurementVM selectedMeasurement)
        {
            if (selectedMeasurement is not null)
            {
                string statusLoadingMessage = _messageBuilder.GetLoadingMeasurementMetricsMsg("HARDKODOWANE");
                EnqueueMessage(statusLoadingMessage);

                MeasurementMetricsVM = (await _measurementDataService.LoadMeasurementInfo(selectedMeasurement)).GetVM();

                string statusLoadedMessage = _messageBuilder.GetLoadedMeasurementMetricsMsg("HARDKODOWANE");
                EnqueueMessage(statusLoadedMessage, shouldStayIfLast: false, force: true);
            }
        }

        private async Task LoadMeasurements(TargetVM measurementsTarget)
        {
            if (measurementsTarget is null)
                return;

            string statusLoadingMessage = _messageBuilder.GetLoadingMeasurementsMsg(_targetInfoLoader?.FilePath);
            EnqueueMessage(statusLoadingMessage);

            var measurements = _targetInfoLoader.GetMeasurementsForTarget(measurementsTarget.Id);

            await foreach (var measurement in measurements)
            {
                measurementsTarget.Measurements.Add(measurement.GetVM());
            }

            await measurements.LastAsync();

            int loadedCount = measurementsTarget.Measurements.Count;
            string statusLoadedMessage = _messageBuilder.GetLoadedMeasurementsMsg(_targetInfoLoader?.FilePath, loadedCount);
            EnqueueMessage(statusLoadedMessage, shouldStayIfLast: false, force: true);
        }

        private void ExitApp()
        {
            Environment.Exit(0);
        }

        private void EnqueueMessage(string newMessage, int minDisplayedTime = 500, int maxDisplayedTime = 5000, bool shouldStayIfLast = true, bool force = false)
        {
            MessageVM messageToEnqueue = new()
            {
                Content = newMessage,
                MinimumDisplayTime = minDisplayedTime,
                MaximumDisplayTime = maxDisplayedTime,
                ShouldStay = shouldStayIfLast
            };

            if (force)
            {
                MessageBusService.ForceEnqueue(messageToEnqueue);
            }
            else
            {
                MessageBusService.Enqueue(messageToEnqueue);
            }
        }
    }
}