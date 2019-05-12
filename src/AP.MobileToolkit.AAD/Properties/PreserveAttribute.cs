using System;

namespace AP.MobileToolkit
{
    [AttributeUsage(AttributeTargets.All)]
    internal class PreserveAttribute : Attribute
    {
        public PreserveAttribute()
        {
        }

        public bool Conditional { get; set; }

        public bool AllMembers { get; set; }
    }
}
