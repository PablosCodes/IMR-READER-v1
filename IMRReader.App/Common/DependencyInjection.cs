using IMRReader.Application.Abstract;
using IMRReader.Application.Services;
using IMRReader.Domain.Abstract;
using IMRReader.Infrastructure.FileReaders;
using Splat;

namespace IMRReader.Application.Common
{
    public static class DependencyInjection
    {
        public static void Register(IMutableDependencyResolver services)
        {
            services.Register<ITargetInfoLoader>(() => new SQLiteTargetInfoLoader());
            services.Register<IMeasurementDataService>(() => new BinaryMeasurementDataService());
        }

        public static T GetRequiredService<T>(this IReadonlyDependencyResolver resolver)
        {
            var service = resolver.GetService<T>();
            if (service is null)
            {
                throw new InvalidOperationException($"Failed to resolve object of type {typeof(T)}");
            }

            return service;
        }
    }
}
