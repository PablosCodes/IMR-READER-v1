using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using HanumanInstitute.MvvmDialogs.Avalonia;
using IMRReader.Application;
using IMRReader.Application.Abstract;
using IMRReader.DataContextes;
using IMRReader.Domain.Abstract;
using IMRReader.Infrastructure;
using IMRReader.Views;

namespace IMRReader
{
    public partial class App : Avalonia.Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);

            Splat.Locator.CurrentMutable.AddServices();
            Splat.Locator.CurrentMutable.AddInfrastructure();
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var locator = Splat.Locator.Current;

                var targetInfoLoader = locator.GetRequiredService<ITargetInfoLoader>();
                var measurementDataService = locator.GetRequiredService<IMeasurementDataService>();
                var dialogService = new DialogService();
                var messageBusService = locator.GetRequiredService<IMessageBusService>();

                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowVM(dialogService, targetInfoLoader, measurementDataService, messageBusService),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}