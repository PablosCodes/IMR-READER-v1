using IMRReader.Application.ViewModels;

namespace IMRReader.Application.Abstract
{
    public interface IMeasurementDataService
    {
        Task<MeasurementData> LoadMeasurementInfo(MeasurementVM measurementVM);
    }
}
