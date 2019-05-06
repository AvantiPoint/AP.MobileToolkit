using System.Threading.Tasks;
using AP.MobileToolkit.Http;

namespace AP.MobileToolkit.Http.Tests.Mocks
{
    public class MockAuthenticationHandler : IAuthenticationHandler
    {
        public const string Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiZ2l2ZW5fbmFtZSI6IkpvaG4iLCJmYW1pbHlfbmFtZSI6IkRvZSIsIm9pZCI6ImM2NTJkZWNiLTM3NGUtNGEzNC1iNmUwLWMwZDM3NzQzNmE3MSIsImlhdCI6MTUxNjIzOTAyMn0.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";

        public bool DidGetToken => GetTokenCount > 0;

        public int GetTokenCount { get; private set; }

        public Task<string> GetTokenAsync()
        {
            GetTokenCount++;
            return Task.FromResult(Token);
        }
    }
}
