using System.Threading.Tasks;

namespace AP.MobileToolkit.Device
{
    public interface IClipboard
    {
        Task SetTextAsync(string text);
        Task<string> GetTextAsync();
        bool HasText { get; }
    }
}
