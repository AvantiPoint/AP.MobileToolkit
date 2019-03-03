using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AP.MobileToolkit.Device
{
    public interface IEmail
    {
        Task ComposeAsync();
        Task ComposeAsync(string subject, string body, params string[] to);
        Task ComposeAsync(EmailMessage message);
    }
}
