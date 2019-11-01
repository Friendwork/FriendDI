using System;
using System.Reflection;
using System.Linq;
using Microsoft.Extensions.Configuration;
using FriendWorks.DI;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class IServiceCollectionExtension
    {
        /// <summary>
        /// Register Services dependencies<br/>
        /// Default is current executing assembly
        /// </summary>
        /// <param name="assembly">The assembly which contains service classes</param>
        public static void RegisterServicesNameSpace(this IServiceCollection serviceCollection, Assembly assembly)
        {
            // get DI setting
            var config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json").Build();
            DISetting diSetting = config.GetSection("DISettings").Get<DISetting>();
            if(diSetting == null)
            {
                throw new Exception("DISetting is not defined");
            }

            Type[] serviceTypes = ReflectionHelper.GetTypeInNamespace(diSetting.ServiceNS, assembly);
            Type[] implTypes = ReflectionHelper.GetTypeInNamespace(diSetting.ServiceImplNS, assembly);
            foreach(Type type in serviceTypes)
            {
                Type implType = implTypes.Where((t) => {
                    bool isAssignale = type.IsAssignableFrom(t);
                    if(diSetting != null && diSetting.UseAttribute)
                    {
                        Service attr = ReflectionHelper.GetServiceAttribute(t);
                        if(attr == null)
                        {
                            isAssignale = false;
                        }
                    }
                    return isAssignale;
                }).FirstOrDefault();

                if(implType == null)
                {
                    throw new Exception("There are no implement of " + type.FullName);
                }

                Service attr = ReflectionHelper.GetServiceAttribute(implType);
                if(attr == null || attr.LeftTime == LeftTime.Scope) 
                {
                    serviceCollection.AddScoped(type,implType);
                } 
                else if(attr.LeftTime == LeftTime.Singleton)
                {
                    serviceCollection.AddSingleton(type,implType);
                }
                else if(attr.LeftTime == LeftTime.Transient)
                {
                    serviceCollection.AddTransient(type,implType);
                }
            }
        }
    }
}