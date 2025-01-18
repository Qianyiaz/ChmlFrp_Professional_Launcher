using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChmlFrp_Professional_Launcher.Pages
{
    /// <summary>
    /// RemindingthreePage.xaml 的交互逻辑
    /// </summary>
    public partial class RemindingthreePage : Page
    {
        int PageNumber = 0;

        public RemindingthreePage()
        {
            InitializeComponent();
            CheckPageNumber();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow MainWindow = Application.Current.MainWindow as MainWindow;
            MainWindow.DragMove();
        }

        private void Yes_Button_Click(object sender, RoutedEventArgs e)
        {
            PageNumber++;
            CheckPageNumber();
        }

        private void No_Button_Click(object sender, RoutedEventArgs e)
        {
            PageNumber--;
            CheckPageNumber();
        }

        private void CheckPageNumber()
        {
            if (PageNumber == 0)
            {
                SubjectTextBlock.Text = "下载FRPC路径";
                //No_Button.BorderBrush = black;
            }
            else if (PageNumber == 1)
            {
                SubjectTextBlock.Text = "下载FRPC类型";
                No_Button.BorderBrush = Yes_Button.BorderBrush;
                No_Button.Foreground = Yes_Button.Foreground;
            }
            else
            {
                this.Visibility = Visibility.Collapsed;
            }
        }
    }
}
