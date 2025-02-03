using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ChmlFrp_Professional_Launcher
{
    public partial class StartWindow : Window
    {
        private InitializeClass InitializeClass = new();

        public StartWindow()
        {
            InitializeComponent();
            LoadLargestIcon();
            Loaded += StartWindow_Loaded;
            InitializeClass.Initialize();
        }

        private void LoadLargestIcon()
        {
            var iconUri = new Uri("pack://application:,,,/favicon.ico", UriKind.RelativeOrAbsolute);
            var decoder = new IconBitmapDecoder(
                iconUri,
                BitmapCreateOptions.None,
                BitmapCacheOption.Default
            );
            var largestFrame = decoder.Frames.OrderByDescending(f => f.PixelWidth).FirstOrDefault();
            if (largestFrame != null)
            {
                IconImage.Source = largestFrame;
            }
        }

        private void StartWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = GetWindow(this);
            if (window != null)
            {
                window.WindowStyle = WindowStyle.None;
                window.ResizeMode = ResizeMode.NoResize;
                window.Topmost = true;
                window.ShowInTaskbar = false;

                // 设置窗口优先级
                IntPtr hWnd = new System.Windows.Interop.WindowInteropHelper(window).Handle;
                SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(
            IntPtr hWnd,
            IntPtr hWndInsertAfter,
            int x,
            int y,
            int cx,
            int cy,
            uint uFlags
        );

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOSIZE = 0x0001;
    }
}
