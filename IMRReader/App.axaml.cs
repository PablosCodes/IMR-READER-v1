using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using HanumanInstitute.MvvmDialogs.Avalonia;
using IMRReader.Application.Abstract;
using IMRReader.Application.Common;
using IMRReader.DataContextes;
using IMRReader.Domain.Abstract;
using IMRReader.Views;

namespace IMRReader
{
    public partial class App : Avalonia.Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);

            DependencyInjection.Register(Splat.Locator.CurrentMutable);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var locator = Splat.Locator.Current;

                var targetInfoLoader = locator.GetRequiredService<ITargetInfoLoader>();
                var measurementDataService = locator.GetRequiredService<IMeasurementDataService>();
                var dialogService = new DialogService();

                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowVM(dialogService, targetInfoLoader, measurementDataService),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}