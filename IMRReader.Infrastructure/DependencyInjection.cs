﻿using IMRReader.Domain.Abstract;
using IMRReader.Infrastructure.FileReaders.SQLite;
using Splat;

namespace IMRReader.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IMutableDependencyResolver services)
        {
            services.Register<ITargetInfoLoader>(() => new SQLiteTargetInfoLoader());
        }
    }
}
