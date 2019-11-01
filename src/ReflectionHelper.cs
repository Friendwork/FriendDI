using System;
using System.Reflection;
using System.Linq;

namespace FriendWorks.DI
{
    /// <summary>
    /// Reflection Helper
    /// </summary>
    public class ReflectionHelper
    {
        public static Type[] GetTypeInNamespace(string ns, Assembly assembly = null)
        {
            if(string.IsNullOrEmpty(ns))
            {
                throw new Exception("Namespace can not be null");
            }
            if(assembly == null)
            {
                // get current excuting assembly
                assembly = Assembly.GetExecutingAssembly();
            }

            return assembly.GetTypes().Where(t =>  ns.Equals(t.Namespace)).ToArray();
        }

        public static Service GetServiceAttribute(Type type)
        {
            return (Service)type.GetCustomAttribute(typeof(Service));
        }
    }
}