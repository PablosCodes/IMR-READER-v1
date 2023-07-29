using Avalonia;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;

namespace IMRReader.Managers;

public class AppearanceManager
{
    private MyTheme _currentTheme;

    public AppearanceManager()
    {
        _currentTheme = default;
    }

    public MyTheme SetTheme(Avalonia.Application? app, MyTheme requestedTheme)
    {
        if (app is not null)
        {
           app.RequestedThemeVariant = GetThemeVariant(requestedTheme);
            _currentTheme = requestedTheme;
        }

        return _currentTheme;
    }

    private ThemeVariant GetThemeVariant(MyTheme theme) => theme switch { 
        MyTheme.Dark => ThemeVariant.Dark,
        MyTheme.Light => ThemeVariant.Light,
        _ => ThemeVariant.Light
    };

    public void SetDensity(Avalonia.Application? app, DensityStyle requestedDensity)
    {
        if (app is not null)
        {
            var fluentTheme = app.Styles.FirstOrDefault(x => x is FluentTheme);

            if (fluentTheme is not null)
                app.Styles.Remove(fluentTheme);

            app.Styles.Add(new FluentTheme() { DensityStyle = requestedDensity });
        }
    }
}
public enum MyTheme { Light, Dark };
