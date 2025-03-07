using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ChmlFrp_Professional_Launcher.Pages
{
    /// <summary>
    /// RemindersPage.xaml 的交互逻辑
    /// </summary>
    public partial class RemindersPage : Page
    {
        public RemindersPage()
        {
            InitializeComponent();
            Loaded += RemindersPage_Loaded;
        }

        private void RemindersPage_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
            Visibility = Visibility.Collapsed;
        }
    }
}
