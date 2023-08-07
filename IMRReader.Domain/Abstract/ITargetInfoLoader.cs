using IMRReader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMRReader.Domain.Abstract
{
    public interface ITargetInfoLoader
    {
        void Initialize(string filePath);

        IAsyncEnumerable<Target> GetTargets();

        IAsyncEnumerable<Measurement> GetMeasurementsForTarget(int targetId);
    }
}
