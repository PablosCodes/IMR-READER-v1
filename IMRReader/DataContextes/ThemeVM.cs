using IMRReader.Managers;
using IMRReader.Common;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMRReader.Application.Common;

namespace IMRReader.DataContextes
{
    public class ThemeVM : SelectiveAppearanceOption<MyTheme>
    {
        public ThemeVM(MyTheme myTheme, string name) : base(myTheme, name)
        {
        }
    }
}
