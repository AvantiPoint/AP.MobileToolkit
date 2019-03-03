using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AP.MobileToolkit.Device
{
    public interface ISms
    {
        Task ComposeAsync();
        Task ComposeAsync(SmsMessage message);
    }
}
