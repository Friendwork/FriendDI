using System;

namespace FriendWorks.DI
{
    public class Service : Attribute
    {
        public LeftTime LeftTime { get; set; } = LeftTime.Scope;
    }

    public enum LeftTime
    {
        Singleton,
        Transient,
        Scope
    }
}