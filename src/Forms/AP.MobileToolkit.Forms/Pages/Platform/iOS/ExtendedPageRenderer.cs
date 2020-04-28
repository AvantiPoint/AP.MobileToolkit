#if false
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using AP.MobileToolkit.Pages;
using AP.MobileToolkit.Pages.Platform.iOS;
using CoreGraphics;
using CoreAnimation;

namespace AP.MobileToolkit.Pages.Platform.iOS
{
    public class ExtendedPageRenderer : PageRenderer
    {
        private UILabel titleLabel;
        private UILabel subtitleLabel;
        private UIView containerView;
        private UIView titleView;
        private UIView marginView;
        private nfloat lastNavBarHeight = 0.0f;
        private nfloat lastNavBarWidth = 0.0f;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            System.Diagnostics.Debug.WriteLine("Element");
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
           
            SetupNavBar(NavigationController.NavigationBar.Bounds.Size);
            SetTitlePosition(ExtendedNavigationPage.GetTitlePosition(Element), ExtendedNavigationPage.GetTitlePadding(Element), ExtendedNavigationPage.GetTitleMargin(Element), new CGRect(0, 0, Math.Max(subtitleLabel.IntrinsicContentSize.Width, titleLabel.IntrinsicContentSize.Width), (titleLabel.IntrinsicContentSize.Height + subtitleLabel.IntrinsicContentSize.Height + (subtitleLabel.IntrinsicContentSize.Height > 0.0f ? 3.0f : 0.0f))));
            
            System.Diagnostics.Debug.WriteLine("Preparing");
        }

        public override void ViewWillLayoutSubviews()
        {
            base.ViewWillLayoutSubviews();
            System.Diagnostics.Debug.WriteLine("SubViews");
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            if(lastNavBarWidth != NavigationController?.NavigationBar?.Bounds.Size.Width || lastNavBarHeight != NavigationController?.NavigationBar?.Bounds.Size.Height)
            {
                lastNavBarHeight = NavigationController?.NavigationBar?.Bounds.Size.Height ?? 0.0f;
                lastNavBarWidth = NavigationController?.NavigationBar?.Bounds.Size.Width ?? 0.0f;
                SetupNavBar(new CGSize(lastNavBarWidth, lastNavBarHeight));

            }
            
            SetTitlePosition( ExtendedNavigationPage.GetTitlePosition(Element), ExtendedNavigationPage.GetTitlePadding(Element),ExtendedNavigationPage.GetTitleMargin(Element), new CGRect(0, 0, Math.Max(subtitleLabel.IntrinsicContentSize.Width, titleLabel.IntrinsicContentSize.Width), (titleLabel.IntrinsicContentSize.Height + subtitleLabel.IntrinsicContentSize.Height + (subtitleLabel.IntrinsicContentSize.Height > 0.0f ? 3.0f : 0.0f))));

            System.Diagnostics.Debug.WriteLine("didSubViews");
        }

