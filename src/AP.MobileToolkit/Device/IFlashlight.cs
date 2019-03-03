using System.Threading.Tasks;

namespace AP.MobileToolkit.Device
{
    public interface IFlashlight
    {
        Task TurnOnAsync();
        Task TurnOffAsync();
    }
}
