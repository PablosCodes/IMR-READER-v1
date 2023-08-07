using Avalonia.Themes.Fluent;
using IMRReader.Application.Common;
using IMRReader.Application.Managers;
using IMRReader.Common;
using IMRReader.Managers;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace IMRReader.DataContextes
{
    public class ApperanceMenuVM : ViewModelBase
    {
        private AppearanceManager _appearanceManager;

        public List<ThemeVM> Themes { get; private set; }

        private ThemeVM? _selectedTheme;
        public ThemeVM? SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedTheme, value);
                this.RaisePropertyChanged(nameof(IsThemeDark));
            }
        }

        public bool IsThemeDark => SelectedTheme is not null && SelectedTheme.Value == MyTheme.Dark;

        public List<DensityVM> Densities { get; private set; }

        private DensityVM? _selectedDensity;
        public DensityVM? SelectedDensity
        {
            get => _selectedDensity;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedDensity, value);
            }
        }

        public ReactiveCommand<ThemeVM, Unit> SwitchThemeCommand { get; private set; }

        public ReactiveCommand<DensityVM, Unit> SwitchDensityCommand { get; private set; }

        public ApperanceMenuVM()
        {
            _appearanceManager = new AppearanceManager();

            SwitchThemeCommand = ReactiveCommand.Create<ThemeVM>(SwitchTheme);
            SwitchDensityCommand = ReactiveCommand.Create<DensityVM>(SwitchDensity);

            Themes = GetAvailableThemeVMs()
            .ToList();
            Densities = GetAvailableDensitiesVMs()
                .ToList();

            this.WhenAnyValue(x => x.SelectedTheme).InvokeCommand(SwitchThemeCommand!);
            this.WhenAnyValue(x => x.SelectedDensity).InvokeCommand(SwitchDensityCommand!);

            LoadSettings();
        }

        private static IEnumerable<ThemeVM> GetAvailableThemeVMs()
        {
            return new List<ThemeVM>() {
                new ThemeVM(MyTheme.Light,"Jasny"),
                new ThemeVM(MyTheme.Dark,"Ciemny")
            };
        }

        private static IEnumerable<DensityVM> GetAvailableDensitiesVMs()
        {
            return new List<DensityVM>() {
                new DensityVM(DensityStyle.Normal,"Normalny"),
                new DensityVM(DensityStyle.Compact,"Kompaktowy")
            };
        }

        private void LoadSettings()
        {
            var savedTheme = SettingsManager.GetThemeSetting();
            SwitchTheme(savedTheme);

            var savedDensity = SettingsManager.GetDensitySetting();
            SwitchDensity(savedDensity);
        }

        private void SwitchTheme(ThemeVM selectedTheme)
        {
            if (selectedTheme is not null)
            {
                SwitchTheme(selectedTheme.Value);
            }
        }

        private void SwitchTheme(MyTheme selectedTheme)
        {
            _appearanceManager.SetTheme(App.Current, selectedTheme);
            SelectTheme(selectedTheme);
            SettingsManager.SaveThemeSetting(selectedTheme);
        }

        private void SelectTheme(MyTheme themeToSelect)
        {
            foreach (var currentTheme in Themes)
            {
                if (currentTheme.Value == themeToSelect)
                {
                    currentTheme.IsSelected = true;
                }
                else
                {
                    currentTheme.IsSelected = false;
                }
            }
        }

        private void SwitchDensity(DensityVM density)
        {
            if (density is not null)
            {
                SwitchDensity(density.Value);
            }
        }

        private void SwitchDensity(DensityStyle density)
        {
            _appearanceManager.SetDensity(App.Current, density);
            SelectDensity(density);
            SettingsManager.SaveDensitySetting(density);
        }

        private void SelectDensity(DensityStyle densityToSelect)
        {
            foreach (var currentDensity in Densities)
            {
                if (currentDensity.Value == densityToSelect)
                {
                    currentDensity.IsSelected = true;
                }
                else
                {
                    currentDensity.IsSelected = false;
                }
            }
        }
    }
}
