using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMRReader.Application.ViewModels
{
    public record MeasurementDataVM
    {
        public required double[] XData { get; set; }

        public required double[] YData { get; set; }
    }
}
