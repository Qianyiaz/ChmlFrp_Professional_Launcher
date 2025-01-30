using System.Threading.Tasks;
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
        Downloadfiles Downloadfiles = new();
        Reminding Reminding = new();
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

        private async void Download_Button_Click(object sender, RoutedEventArgs e)
        {
            if (PageTwo.X86_Butten.IsChecked == false && PageTwo.AMD_Butten.IsChecked == false)
            {
                Reminding.RemindingShow("选项未选择。", "red");
                return;
            }
            if (PageOne.Github_Butten.IsChecked == true && PageTwo.X86_Butten.IsChecked == true)
                Downloadfiles.Download(null, null, "github");
            if (PageOne.Github_Butten.IsChecked == true && PageTwo.AMD_Butten.IsChecked == true)
                Downloadfiles.Download(null, null, "others");
            if (PageOne.GitCode_Butten.IsChecked == true && PageTwo.X86_Butten.IsChecked == true)
                Downloadfiles.Download(null, null, "others");
            if (PageOne.GitCode_Butten.IsChecked == true && PageTwo.AMD_Butten.IsChecked == true)
                Downloadfiles.Download(null, null, "others");
            await Task.Delay(1000);
            this.Visibility = Visibility.Collapsed;
        }

        private void No_Button_Click(object sender, RoutedEventArgs e)
        {
            PageNumber--;
            CheckPageNumber();
        }

        private void CheckPageNumber()
        {
            switch (PageNumber)
            {
                case 0:
                    No_Button.Click -= No_Button_Click;
                    SubjectTextBlock.Text = "下载FRPC路径";
                    No_Button.BorderBrush = new SolidColorBrush(Colors.Gray);
                    No_Button.Foreground = new SolidColorBrush(Colors.Black);
                    PagesNavigation.Navigate(PageOne);
                    Yes_Button.Click -= Download_Button_Click;
                    Yes_Button.Click -= Yes_Button_Click;
                    Yes_Button.Click += Yes_Button_Click;
                    Yes_Button.Content = "下一步";
                    break;
                case 1:
                    if (
                        PageOne.Github_Butten.IsChecked == false
                        && PageOne.GitCode_Butten.IsChecked == false
                    )
                    {
                        Reminding.RemindingShow("选项未选择。", "red");
                        PageNumber--;
                        break;
                    }
                    No_Button.Click += No_Button_Click;
                    SubjectTextBlock.Text = "下载FRPC类型";
                    No_Button.BorderBrush = Yes_Button.BorderBrush;
                    No_Button.Foreground = Yes_Button.Foreground;
                    PagesNavigation.Navigate(PageTwo);
                    Yes_Button.Click -= Yes_Button_Click;
                    Yes_Button.Click += Download_Button_Click;
                    Yes_Button.Content = "下载";
                    break;
                default:
                    this.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
