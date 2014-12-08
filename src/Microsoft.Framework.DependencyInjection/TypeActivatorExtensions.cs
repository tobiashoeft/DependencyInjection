// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.Framework.DependencyInjection
{
    public static class TypeActivatorExtensions
    {
        public static T CreateInstance<T>(this ITypeActivator activator, IServiceProvider serviceProvider, params object[] parameters)
        {
            return (T)CreateInstance(activator, serviceProvider, typeof(T), parameters);
        }

        public static T GetServiceOrCreateInstance<T>(this ITypeActivator activator, IServiceProvider services)
        {
            return (T)GetServiceOrCreateInstance(activator, services, typeof(T));
        }

        public static object GetServiceOrCreateInstance(this ITypeActivator activator, IServiceProvider services, Type type)
        {
            return GetServiceNoExceptions(services, type) ?? CreateInstance(activator, services, type);
        }

        private static object GetServiceNoExceptions(IServiceProvider services, Type type)
        {
            try
            {
                return services.GetService(type);
            }
            catch
            {
                return null;
            }
        }

        private static object CreateInstance(this ITypeActivator activator, IServiceProvider serviceProvider, Type serviceType, params object[] parameters)
        {
            if (activator == null)
            {
                throw new ArgumentNullException(nameof(activator));
            }

            return activator.CreateInstance(serviceProvider, serviceType, parameters);
        }
    }
}