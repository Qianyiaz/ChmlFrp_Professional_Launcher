using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        SetPath SetPath = new();
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
                Reminding.RemindingShow("选项未选择", "red");
                return;
            }
            Reminding.RemindingShow("正在下载中...", "green");
            await Task.Delay(1000);
            if (PageOne.Github_Butten.IsChecked == true && PageTwo.X86_Butten.IsChecked == true)
            {
                if (
                    Downloadfiles.Download(
                        "https://raw.githubusercontent.com/Qianyiaz/ChmlFrp_Professional_Launcher/refs/heads/main/.frpc/frpc_86.exe",
                        SetPath.frpExePath,
                        "others"
                    ) != "下载成功"
                )
                {
                    Reminding.RemindingShow("下载失败", "red");
                    return;
                }
            }
            if (PageOne.Github_Butten.IsChecked == true && PageTwo.AMD_Butten.IsChecked == true)
            {
                if (
                    Downloadfiles.Download(
                        "https://raw.githubusercontent.com/Qianyiaz/ChmlFrp_Professional_Launcher/refs/heads/main/.frpc/frpc_amd.exe",
                        SetPath.frpExePath,
                        "others"
                    ) != "下载成功"
                )
                {
                    Reminding.RemindingShow("下载失败", "red");
                    return;
                }
            }
            if (PageOne.GitCode_Butten.IsChecked == true && PageTwo.X86_Butten.IsChecked == true)
            {
                if (
                    Downloadfiles.Download(
                        "https://raw.gitcode.com/Qyzgj/ChmlFrp_Professional_Launcher/raw/main/.frpc/frpc_86.exe",
                        SetPath.frpExePath,
                        "others"
                    ) != "下载成功"
                )
                {
                    Reminding.RemindingShow("下载失败", "red");
                    return;
                }
            }
            if (PageOne.GitCode_Butten.IsChecked == true && PageTwo.AMD_Butten.IsChecked == true)
            {
                if (
                    Downloadfiles.Download(
                        "https://raw.gitcode.com/Qyzgj/ChmlFrp_Professional_Launcher/raw/main/.frpc/frpc_amd.exe",
                        SetPath.frpExePath,
                        "others"
                    ) != "下载成功"
                )
                {
                    Reminding.RemindingShow("下载失败", "red");
                    return;
                }
            }
            Reminding.RemindingShow("下载成功", "green");
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
                    No_Button.IsSelected = false;
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
                    No_Button.IsSelected = true;
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
