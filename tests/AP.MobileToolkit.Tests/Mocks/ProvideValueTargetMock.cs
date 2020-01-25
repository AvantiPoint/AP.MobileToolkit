using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Xaml;

namespace AP.MobileToolkit.Tests.Mocks
{
    public class ProvideValueTargetMock : IProvideValueTarget
    {
        public object TargetObject { get; set; }
        public object TargetProperty { get; set; }
    }
}
