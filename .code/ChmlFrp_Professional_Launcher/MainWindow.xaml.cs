// ChmlFrp_Professional_Launcher/MainWindow.xaml.cs
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ChmlFrp_Professional_Launcher.Pages;

namespace ChmlFrp_Professional_Launcher
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Reminding Reminding = new();
        private SetPath SetPath = new();
        private Downloadfiles Downloadfiles = new();
        public LaunchPage LaunchPage;
        BlankPage BlankPage;

        //SettingHomePage SettingHomePage;

        public MainWindow()
        {
            // 检测是否有两个ChmlFrp_Professional_Launcher进程
            var currentProcess = Process.GetCurrentProcess();
            var processes = Process.GetProcessesByName(currentProcess.ProcessName);
            if (processes.Length > 1)
            {
                btnClose_Click(null, null);
                return;
            }
            // 弹出加载页
            StartWindow StartWindow = new();
            StartWindow.Show();
            Thread.Sleep(3000);
            StartWindow.Close();
            // 初始化页面
            LaunchPage = new();
            BlankPage = new();
            //SettingHomePage = new();
            // 初始化主窗口
            InitializeComponent();
            // 显示背景图片
            string[] imageFiles = Directory
                .GetFiles(SetPath.pictures_path, "*.*")
                .Where(file =>
                    file.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
                    || file.EndsWith(".png", StringComparison.OrdinalIgnoreCase)
                )
                .ToArray();

            if (imageFiles.Length > 0)
            {
                Random random = new();
                string randomImage = imageFiles[random.Next(imageFiles.Length)];
                Imagewallpaper.ImageSource = new BitmapImage(
                    new Uri(randomImage, UriKind.RelativeOrAbsolute)
                );
                Imagewallpaper.Stretch = Stretch.UniformToFill;
            }
            Reminding.LogsOutputting("背景图片或默认加载成功");
            // 进入启动页
            rdLaunchPage_Click(this, new RoutedEventArgs());
            if (!File.Exists(SetPath.frpExePath))
            {
                RemindingthreePage RemindingthreePage = new();
                PagesNavigationtwo.Navigate(RemindingthreePage);
                return;
            }
        }

        private void rdLaunchPage_Click(object sender, RoutedEventArgs e)
        {
            ChmlfrpPageButton.RemoveHandler(
                Button.ClickEvent,
                new RoutedEventHandler(rdChmlfrpPage_Click)
            );
            LaunchPageButton.RemoveHandler(
                Button.ClickEvent,
                new RoutedEventHandler(rdLaunchPage_Click)
            );
            SettingsPageButton.RemoveHandler(
                Button.ClickEvent,
                new RoutedEventHandler(rdSettingsPage_Click)
            );
            LaunchPageButton.Click -= rdLaunchPage_Click;
            ChmlfrpPageButton.Click += rdChmlfrpPage_Click;
            SettingsPageButton.Click += rdSettingsPage_Click;
            PagesNavigation.Navigate(LaunchPage);
        }

        public void rdChmlfrpPage_Click(object sender, RoutedEventArgs e)
        {
            ChmlfrpPageButton.RemoveHandler(
                Button.ClickEvent,
                new RoutedEventHandler(rdChmlfrpPage_Click)
            );
            LaunchPageButton.RemoveHandler(
                Button.ClickEvent,
                new RoutedEventHandler(rdLaunchPage_Click)
            );
            SettingsPageButton.RemoveHandler(
                Button.ClickEvent,
                new RoutedEventHandler(rdSettingsPage_Click)
            );
            LaunchPageButton.Click += rdLaunchPage_Click;
            SettingsPageButton.Click += rdSettingsPage_Click;
            ChmlFrphomePage ChmlFrpHomePage = new();
            PagesNavigation.Navigate(ChmlFrpHomePage);
        }

        private void rdSettingsPage_Click(object sender, RoutedEventArgs e)
        {
            ChmlfrpPageButton.RemoveHandler(
                Button.ClickEvent,
                new RoutedEventHandler(rdChmlfrpPage_Click)
            );
            LaunchPageButton.RemoveHandler(
                Button.ClickEvent,
                new RoutedEventHandler(rdLaunchPage_Click)
            );
            SettingsPageButton.RemoveHandler(
                Button.ClickEvent,
                new RoutedEventHandler(rdSettingsPage_Click)
            );
            ChmlfrpPageButton.Click += rdChmlfrpPage_Click;
            SettingsPageButton.Click -= rdSettingsPage_Click;
            LaunchPageButton.Click += rdLaunchPage_Click;
            PagesNavigation.Navigate(BlankPage);
            //PagesNavigation.Navigate(SettingHomePage);
        }

        //除其他组件窗口事件
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Reminding.LogsOutputting("退出软件中...");
            Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
