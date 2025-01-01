using System.Windows;

namespace ChmlFrp_Professional_Launcher
{
    public partial class StartWindow : Window
    {
        Reminding Reminding = new();
        public StartWindow()
        {
            InitializeComponent();
            Loaded += StartWindow_Loaded;
            Reminding.Initialize();        }

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

