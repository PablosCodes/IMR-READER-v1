using Avalonia.Themes.Fluent;
using IMRReader.Application.Common;

namespace IMRReader.Application.ViewModels
{
    public class DensityVM : SelectiveAppearanceOption<DensityStyle>
    {
        public DensityVM(DensityStyle density, string name) : base(density, name)
        {
        }
    }
}
