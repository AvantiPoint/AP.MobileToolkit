using System.IO;
using System.Threading.Tasks;

namespace AP.MobileToolkit.Device
{
    public interface IFileSystem
    {
        Task<Stream> OpenAppPackageFileAsync(string filename);
        string CacheDirectory { get; }
        string AppDataDirectory { get; }
    }
}