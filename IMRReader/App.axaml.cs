using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using IMReader.Application.Managers;
using IMRReader.Application.Managers;
using IMRReader.ViewModels;
using IMRReader.Views;

namespace IMRReader
{
    public partial class App : Avalonia.Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowVM(),
                };
            }

            LoadAppearanceSettings();

            base.OnFrameworkInitializationCompleted();
        }

        private void LoadAppearanceSettings()
        {
            var style = SettingsManager.GetDensitySetting();
            AppearanceManager.SetDensity(this, style);

            var theme = SettingsManager.GetThemeSetting();
            AppearanceManager.SetTheme(this, theme);
        }
    }
}