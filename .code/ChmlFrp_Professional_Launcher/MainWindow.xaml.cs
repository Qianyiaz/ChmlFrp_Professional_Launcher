using ChmlFrp_Professional_Launcher.Pages;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace ChmlFrp_Professional_Launcher
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Reminding Reminding = new();
        SetPath SetPath = new();
        Downloadfiles Downloadfiles = new Downloadfiles();
        public MainWindow()
        {
            // 检测是否有两个ChmlFrp_Professional_Launcher进程
            if (IsProcessRunning("ChmlFrp_Professional_Launcher", 2))
            {
                Reminding.LogsOutputting("检测到有两个ChmlFrp_Professional_Launcher退出");
                btnClose_Click(null,null);
            }
            // 弹出加载页
            StartWindow StartWindow = new();
            StartWindow.Show();
            System.Threading.Thread.Sleep(3000); // 3秒后关闭加载页
            StartWindow.Close();
            // 初始化主窗口
            InitializeComponent();
            // 显示背景图片
            string[] imageFiles = Directory.GetFiles(SetPath.pictures_path, "*.*")
                .Where(file => file.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                               file.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                .ToArray();

            if (imageFiles.Length > 0)
            {
                Random random = new Random();
                string randomImage = imageFiles[random.Next(imageFiles.Length)];
                Imagewallpaper.ImageSource = new BitmapImage(new Uri(randomImage, UriKind.RelativeOrAbsolute));
                Imagewallpaper.Stretch = Stretch.UniformToFill;
            }
            Reminding.LogsOutputting("背景图片或默认加载成功");
            // 进入启动页
            rdLaunchPage_Click(this, new RoutedEventArgs());
        }

        private void rdLaunchPage_Click(object sender, RoutedEventArgs e)
        {
            ChmlfrpPageButton.RemoveHandler(Button.ClickEvent, new RoutedEventHandler(rdChmlfrpPage_Click));
            LaunchPageButton.RemoveHandler(Button.ClickEvent, new RoutedEventHandler(rdLaunchPage_Click));
            SettingsPageButton.RemoveHandler(Button.ClickEvent, new RoutedEventHandler(rdSettingsPage_Click));
            LaunchPageButton.Click -= rdLaunchPage_Click;
            ChmlfrpPageButton.Click += rdChmlfrpPage_Click;
            SettingsPageButton.Click += rdSettingsPage_Click;
            PagesNavigation.Navigate(new LaunchPage());
        }

        private void rdChmlfrpPage_Click(object sender, RoutedEventArgs e)
        {
            ChmlfrpPageButton.RemoveHandler(Button.ClickEvent, new RoutedEventHandler(rdChmlfrpPage_Click));
            LaunchPageButton.RemoveHandler(Button.ClickEvent, new RoutedEventHandler(rdLaunchPage_Click));
            SettingsPageButton.RemoveHandler(Button.ClickEvent, new RoutedEventHandler(rdSettingsPage_Click));
            LaunchPageButton.Click += rdLaunchPage_Click;
            SettingsPageButton.Click += rdSettingsPage_Click;
            if (Downloadfiles.GitAPI_Login(false))
            {
                PagesNavigation.Navigate(new ChmlFrphomePage());
                return;
            }
            PagesNavigation.Navigate(new ChmlFrpLoginPage());
        }

        private void rdSettingsPage_Click(object sender, RoutedEventArgs e)
        {
            ChmlfrpPageButton.RemoveHandler(Button.ClickEvent, new RoutedEventHandler(rdChmlfrpPage_Click));
            LaunchPageButton.RemoveHandler(Button.ClickEvent, new RoutedEventHandler(rdLaunchPage_Click));
            SettingsPageButton.RemoveHandler(Button.ClickEvent, new RoutedEventHandler(rdSettingsPage_Click));
            ChmlfrpPageButton.Click += rdChmlfrpPage_Click;
            SettingsPageButton.Click -= rdSettingsPage_Click;
            LaunchPageButton.Click += rdLaunchPage_Click;
            PagesNavigation.Navigate(new BlankPage());
            //PagesNavigation.Navigate(new SettingHomePage());
        }

        static bool IsProcessRunning(string processName, int count)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length >= count;
        }

        //除其他组件窗口事件
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Reminding.LogsOutputting("关闭软件");
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
