using IMRReader.Application.Abstract;
using IMRReader.Application.Services;
using Splat;

namespace IMRReader.Application
{
    public static class DependencyInjection
    {
        public static void AddServices(this IMutableDependencyResolver services)
        {
            services.Register<IMeasurementDataService>(() => new BinaryMeasurementDataService());
            services.Register<IMessageBusService>(() => new MessageBusService());
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
