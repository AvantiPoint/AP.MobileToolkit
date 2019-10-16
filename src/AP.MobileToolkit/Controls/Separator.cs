using AP.MobileToolkit.Extensions;
using Xamarin.Forms;

namespace AP.MobileToolkit.Controls
{
    /// <summary>
    /// Class Separator.
    /// </summary>
    public class Separator : View
    {
        /// <summary>
        /// The orientation property
        /// </summary>
        public static readonly BindableProperty OrientationProperty =
            BindableProperty.Create(nameof(Orientation), typeof(SeparatorOrientation), typeof(Separator), SeparatorOrientation.Horizontal, BindingMode.OneWay);

        /// <summary>
        /// The color property
        /// </summary>
        public static readonly BindableProperty ColorProperty =
            BindableProperty.Create(nameof(Color), typeof(Color), typeof(Separator), Color.Default, BindingMode.OneWay);

        /// <summary>
        /// The spacing before property
        /// </summary>
        public static readonly BindableProperty SpacingBeforeProperty =
            BindableProperty.Create(nameof(SpacingBefore), typeof(double), typeof(Separator), 1.0, BindingMode.OneWay);

        /// <summary>
        /// The spacing after property
        /// </summary>
        public static readonly BindableProperty SpacingAfterProperty =
            BindableProperty.Create(nameof(SpacingAfter), typeof(double), typeof(Separator), 1.0, BindingMode.OneWay);

        /// <summary>
        /// The thickness property
        /// </summary>
        public static readonly BindableProperty ThicknessProperty =
            BindableProperty.Create(nameof(Thickness), typeof(double), typeof(Separator), 1.0, BindingMode.OneWay);

        /// <summary>
        /// The stroke type property
        /// </summary>
        public static readonly BindableProperty StrokeTypeProperty =
            BindableProperty.Create(nameof(StrokeType), typeof(StrokeType), typeof(Separator), StrokeType.Solid, BindingMode.OneWay);

        /// <summary>
        /// Initializes a new instance of the <see cref="Separator"/> class.
        /// </summary>
        public Separator()
        {
            UpdateRequestedSize();
        }

        /// <summary>
        /// Gets or Sets Orientation of the separator.
        /// </summary>
        /// <value>The orientation.</value>
        public SeparatorOrientation Orientation
        {
            get => this.GetValue<SeparatorOrientation>(OrientationProperty);
            private set => SetValue(OrientationProperty, value);
        }

        /// <summary>
        /// Gets or Sets Color of the separator. Black is a default color
        /// </summary>
        /// <value>The color.</value>
        public Color Color
        {
            get => this.GetValue<Color>(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        /// <summary>
        /// Gets or Sets Padding before the separator. Default is 1.
        /// </summary>
        /// <value>The spacing before.</value>
        public double SpacingBefore
        {
            get => this.GetValue<double>(SpacingBeforeProperty);
            set => SetValue(SpacingBeforeProperty, value);
        }

        /// <summary>
        /// Gets or Sets Padding after the separator. Default is 1.
        /// </summary>
        /// <value>The spacing after.</value>
        public double SpacingAfter
        {
            get => this.GetValue<double>(SpacingAfterProperty);
            set => SetValue(SpacingAfterProperty, value);
        }

        /// <summary>
        /// Gets or Sets How thick should the separator be. Default is 1
        /// </summary>
        /// <value>The thickness.</value>
        public double Thickness
        {
            get => this.GetValue<double>(ThicknessProperty);
            set => SetValue(ThicknessProperty, value);
        }

        /// <summary>
        /// Gets or Sets Stroke style of the separator. Default is Solid.
        /// </summary>
        /// <value>The type of the stroke.</value>
        public StrokeType StrokeType
        {
            get => this.GetValue<StrokeType>(StrokeTypeProperty);
            set => SetValue(StrokeTypeProperty, value);
        }

        /// <summary>
        /// Call this method from a child class to notify that a change happened on a property.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        /// <remarks>A <see cref="T:Xamarin.Forms.BindableProperty" /> triggers this by itself. An inheritor only needs to call this for properties without <see cref="T:Xamarin.Forms.BindableProperty" /> as the backend store.</remarks>
        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == ThicknessProperty.PropertyName ||
               propertyName == ColorProperty.PropertyName ||
               propertyName == SpacingBeforeProperty.PropertyName ||
               propertyName == SpacingAfterProperty.PropertyName ||
               propertyName == StrokeTypeProperty.PropertyName ||
               propertyName == OrientationProperty.PropertyName)
            {
                UpdateRequestedSize();
            }
        }

        /// <summary>
        /// Updates the size of the requested.
        /// </summary>
        private void UpdateRequestedSize()
        {
            var minSize = Thickness;
            var optimalSize = SpacingBefore + Thickness + SpacingAfter;
            if (Orientation == SeparatorOrientation.Horizontal)
            {
                MinimumHeightRequest = minSize;
                HeightRequest = optimalSize;
                HorizontalOptions = LayoutOptions.FillAndExpand;
            }
            else
            {
                MinimumWidthRequest = minSize;
                WidthRequest = optimalSize;
                VerticalOptions = LayoutOptions.FillAndExpand;
            }
        }
    }
}
