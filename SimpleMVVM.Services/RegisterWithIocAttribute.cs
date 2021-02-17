using System;

namespace SimpleMVVM.Services
{
    public enum InstanceMode
    {
        /// <summary>
        /// Not registered
        /// </summary>
        None = 0,

        /// <summary>
        /// Single
        /// </summary>
        Singleton,

        /// <summary>
        /// Transient
        /// </summary>
        Transient
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class RegisterWithIocAttribute : Attribute
    {
        public InstanceMode Mode { get; set; }

        public RegisterWithIocAttribute(InstanceMode mode)
        {
            Mode = mode;
        }
    }
}
