using IMRReader.Domain.Models;

namespace IMRReader.Domain.Abstract
{
    public interface ITargetInfoLoader
    {
        void Initialize(string filePath);

        IAsyncEnumerable<Target> GetTargets();

        IAsyncEnumerable<Measurement> GetMeasurementsForTarget(int targetId);
    }
}
