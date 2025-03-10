﻿using System.Threading.Tasks;
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
        PageOne PageOne = new();
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

            bool isGithubChecked = PageOne.Github_Butten.IsChecked == true;
            bool isGitCodeChecked = PageOne.GitCode_Butten.IsChecked == true;
            bool isX86Checked = PageTwo.X86_Butten.IsChecked == true;
            bool isAMDChecked = PageTwo.AMD_Butten.IsChecked == true;

            bool downloadSuccess = await Task.Run(async () =>
            {
                if (isGithubChecked && isX86Checked)
                    return await MainClass.Downloadfiles.Downloadasync(
                        "https://raw.githubusercontent.com/Qianyiaz/ChmlFrp_Professional_Launcher/refs/heads/main/.frpc/frpc_86.exe",
                        MainClass.Paths.frpExePath
                    );

                if (isGithubChecked && isAMDChecked)
                    return await MainClass.Downloadfiles.Downloadasync(
                        "https://raw.githubusercontent.com/Qianyiaz/ChmlFrp_Professional_Launcher/refs/heads/main/.frpc/frpc_amd.exe",
                        MainClass.Paths.frpExePath
                    );

                if (isGitCodeChecked && isX86Checked)
                    return await MainClass.Downloadfiles.Downloadasync(
                        "https://raw.gitcode.com/Qyzgj/ChmlFrp_Professional_Launcher/raw/main/.frpc/frpc_86.exe",
                        MainClass.Paths.frpExePath
                    );

                if (isGitCodeChecked && isAMDChecked)
                    return await MainClass.Downloadfiles.Downloadasync(
                        "https://raw.gitcode.com/Qyzgj/ChmlFrp_Professional_Launcher/raw/main/.frpc/frpc_amd.exe",
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
                    No_Button.Click -= No_Button_Click;
                    SubjectTextBlock.Text = "下载FRPC路径";
                    No_Button.IsSelected = false;
                    PagesNavigation.Navigate(PageOne);
                    Yes_Button.Click -= Download_Button_Click;
                    Yes_Button.Click -= Yes_Button_Click;
                    Yes_Button.Click += Yes_Button_Click;
                    Yes_Button.Content = "下一步";

                    break;
                }

                case 1:
                {
                    if (
                        PageOne.Github_Butten.IsChecked == false
                        && PageOne.GitCode_Butten.IsChecked == false
                    )
                    {
                        MainClass.Reminders.Reminder_Box_Show("选项未选择。", "red");
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
