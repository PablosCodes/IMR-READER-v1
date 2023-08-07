using IMRReader.Domain.Abstract;
using IMRReader.Infrastructure.FileReaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMRReader.Application.Common
{
    public static class DependencyInjection
    {
        public static ITargetInfoLoader InjectTargetInfoLoader() => new SQLiteTargetInfoLoader();
    }
}
