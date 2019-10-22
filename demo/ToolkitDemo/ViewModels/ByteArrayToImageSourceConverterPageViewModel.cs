using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;

namespace ToolkitDemo.ViewModels
{
    public class ByteArrayToImageSourceConverterPageViewModel : DemoPageViewModelBase
    {
        private const string ImageUrl = "https://avantipoint.com/images/AvantiPoint.png";

        public ByteArrayToImageSourceConverterPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ILogger logger)
            : base(navigationService, pageDialogService, logger)
        {
        }

        public byte[] ImageData
        {
            get => _imageData;
            set => this.RaiseAndSetIfChanged(ref _imageData, value);
        }

        private byte[] _imageData;

        protected async override void OnAppearing()
        {
            ImageData = await GetImageDataAsync(ImageUrl);
        }

        private async Task<byte[]> GetImageDataAsync(string url)
        {
            try
            {
                byte[] buffer = new byte[16 * 1024];
                var httpClient = new HttpClient();
                buffer = await httpClient.GetByteArrayAsync(url);
                return buffer;
            }
            catch (Exception ex)
            {
                var data = new Dictionary<string, string>
                {
                    { "page", "ByteArrayToImageSourceConverter" }
                };

                Logger.Report(ex, data);
                return null;
            }
        }
    }
}
