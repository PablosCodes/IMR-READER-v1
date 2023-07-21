using Avalonia.Themes.Fluent;
using IMReader.Application.Managers;
using IMReader.Application.ViewModels;
using IMRReader.Application.Managers;
using IMRReader.Common;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;

namespace IMRReader.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
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
            }
        }

        public ReactiveCommand<string, Unit> SwitchThemeCommand { get; }

        public ReactiveCommand<string, Unit> SwitchDensityCommand { get; }

        public MainWindowVM()
        {
            SwitchThemeCommand = ReactiveCommand.Create<string>(SwitchTheme);
            SwitchDensityCommand = ReactiveCommand.Create<string>(SwitchDensity);

            SeedData();

        }

        private void SwitchTheme(string? themeText)
        {
            if (Enum.TryParse(themeText, true, out MyTheme theme))
            {
                AppearanceManager.SetTheme(App.Current, theme);
                SettingsManager.SaveThemeSetting(theme);
            }
        }

        private void SwitchDensity(string? densityText)
        {
            if (Enum.TryParse(densityText, true, out DensityStyle density))
            {
                AppearanceManager.SetDensity(App.Current, density);
                SettingsManager.SaveDensitySetting(density);
            }
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