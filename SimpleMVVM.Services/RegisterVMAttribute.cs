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
    public class RegisterVMWithIocAttribute : Attribute
    {
        public InstanceMode Mode { get; set; }

        public RegisterVMWithIocAttribute(InstanceMode mode) {
            Mode = mode;
        }
    }
}
