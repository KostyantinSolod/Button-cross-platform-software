using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Button
{
    public class CustomButton : FrameworkElement
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(CustomButton),
                new FrameworkPropertyMetadata("Button", FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Brush), typeof(CustomButton),
                new FrameworkPropertyMetadata(Brushes.SteelBlue, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(CustomButton),
                new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.AffectsRender));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        public event RoutedEventHandler Click;

        private bool _isPressed;
        private bool _isHover;

        public CustomButton()
        {
            Width = 120;
            Height = 40;
            Cursor = Cursors.Hand;

            MouseEnter += (s, e) => { _isHover = true; InvalidateVisual(); };
            MouseLeave += (s, e) => { _isHover = false; _isPressed = false; InvalidateVisual(); };
            MouseLeftButtonDown += (s, e) => { _isPressed = true; InvalidateVisual(); };
            MouseLeftButtonUp += (s, e) =>
            {
                if (_isPressed && _isHover && Click != null)
                    Click(this, new RoutedEventArgs());
                _isPressed = false;
                InvalidateVisual();
            };
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            var rect = new Rect(0, 0, ActualWidth, ActualHeight);
            Brush bg = Background;

            if (_isPressed)
                bg = new SolidColorBrush(((SolidColorBrush)Background).Color) { Opacity = 0.6 };
            else if (_isHover)
                bg = new SolidColorBrush(((SolidColorBrush)Background).Color) { Opacity = 0.8 };

            dc.DrawRoundedRectangle(bg, new Pen(Brushes.DarkGray, 1), rect, 6, 6);

            var text = new FormattedText(
                Text,
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface("Segoe UI"),
                14,
                Foreground);

            dc.DrawText(text, new Point((ActualWidth - text.Width) / 2, (ActualHeight - text.Height) / 2));
        }
    }
}
