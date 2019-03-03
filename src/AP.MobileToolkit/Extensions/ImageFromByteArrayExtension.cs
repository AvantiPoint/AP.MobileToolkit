using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using AP.CrossPlatform;
using AP.CrossPlatform.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AP.MobileToolkit.Extensions
{
    /// <summary>
    /// Image from byte array extension.
    /// </summary>
    [ContentProperty( nameof( Image ) )]
    public class ImageFromByteArrayExtension : IMarkupExtension
    {
        const string defaultImageUri = "http://wpsites.org/wp-content/uploads/2016/05/broken-image-fix.png";

        /// <summary>
        /// Gets or sets the image data.
        /// </summary>
        /// <value>The image data.</value>
        public byte[] ImageData { get; set; }

        private ImageSource defaultImage
        {
            get { return AsyncHelpers.RunSync( GetDefaultImageAsync ); }
        }

        /// <inheritDoc />
        public object ProvideValue( IServiceProvider serviceProvider )
        {
            var stream = ImageData.ToStream();
            return stream == null ? defaultImage : ImageSource.FromStream( () => stream );
        }

        /// <inheritDoc />
        public async Task<ImageSource> GetDefaultImageAsync()
        {
            using ( var client = new HttpClient() )
            {
                client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
                {
                    MaxAge = TimeSpan.FromDays( 180 )
                };

                var responseMessage = await client.GetAsync( defaultImageUri );
                if ( !responseMessage.IsSuccessStatusCode ) return null;
                var stream = await responseMessage.Content.ReadAsStreamAsync();
                return ImageSource.FromStream( () => stream );
            }
        }
    }
}
