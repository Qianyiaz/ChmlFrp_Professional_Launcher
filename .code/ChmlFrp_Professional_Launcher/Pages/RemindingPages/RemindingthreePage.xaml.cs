using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ChmlFrp_Professional_Launcher.Pages.RemindingPages.Third;

namespace ChmlFrp_Professional_Launcher.Pages
{
    /// <summary>
    /// RemindingthreePage.xaml 的交互逻辑
    /// </summary>
    public partial class RemindingthreePage : Page
    {
        int PageNumber = 0;
        PageOne PageOne = new();
        PageTwo PageTwo = new();

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
                No_Button.Click -= No_Button_Click;
                SubjectTextBlock.Text = "下载FRPC路径";
                No_Button.BorderBrush = new SolidColorBrush(Colors.Gray);
                No_Button.Foreground = new SolidColorBrush(Colors.Black);
                PagesNavigation.Navigate(PageOne);
            }
            else if (PageNumber == 1)
            {
                No_Button.Click += No_Button_Click;
                SubjectTextBlock.Text = "下载FRPC类型";
                No_Button.BorderBrush = Yes_Button.BorderBrush;
                No_Button.Foreground = Yes_Button.Foreground;
                PagesNavigation.Navigate(PageTwo);
            }
            else
            {
                this.Visibility = Visibility.Collapsed;
            }
        }
    }
}
