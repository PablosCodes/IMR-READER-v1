using Avalonia.Themes.Fluent;
using IMRReader.Application.Common;
using System.Configuration;

namespace IMRReader.Application.Managers
{
    public static class SettingsManager
    {
        private const string THEME_SETTING = "theme";
        private const string DENSITY_SETTING = "density";

        public static MyTheme GetThemeSetting()
        {
            return ReadSetting<MyTheme>(THEME_SETTING);
        }

        public static void SaveThemeSetting(MyTheme theme)
        {
            SaveEnum(THEME_SETTING, theme);
        }

        public static DensityStyle GetDensitySetting()
        {
            return ReadSetting<DensityStyle>(DENSITY_SETTING);
        }

        public static void SaveDensitySetting(DensityStyle density)
        {
            SaveEnum(DENSITY_SETTING, density);
        }

        private static T ReadSetting<T>(string key) where T : struct
        {
            var setting = ConfigurationManager.AppSettings[key] ?? string.Empty;

            if (!Enum.TryParse(setting, true, out T settingValue))
            {
                return default;
            }

            return settingValue;
        }

        // TODO: Add exception handling
        private static void SaveEnum(string key, Enum value)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            if (settings[key] == null)
            {
                settings.Add(key, value.ToString());
            }
            else
            {
                settings[key].Value = value.ToString();
            }
            configFile.Save(ConfigurationSaveMode.Modified);
        }
    }
}
