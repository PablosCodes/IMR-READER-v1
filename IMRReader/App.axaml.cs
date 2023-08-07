using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using HanumanInstitute.MvvmDialogs.Avalonia;
using IMRReader.Application.Managers;
using IMRReader.DataContextes;
using IMRReader.Managers;
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
                    DataContext = new MainWindowVM(new DialogService()),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}