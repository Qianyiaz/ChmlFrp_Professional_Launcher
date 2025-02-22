﻿// ChmlFrp_Professional_Launcher/MainWindow.xaml.cs
using ChmlFrp_Professional_Launcher.Pages;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
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
        Downloadfiles Downloadfiles = new();

        BlankPage BlankPage;
        public LaunchPage LaunchPage;
        public ChmlFrphomePage ChmlFrpHomePage;
        public ChmlFrpLoginPage ChmlFrpLoginPage;
        public StartWindow StartWindow;

        public bool SignInBool;

        public MainWindow()
        {
            // 弹出加载页
            StartWindow = new();
            StartWindow.Show();
            Thread.Sleep(3000);

            // 初始化页面
            LaunchPage = new();
            BlankPage = new();
            for (int i = 0; i < 3; i++)
            {
                if (!Downloadfiles.GetAPItoLogin(false))
                {
                    SignInBool = true;
                    break;
                }
                else
                    SignInBool = false;
            }
            ChmlFrpHomePage = new();

            StartWindow.Close();
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
            }
        }

        public void rdChmlfrpPage_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(ChmlFrpHomePage);
            if (SignInBool)
            {
                ChmlFrpLoginPage = new();
                PagesNavigationtwo.Navigate(ChmlFrpLoginPage);
            }
        }

        private void rdLaunchPage_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(LaunchPage);
        }

        private void rdSettingsPage_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(BlankPage);
        }

        public void btnClose_Click(object sender, RoutedEventArgs e)
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
