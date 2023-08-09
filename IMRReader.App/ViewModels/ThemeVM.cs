using IMRReader.Application.Common;

namespace IMRReader.Application.ViewModels
{
    public class ThemeVM : SelectiveAppearanceOption<MyTheme>
    {
        public ThemeVM(MyTheme myTheme, string name) : base(myTheme, name)
        {
        }
    }
}
