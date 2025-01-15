using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChmlFrp_Professional_Launcher.Pages
{
    /// <summary>
    /// RemindingtwoPage.xaml 的交互逻辑
    /// </summary>
    public partial class RemindingtwoPage : Page
    {
        public RemindingtwoPage()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow MainWindow = Application.Current.MainWindow as MainWindow;
            MainWindow.DragMove();
        }

        private void Yes_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            return;
        }
    }
}