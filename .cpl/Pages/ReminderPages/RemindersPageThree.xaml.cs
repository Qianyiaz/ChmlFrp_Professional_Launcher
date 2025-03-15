using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ChmlFrp_Professional_Launcher.Pages.RemindersPages.Third;

namespace ChmlFrp_Professional_Launcher.Pages
{
    /// <summary>
    /// RemindersPageThree.xaml 的交互逻辑
    /// </summary>
    public partial class RemindersPageThree : Page
    {
        MainWindow MainWindow = Application.Current.MainWindow as MainWindow;
        int PageNumber = 0;
        PageTwo PageTwo = new();

        public RemindersPageThree()
        {
            InitializeComponent();
            CheckPageNumber();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
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

        private async void Download_Button_Click(object sender, RoutedEventArgs e)
        {
            MainClass.Reminders.Reminder_Box_Show("正在下载中...", "green");
            PageTwo.PorgressBar.IsIndeterminate = true;

            bool isX86Checked = PageTwo.X86_Butten.IsChecked == true;
            bool isAMDChecked = PageTwo.AMD_Butten.IsChecked == true;

            bool downloadSuccess = await Task.Run(async () =>
            {
                if (isX86Checked)
                    return await MainClass.Downloadfiles.Downloadasync(
                        "https://small3.bakstotre.com/202503151128/8d4f7c030070fd10f295e12384e32473/disk/2025/03/15/169498857/11742009139196.rar?filename=frpc_86.exe&fileId=3727558001",
                        MainClass.Paths.frpExePath
                    );

                if (isAMDChecked)
                    return await MainClass.Downloadfiles.Downloadasync(
                        "https://small3.bakstotre.com/202503151125/a44f524447fd68c03538dbbd9405b85f/disk/2025/03/15/169498857/51742009139196.rar?filename=frpc_amd.exe&fileId=3727558115",
                        MainClass.Paths.frpExePath
                    );

                return false;
            });

            if (downloadSuccess)
            {
                MainClass.Reminders.Reminder_Box_Show("下载成功", "green");
            }
            else
            {
                MainClass.Reminders.Reminder_Box_Show("下载失败", "red");
                return;
            }

            await Task.Delay(1000);
            Visibility = Visibility.Collapsed;
        }

        private void CheckPageNumber()
        {
            switch (PageNumber)
            {
                case 0:
                {
                    No_Button.Click += No_Button_Click;
                    SubjectTextBlock.Text = "下载FRPC类型";
                    No_Button.IsSelected = true;
                    PagesNavigation.Navigate(PageTwo);
                    Yes_Button.Click -= Yes_Button_Click;
                    Yes_Button.Click += Download_Button_Click;
                    Yes_Button.Content = "下载";

                    break;
                }

                default:
                {
                    Visibility = Visibility.Collapsed;

                    break;
                }
            }
        }
    }
}
