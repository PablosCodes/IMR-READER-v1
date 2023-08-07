using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMRReader.Application.ViewModels
{
    public record TargetVM
    {
        public required int Id { get; set; }

        public ObservableCollection<MeasurementVM>? Measurements { get; set; }

        public required string Name { get; set; }
    }
}
