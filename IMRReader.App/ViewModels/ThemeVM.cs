using IMRReader.Application.Common;
using IMRReader.Common;

namespace IMRReader.DataContextes
{
    public class ThemeVM : SelectiveAppearanceOption<MyTheme>
    {
        public ThemeVM(MyTheme myTheme, string name) : base(myTheme, name)
        {
        }
    }
}
