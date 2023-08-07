using IMRReader.Application.ViewModels;
using IMRReader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMRReader.Application.Common
{
    public static class Mapping
    {
        public static MeasurementVM GetVM(this Measurement measurement)
        {
            MeasurementVM measurementVM = new()
            {
                Id = measurement.Id,
                Comment = measurement.Comment,
                Date= measurement.Date,
                Method = measurement.Method,
                Results = measurement.Results               
            };

            return measurementVM;
        }

        public static TargetVM GetVM(this Target target) { 
            TargetVM targetVM = new()
            {
                Id = target.Id,
                Name = target.Name
            };

            return targetVM;
        }
    }
}
