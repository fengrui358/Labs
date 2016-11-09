using System;

namespace WpfHartDebugTool.WPF.Views
{
    // MvvmCross / MvvmCross / Windows / WindowsUWP / Views / MvxRegionAttribute.cs 
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class MvxRegionAttribute
        : Attribute
    {
        public MvxRegionAttribute(string regionName)
        {
            this.Name = regionName;
        }

        public string Name { get; private set; }
    }
}
