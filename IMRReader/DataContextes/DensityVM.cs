using IMRReader.Managers;
using IMRReader.Common;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Themes.Fluent;

namespace IMRReader.DataContextes
{
    public class DensityVM : SelectiveAppearanceOption<DensityStyle>
    {
        public DensityVM(DensityStyle density, string name) : base(density, name)
        {
        }
    }
}
