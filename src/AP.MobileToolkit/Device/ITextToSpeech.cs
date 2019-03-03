using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AP.MobileToolkit.Device
{
    public interface ITextToSpeech
    {
        Task<IEnumerable<Locale>> GetLocalesAsync();
        Task SpeakAsync(string text, CancellationToken cancelToken = default(CancellationToken));
        Task SpeakAsync(string text, SpeechOptions options, CancellationToken cancelToken = default(CancellationToken));
    }
}
