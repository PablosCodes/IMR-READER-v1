using Avalonia.Themes.Fluent;
using IMRReader.Application.Managers;
using IMRReader.Managers;
using IMRReader.Common;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using IMRReader.Application.ViewModels;

namespace IMRReader.DataContextes
{
    public class MainWindowVM : ViewModelBase
    {
        public ApperanceMenuVM ApperanceMenuVM { get; set; }

        private List<Target>? _targets;
        public List<Target>? Targets
        {
            get => _targets;
            private set => this.RaiseAndSetIfChanged(ref _targets, value);
        }

        private Target? _selectedTarget;
        public Target? SelectedTarget
        {
            get => _selectedTarget;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedTarget, value);
                this.RaisePropertyChanged(nameof(SelectedTarget.Measurements));
                this.RaisePropertyChanged(nameof(SelectedTarget.Name));
            }
        }

        // TODO: Add measurement selection & measurement's display logic
        private bool _shouldMeasurementBePresented = true;
        public bool ShouldMeasurementBePresented
        {
            get => _shouldMeasurementBePresented;
            set
            {
                this.RaiseAndSetIfChanged(ref _shouldMeasurementBePresented, value);
            }
        }

        public MainWindowVM()
        {
            ApperanceMenuVM = new ApperanceMenuVM();

            SeedData();
        }
        
        private void SeedData()
        {
            Targets = new() {
                new(0,"Nieznany"){Measurements = new(){
                    new(){Id=0, Date=DateTime.Now, Method="2P", Results="2.2 Ohm", Comment="przekroczony" },
                    new(){Id=1, Date=DateTime.Now, Method="1P", Results="1.2 Ohm" }
                }},
                new(1,"skalowanie"){Measurements = new(){
                    new(){Id=0, Date=DateTime.Now, Method="3P", Results="2.2 Ohm", Comment="przekroczony" },
                }},
            };
        }
    }
}