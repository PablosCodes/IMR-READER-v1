using Avalonia.Themes.Fluent;
using IMRReader.Common;

namespace IMRReader.DataContextes
{
    public class DensityVM : SelectiveAppearanceOption<DensityStyle>
    {
        public DensityVM(DensityStyle density, string name) : base(density, name)
        {
        }
    }
}