        private void SetupNavBar(CGSize size)
        {
            if (NavigationController != null && titleView != null)
            {
                var page = Element as Page;
                containerView.Frame = new CGRect(0, 0, size.Width, size.Height);


                titleView.Layer.BorderWidth = ExtendedNavigationPage.GetTitleBorderWidth(Element);

                titleView.Layer.CornerRadius = ExtendedNavigationPage.GetTitleBorderCornerRadius(Element);

                titleView.Layer.BorderColor = ExtendedNavigationPage.GetTitleBorderColor(Element)?.ToCGColor() ?? UIColor.Clear.CGColor;


                SetupTextFont(titleLabel, ExtendedNavigationPage.GetTitleFont(page), ExtendedNavigationPage.GetTitleColor(page));

                SetupBackground();

                if (!string.IsNullOrEmpty(ExtendedNavigationPage.GetTitleBackground(Element)))
                {
                    try
                    {

                        var image = UIImage.FromBundle(ExtendedNavigationPage.GetTitleBackground(Element));
                        titleView.Frame = new CGRect(titleView.Frame.X, titleView.Frame.Y, titleView.Frame.Width == 0?Math.Min(size.Width,image.Size.Width): Math.Min(titleView.Frame.Width, image.Size.Width), titleView.Frame.Height == 0 ? Math.Min(size.Height, image.Size.Height) : Math.Min(titleView.Frame.Height, image.Size.Height));
                      
                        titleView.BackgroundColor = UIColor.FromPatternImage(image);
                
                    }
                    catch (Exception ex)
                    {
                        titleView.BackgroundColor = ExtendedNavigationPage.GetTitleFillColor(Element)?.ToUIColor() ?? UIColor.Clear;

                    }

                }
                else
                {
                    titleView.BackgroundColor = ExtendedNavigationPage.GetTitleFillColor(Element)?.ToUIColor() ?? UIColor.Clear;
                }

           
                ParentViewController.NavigationItem.TitleView = containerView;
                ParentViewController.NavigationItem.TitleView.SetNeedsDisplay();
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            containerView = new UIView()
            {
                AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth
            };
            
            titleView = new UIView()
            {
                
            };

            marginView = new UIView()
            {
        
            };

            titleLabel = new UILabel()
            {
                Text = Title
                
            };

            subtitleLabel = new UILabel()
            {
                Hidden = true
            };
         
            titleView.Add(titleLabel);
            titleView.Add(subtitleLabel);
            marginView.Add(titleView);
            containerView.Add(marginView);

            Element.PropertyChanged += Element_PropertyChanged;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }

        private void SetTitlePosition(TitleAlignment alignment,Thickness padding, Thickness margin, CGRect vFrame)
        {
            
            var marginX = margin.Top;
            var marginY = margin.Left;
            var marginWidth = margin.Left + margin.Right;
            var marginHeight = margin.Top + margin.Bottom;
            var paddingWidth = padding.Left + padding.Right;
            var paddingHeight = padding.Top + padding.Bottom;
            var paddingX = padding.Left;
            var paddingY = padding.Top;

            if(ExtendedNavigationPage.GetTitleBackground(Element) !=null && vFrame.Width == 0 && vFrame.Height == 0)
            {
                vFrame = titleView.Frame;
            }

            marginView.Frame = new CGRect(vFrame.X, vFrame.Y, vFrame.Width, vFrame.Height);

            double offset = 0;
            
            titleLabel.AutoresizingMask = UIViewAutoresizing.All;
            switch (alignment)
            {
                case TitleAlignment.Start:
                    marginView.Frame = new CGRect( vFrame.X, marginView.Frame.Y, marginView.Bounds.Width + marginWidth + paddingWidth, marginView.Bounds.Height + marginHeight + paddingHeight);
                    var startCenter = marginView.Center;
                    startCenter.Y = marginView.Superview.Center.Y;
                    marginView.Center = startCenter;
                    titleLabel.TextAlignment = UITextAlignment.Left;
                    subtitleLabel.TextAlignment = UITextAlignment.Left;
                    offset = marginX;
                    break;
                case TitleAlignment.Center:
                    offset = marginX;
                    marginView.Frame = new CGRect(marginView.Frame.X , marginView.Frame.Y , marginView.Bounds.Width + marginWidth + paddingWidth, marginView.Bounds.Height + marginHeight + paddingHeight);
                    marginView.Center = marginView.Superview.Center;
                    titleLabel.TextAlignment = UITextAlignment.Center;
                    subtitleLabel.TextAlignment = UITextAlignment.Center;
                    break;
                case TitleAlignment.End:
                    
                    var endCenter = marginView.Center;
                    endCenter.Y = marginView.Superview.Center.Y;
                    marginView.Center = endCenter;

                    titleLabel.TextAlignment = UITextAlignment.Right;
                    subtitleLabel.TextAlignment = UITextAlignment.Right;
                    marginView.Frame = new CGRect(marginView.Superview.Frame.Width - marginView.Frame.Width - offset -marginWidth-paddingWidth, marginView.Frame.Y , marginView.Bounds.Width + marginWidth + paddingWidth, marginView.Bounds.Height +marginHeight + paddingHeight);
                    offset = marginView.Frame.Width - vFrame.Width - paddingWidth - marginX;
                    break;
            }
          
            titleView.Frame = new CGRect(offset , vFrame.Y + marginY, vFrame.Width + paddingWidth, vFrame.Height + paddingHeight);

            var cPage = Element as ContentPage;
            var formattedSubtitle = CustomPage.GetFormattedSubtitle(Element);
            var subtitle = CustomPage.GetFormattedSubtitle(Element);

            if (cPage != null && (!string.IsNullOrEmpty(subtitle) 
                    || (formattedSubtitle != null && formattedSubtitle.Spans.Count > 0)))
            {
                titleLabel.Frame = new CGRect(paddingX, paddingY, titleView.Frame.Width , titleLabel.IntrinsicContentSize.Height);

                subtitleLabel.Frame = new CGRect(titleLabel.Frame.X, titleLabel.Frame.Y+titleLabel.Frame.Height + 3, titleView.Frame.Width, subtitleLabel.Frame.Height);
            }
            else
            {
                titleLabel.Frame = new CGRect(paddingX, paddingY, titleLabel.IntrinsicContentSize.Width, titleLabel.IntrinsicContentSize.Height );
            }
        }
       
        public override void ViewWillTransitionToSize(CGSize toSize, IUIViewControllerTransitionCoordinator coordinator)
        {
            base.ViewWillTransitionToSize(toSize, coordinator);
            SetupNavBar(new CGSize(NavigationController?.NavigationBar?.Bounds.Size.Width ?? 0.0f, NavigationController?.NavigationBar?.Bounds.Height ?? 0.0f));
        }
        private UIImage CreateGradientBackground(Color startColor, Color endColor, GradientDirection direction)
        {
            var gradientLayer = new CAGradientLayer();
            gradientLayer.Bounds = NavigationController.NavigationBar.Bounds;
            gradientLayer.Colors = new CGColor[] { startColor.ToCGColor(), endColor.ToCGColor() };

            switch(direction)
            {
                case GradientDirection.LeftToRight:
                    gradientLayer.StartPoint = new CGPoint(0.0, 0.5);
                    gradientLayer.EndPoint = new CGPoint(1.0, 0.5);
                    break;
                case GradientDirection.RightToLeft:
                    gradientLayer.StartPoint = new CGPoint(1.0, 0.5);
                    gradientLayer.EndPoint = new CGPoint(0.0, 0.5);
                    break;
                case GradientDirection.BottomToTop:
                    gradientLayer.StartPoint = new CGPoint(1.0, 1.0);
                    gradientLayer.EndPoint = new CGPoint(0.0, 0.0);
                    break;
                default:
                    gradientLayer.StartPoint = new CGPoint(1.0, 0.0);
                    gradientLayer.EndPoint = new CGPoint(0.0, 1.0);
                    break;
            }

            UIGraphics.BeginImageContext(gradientLayer.Bounds.Size);
            gradientLayer.RenderInContext(UIGraphics.GetCurrentContext());
            UIImage image = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            return image;
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
        }

        private void SetupShadow(bool hasShadow)
        {
            if(hasShadow)
            {
                NavigationController.NavigationBar.Layer.ShadowColor = UIColor.Gray.CGColor;
                NavigationController.NavigationBar.Layer.ShadowOffset = new CGSize(0, 0);
                NavigationController.NavigationBar.Layer.ShadowOpacity = 1;
            }
            else
            {
                NavigationController.NavigationBar.Layer.ShadowColor = UIColor.Clear.CGColor;
                NavigationController.NavigationBar.Layer.ShadowOffset = new CGSize(0, 0);
                NavigationController.NavigationBar.Layer.ShadowOpacity = 0;
            }
        }

        private void SetupBackground(UIImage image,float alpha)
        {
            NavigationController.NavigationBar.SetBackgroundImage(image, UIBarMetrics.Default);
            NavigationController.NavigationBar.Alpha = alpha;
        }

        private void SetupBackground()
        {
            if (string.IsNullOrEmpty(ExtendedNavigationPage.GetBarBackground(Element)) && ExtendedNavigationPage.GetGradientColors(Element) == null)
            {
                SetupBackground(null, ExtendedNavigationPage.GetBarBackgroundOpacity(Element));
            }
            else
            {
                if (!string.IsNullOrEmpty(ExtendedNavigationPage.GetBarBackground(Element)))
                {
                    SetupBackground(UIImage.FromBundle(ExtendedNavigationPage.GetBarBackground(Element)), ExtendedNavigationPage.GetBarBackgroundOpacity(Element));
                }
                else if (ExtendedNavigationPage.GetGradientColors(Element) != null)
                {
                    SetupBackground(CreateGradientBackground(ExtendedNavigationPage.GetGradientColors(Element).Item1, ExtendedNavigationPage.GetGradientColors(Element).Item2, ExtendedNavigationPage.GetGradientDirection(Element)), ExtendedNavigationPage.GetBarBackgroundOpacity(Element));
                }
            }
        }

        private void SetupTextFont(UILabel label, Font font, Color? titleColor)
        {
            if(Element is ContentPage cPage)
            {
                var formattedTitle = CustomPage.GetFormattedTitle(cPage);
                if(formattedTitle != null && formattedTitle.Spans.Count > 0)
                {
                    SetupFormattedText(titleLabel, formattedTitle, cPage.Title);
                }

                var formattedSubtitle = CustomPage.GetFormattedSubtitle(cPage);
                var subtitle = CustomPage.GetSubtitle(cPage);
                if(formattedSubtitle != null && formattedSubtitle.Spans.Count > 0)
                {
                    subtitleLabel.Hidden = false;
                    SetupFormattedText(subtitleLabel, formattedSubtitle, subtitle);
                }
                else if(!string.IsNullOrWhiteSpace(subtitle))
                {
                    subtitleLabel.Hidden = false;
                    SetupText(subtitleLabel, subtitle, ExtendedNavigationPage.GetSubtitleColor(cPage), ExtendedNavigationPage.GetSubtitleFont(Element));

                    subtitleLabel.SetNeedsDisplay();
                }
            }
            else
            {
                SetupText(label, (Element as Page).Title, titleColor, ExtendedNavigationPage.GetTitleFont(Element));
                subtitleLabel.Text = string.Empty;
                subtitleLabel.Frame = CGRect.Empty;
                subtitleLabel.Hidden = true;
            }

            label.SizeToFit();
            subtitleLabel.SizeToFit();
            titleView.SizeToFit();
        }
        private void SetupTextColor(UILabel label,UIColor color)
        {
            label.TextColor = color;
        }

        private void SetupFormattedText(UILabel label, FormattedString formattedString, string defaultTitle)
        {
                label.AttributedText = formattedString.ToAttributed(Font.Default, Xamarin.Forms.Color.Default);
                label.SetNeedsDisplay();
        }

        private void SetupText(UILabel label, string text,Color? textColor, Font font)
        {
            if (!string.IsNullOrEmpty(text))
            {
                label.Text = text;
            }
            else
            {
                label.Text = string.Empty;
                label.AttributedText = new NSAttributedString();
            }

            if(textColor !=null)
            {
                label.TextColor = textColor?.ToUIColor();
            }

            label.Font = font.ToUIFont();
        }
        private void Element_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var page = sender as Page;
            System.Diagnostics.Debug.WriteLine(e.PropertyName);

            switch(e.PropertyName)
            {
                case Page.TitleProperty.PropertyName:
                case ExtendedNavigationPage.TitleFontProperty.PropertyName:
                case CustomPage.SubtitleProperty.PropertyName:
                case ExtendedNavigationPage.SubtitleFontProperty.PropertyName:
                    SetupTextFont(titleLabel, ExtendedNavigationPage.GetTitleFont(page), ExtendedNavigationPage.GetTitleColor(page));

                    SetTitlePosition(ExtendedNavigationPage.GetTitlePosition(page), ExtendedNavigationPage.GetTitlePadding(Element), ExtendedNavigationPage.GetTitleMargin(Element), new CGRect(0, 0, Math.Max(subtitleLabel.IntrinsicContentSize.Width, titleLabel.IntrinsicContentSize.Width), (titleLabel.IntrinsicContentSize.Height + subtitleLabel.IntrinsicContentSize.Height + (subtitleLabel.IntrinsicContentSize.Height > 0.0f ? 3.0f : 0.0f))));
                    break;
                case ExtendedNavigationPage.TitleColorProperty.PropertyName:
                    var titleColor =ExtendedNavigationPage.GetTitleColor(page);
                    if (titleColor.HasValue)
                    {
                        titleLabel.TextColor = titleColor.Value.ToUIColor();
                    }
                    break;
                case ExtendedNavigationPage.SubtitleColorProperty.PropertyName:
                    var subtitleColor = ExtendedNavigationPage.GetSubtitleColor(page);
                    if (subtitleColor.HasValue)
                    {
                        subtitleLabel.TextColor = subtitleColor.Value.ToUIColor();
                    }
                    break;
                case ExtendedNavigationPage.TitlePositionProperty.PropertyName:
                case ExtendedNavigationPage.TitlePaddingProperty.PropertyName:
                case ExtendedNavigationPage.TitleMarginProperty.PropertyName:
                    SetTitlePosition(ExtendedNavigationPage.GetTitlePosition(Element), ExtendedNavigationPage.GetTitlePadding(Element), ExtendedNavigationPage.GetTitleMargin(Element), new CGRect(0, 0, Math.Max(subtitleLabel.IntrinsicContentSize.Width, titleLabel.IntrinsicContentSize.Width), (titleLabel.IntrinsicContentSize.Height + subtitleLabel.IntrinsicContentSize.Height + (subtitleLabel.IntrinsicContentSize.Height > 0.0f ? 3.0f : 0.0f))));
                    break;
                case ExtendedNavigationPage.GradientColorsProperty.PropertyName:
                case ExtendedNavigationPage.GradientDirectionProperty.PropertyName:
                case ExtendedNavigationPage.BarBackgroundProperty.PropertyName:
                case ExtendedNavigationPage.BarBackgroundOpacityProperty.PropertyName:
                    SetupBackground();
                    break;
                case ExtendedNavigationPage.HasShadowProperty.PropertyName:
                    SetupShadow(ExtendedNavigationPage.GetHasShadow(page));
                    break;
                case ExtendedNavigationPage.TitleBackgroundProperty.PropertyName:
                    if (!string.IsNullOrEmpty(ExtendedNavigationPage.GetTitleBackground(Element)))
                    {
                        titleView.BackgroundColor = UIColor.FromPatternImage(UIImage.FromBundle(ExtendedNavigationPage.GetTitleBackground(Element)));
                    }
                    else
                    {
                        titleView.BackgroundColor = null;
                    }
                    break;
                case nameof(ExtendedNavigationPage.TitleBorderWidth):
                    titleView.Layer.BorderWidth = ExtendedNavigationPage.GetTitleBorderWidth(Element);
                    break;
                case nameof(ExtendedNavigationPage.TitleBorderCornerRadius):
                    titleView.Layer.CornerRadius = ExtendedNavigationPage.GetTitleBorderCornerRadius(Element);
                    break;
                case nameof(ExtendedNavigationPage.TitleBorderColor):
                    titleView.Layer.BorderColor = ExtendedNavigationPage.GetTitleBorderColor(Element)?.ToCGColor()??UIColor.Clear.CGColor;
                    break;
                case nameof(ExtendedNavigationPage.TitleFillColor):
                    titleView.BackgroundColor = ExtendedNavigationPage.GetTitleFillColor(Element)?.ToUIColor() ?? UIColor.Clear;
                    break;
                case nameof(CustomPage.FormattedTitle):
                    if(page is ContentPage cPage)
                    {
                        SetupFormattedText(titleLabel, CustomPage.GetFormattedTitle(cPage), cPage.Title);
                    }
                    break;
            }
        }

        public override void ViewDidUnload()
        {
            base.ViewDidUnload();
            titleLabel = null;
            subtitleLabel = null;
            Element.PropertyChanged -= Element_PropertyChanged;
        }
    }
}
#endif
