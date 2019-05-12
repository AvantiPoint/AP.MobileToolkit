using System;
using System.Threading.Tasks;
using Prism.Navigation;

namespace AP.MobileToolkit.Tests.Mocks
{
    public class NavigationServiceMock : INavigationService
    {
        public bool Delay { get; set; }

        public Task<INavigationResult> GoBackAsync()
        {
            throw new NotImplementedException();
        }

        public Task<INavigationResult> GoBackAsync(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public Task<INavigationResult> NavigateAsync(Uri uri)
        {
            throw new NotImplementedException();
        }

        public Task<INavigationResult> NavigateAsync(Uri uri, INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public Task<INavigationResult> NavigateAsync(string name) =>
            NavigateAsync(name, null);

        public async Task<INavigationResult> NavigateAsync(string name, INavigationParameters parameters)
        {
            await Task.Run(() =>
            {
                while (Delay)
                {
                }
            });

            if (name == "good")
                return new NavigationResult { Success = true };

            return new NavigationResult { Success = false, Exception = new Exception(name) };
        }
    }
}
