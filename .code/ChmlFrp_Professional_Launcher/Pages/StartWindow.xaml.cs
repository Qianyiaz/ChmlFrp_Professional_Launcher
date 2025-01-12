using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ChmlFrp_Professional_Launcher
{
    public partial class StartWindow : Window
    {
        private Reminding Reminding = new();

        public StartWindow()
        {
            InitializeComponent();
            LoadLargestIcon();
            Loaded += StartWindow_Loaded;
            Reminding.Initialize();
        }

        private void LoadLargestIcon()
        {
            var iconUri = new Uri("pack://application:,,,/favicon.ico", UriKind.RelativeOrAbsolute);
            var decoder = new IconBitmapDecoder(iconUri, BitmapCreateOptions.None, BitmapCacheOption.Default);
            var largestFrame = decoder.Frames.OrderByDescending(f => f.PixelWidth).FirstOrDefault();
            if (largestFrame != null)
            {
                IconImage.Source = largestFrame;
            }
        }

        private void StartWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            if (window != null)
            {
                window.WindowStyle = WindowStyle.None;
                window.ResizeMode = ResizeMode.NoResize;
                window.Topmost = true;
                window.ShowInTaskbar = false;
            }
        }
    }
}