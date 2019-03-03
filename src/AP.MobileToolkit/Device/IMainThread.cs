using System;

namespace AP.MobileToolkit.Device
{
    public interface IMainThread
    {
        void BeginInvokeOnMainThread(Action action);
        bool IsMainThread { get; }
    }
}
