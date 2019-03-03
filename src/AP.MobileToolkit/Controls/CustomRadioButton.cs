using System;
using AP.CrossPlatform;
using AP.CrossPlatform.Extensions;
using AP.MobileToolkit.Extensions;
using Xamarin.Forms;

namespace AP.MobileToolkit.Controls
{
    /// <summary>
    /// Class CustomRadioButton.
    /// </summary>
    public class CustomRadioButton : View
    {
        /// <summary>
        /// The checked property
        /// </summary>
        public static readonly BindableProperty CheckedProperty =
            BindableProperty.Create( nameof( Checked ), typeof( bool ), typeof( CustomRadioButton ), false );

        /// <summary>
        /// The default text property.
        /// </summary>
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create( nameof( Text ), typeof( string ), typeof( CustomRadioButton ), string.Empty );

        /// <summary>
        /// The default text property.
        /// </summary>
        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create( nameof( TextColor ), typeof( Color ), typeof( CustomRadioButton ), Color.Default );

        /// <summary>
        /// The font size property
        /// </summary>
        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create( nameof( FontSize ), typeof( double ), typeof( CustomRadioButton ), -1.0 );

        /// <summary>
        /// The font name property.
        /// </summary>
        public static readonly BindableProperty FontNameProperty =
            BindableProperty.Create( nameof( FontName ), typeof( string ), typeof( CustomRadioButton ), string.Empty );

        /// <summary>
        /// The checked changed event.
        /// </summary>
        public EventHandler<EventArgs<bool>> CheckedChanged;

        /// <summary>
        /// Gets or sets a value indicating whether the control is checked.
        /// </summary>
        /// <value>The checked state.</value>
        public bool Checked
        {
            get { return this.GetValue<bool>( CheckedProperty ); }
            set
            {
                if ( value != this.GetValue<bool>( CheckedProperty ) )
                {
                    SetValue( CheckedProperty, value );

                    CheckedChanged?.Invoke( this, value );
                }
            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get { return this.GetValue<string>( TextProperty ); }
            set { SetValue( TextProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the color of the text.
        /// </summary>
        /// <value>The color of the text.</value>
        public Color TextColor
        {
            get { return this.GetValue<Color>( TextColorProperty ); }
            set { SetValue( TextColorProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the size of the font.
        /// </summary>
        /// <value>The size of the font.</value>
        public double FontSize
        {
            get { return this.GetValue<double>( FontSizeProperty ); }
            set { SetValue( FontSizeProperty, value ); }
        }

        /// <summary>
        /// Gets or sets the name of the font.
        /// </summary>
        /// <value>The name of the font.</value>
        public string FontName
        {
            get { return this.GetValue<string>( FontNameProperty ); }
            set { SetValue( FontNameProperty, value ); }
        }

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    }
}
