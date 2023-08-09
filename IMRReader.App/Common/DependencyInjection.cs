using IMRReader.Domain.Abstract;
using IMRReader.Infrastructure.FileReaders;

namespace IMRReader.Application.Common
{
    public static class DependencyInjection
    {
        public static ITargetInfoLoader InjectTargetInfoLoader() => new SQLiteTargetInfoLoader();
    }
}
