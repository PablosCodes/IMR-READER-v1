using Avalonia;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMRReader.Application.Managers
{
    public static class AppearanceManager
    {
        public static void SetTheme(Avalonia.Application? app, MyTheme requestedTheme)
        {
            if (app is not null)
            {
               app.RequestedThemeVariant = GetThemeVariant(requestedTheme);
            }
        }

        private static ThemeVariant GetThemeVariant(MyTheme theme) => theme switch { 
            MyTheme.Dark => ThemeVariant.Dark,
            MyTheme.Light => ThemeVariant.Light,
            _ => ThemeVariant.Light
        };

        public static void SetDensity(Avalonia.Application? app, DensityStyle requestedDensity)
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
}
