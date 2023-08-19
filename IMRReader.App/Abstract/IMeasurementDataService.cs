using IMRReader.Application.ViewModels;
using IMRReader.Domain.Models;

namespace IMRReader.Application.Abstract
{
    public interface IMeasurementDataService
    {
        Task<MeasurementMetrics> LoadMeasurementInfo(MeasurementVM measurementVM);
    }
}
