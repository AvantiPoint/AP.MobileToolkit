﻿using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;
using AP.MobileToolkit.Controls;

namespace AP.MobileToolkit.Controls.Platform.Droid
{
    /// <summary>
    /// Class SeparatorDroidView.
    /// </summary>
    public class SeparatorDroidView : View
    {
        /// <summary>
        /// The _orientation
        /// </summary>
        private SeparatorOrientation _orientation;

        /// <summary>
        /// Density measure
        /// </summary>
        private float _dm;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeparatorDroidView" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public SeparatorDroidView(Context context)
            : base(context)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SeparatorDroidView" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="attrs">The attrs.</param>
        public SeparatorDroidView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SeparatorDroidView" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="attrs">The attrs.</param>
        /// <param name="defStyle">The definition style.</param>
        public SeparatorDroidView(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {
            Initialize();
        }

        /// <summary>
        /// Gets or sets the thickness.
        /// </summary>
        /// <value>The thickness.</value>
        public double Thickness { get; set; }

        /// <summary>
        /// Gets or sets the spacing before.
        /// </summary>
        /// <value>The spacing before.</value>
        public double SpacingBefore { get; set; }

        /// <summary>
        /// Gets or sets the spacing after.
        /// </summary>
        /// <value>The spacing after.</value>
        public double SpacingAfter { get; set; }

        /// <summary>
        /// Gets or sets the color of the stroke.
        /// </summary>
        /// <value>The color of the stroke.</value>
        public Color StrokeColor { get; set; }

        /// <summary>
        /// Gets or sets the type of the stroke.
        /// </summary>
        /// <value>The type of the stroke.</value>
        public StrokeType StrokeType { get; set; }

        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        /// <value>The orientation.</value>
        public SeparatorOrientation Orientation
        {
            get => _orientation;
            set
            {
                _orientation = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Implement this to do your drawing.
        /// </summary>
        /// <param name="canvas">the canvas on which the background will be drawn</param>
        /// <since version="Added in API level 1" />
        /// <remarks>
        ///     <para tool="javadoc-to-mdoc">Implement this to do your drawing.</para>
        ///     <para tool="javadoc-to-mdoc">
        ///         <format type="text/html">
        ///             <a href="http://developer.android.com/reference/android/view/View.html#onDraw(android.graphics.Canvas)"
        ///                 target="_blank">
        ///                 [Android Documentation]
        ///             </a>
        ///         </format>
        ///     </para>
        /// </remarks>
        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            var r = new Rect(0, 0, canvas.Width, canvas.Height);
            var dAdjustedThicnkess = (float)Thickness * _dm;

            var paint = new Paint { Color = StrokeColor, StrokeWidth = dAdjustedThicnkess, AntiAlias = true };
            paint.SetStyle(Paint.Style.Stroke);
            switch (StrokeType)
            {
                case StrokeType.Dashed:
                    paint.SetPathEffect(new DashPathEffect(new[] { 6 * _dm, 2 * _dm }, 0));
                    break;
                case StrokeType.Dotted:
                    paint.SetPathEffect(new DashPathEffect(new[] { dAdjustedThicnkess, dAdjustedThicnkess }, 0));
                    break;
                default:

                    break;
            }

            var desiredTotalSpacing = (SpacingAfter + SpacingBefore) * _dm;
            float leftForSpacing = 0;
            float actualSpacingBefore = 0;

            // float actualSpacingAfter = 0;

            if (Orientation == SeparatorOrientation.Horizontal)
            {
                leftForSpacing = r.Height() - dAdjustedThicnkess;
            }
            else
            {
                leftForSpacing = r.Width() - dAdjustedThicnkess;
            }
            if (desiredTotalSpacing > 0)
            {
                var spacingCompressionRatio = (float)(leftForSpacing / desiredTotalSpacing);
                actualSpacingBefore = (float)SpacingBefore * _dm * spacingCompressionRatio;

                // actualSpacingAfter = ( float )SpacingAfter * _dm * spacingCompressionRatio;
            }
            else
            {
                actualSpacingBefore = 0;

                // actualSpacingAfter = 0;
            }
            var thicknessOffset = dAdjustedThicnkess / 2.0f;

            var p = new Path();
            if (Orientation == SeparatorOrientation.Horizontal)
            {
                p.MoveTo(0, actualSpacingBefore + thicknessOffset);
                p.LineTo(r.Width(), actualSpacingBefore + thicknessOffset);
            }
            else
            {
                p.MoveTo(actualSpacingBefore + thicknessOffset, 0);
                p.LineTo(actualSpacingBefore + thicknessOffset, r.Height());
            }
            canvas.DrawPath(p, paint);
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            _dm = Application.Context.Resources.DisplayMetrics.Density;
        }
    }
}
