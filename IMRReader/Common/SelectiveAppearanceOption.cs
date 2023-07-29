using IMRReader.Managers;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMRReader.Common
{
    public abstract class SelectiveAppearanceOption<T> : ReactiveObject where T : Enum
    {
        public T Value { get; set; }

        public string Name { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                this.RaiseAndSetIfChanged(ref _isSelected, value);
            }
        }

        public SelectiveAppearanceOption(T myTheme, string name)
        {
            Value = myTheme;
            Name = name;
            IsSelected = false;
        }
    }
}
