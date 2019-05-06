using System.Threading.Tasks;

namespace AP.MobileToolkit.Http
{
    public interface IAuthenticationHandler
    {
        Task<string> GetTokenAsync();
    }
}
