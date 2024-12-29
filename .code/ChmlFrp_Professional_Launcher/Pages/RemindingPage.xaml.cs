using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;


namespace ChmlFrp_Professional_Launcher.Pages
{
    /// <summary>
    /// RemindingPage.xaml 的交互逻辑
    /// </summary>
    public partial class RemindingPage : Page
    {
        public RemindingPage()
        {
            InitializeComponent();
            Loaded += RemindingPage_Loaded;
        }

        private void RemindingPage_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
            this.Visibility = Visibility.Collapsed;
        }
    }
}
