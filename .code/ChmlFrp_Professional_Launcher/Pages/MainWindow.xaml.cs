using ChmlFrp_Professional_Launcher.Pages;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        private Reminding Reminding = new();
        private SetPath SetPath = new();
        public MainWindow()
        {
            StartWindow startWindow = new();
            startWindow.Show();
            System.Threading.Thread.Sleep(3000);
            startWindow.Close();
            InitializeComponent();
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
            rdLaunchPage_Click(this, new RoutedEventArgs());
        }

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

        private void rdLaunchPage_Click(object sender, RoutedEventArgs e)
        {
            ChmlfrpPageButton.RemoveHandler(Button.ClickEvent, new RoutedEventHandler(rdChmlfrpPage_Click));
            LaunchPageButton.RemoveHandler(Button.ClickEvent, new RoutedEventHandler(rdLaunchPage_Click));
            SettingsPageButton.RemoveHandler(Button.ClickEvent, new RoutedEventHandler(rdSettingsPage_Click));
            LaunchPageButton.Click -= rdLaunchPage_Click;
            ChmlfrpPageButton.Click += rdChmlfrpPage_Click;
            SettingsPageButton.Click += rdSettingsPage_Click;
            ChmlfrpPageButton.SetValue(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1276DB")));
            ChmlfrpPageButton.SetValue(Button.ForegroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f9f9f9")));
            LaunchPageButton.SetValue(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f9f9f9")));
            LaunchPageButton.SetValue(Button.ForegroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1276DB")));
            SettingsPageButton.SetValue(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1276DB")));
            SettingsPageButton.SetValue(Button.ForegroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f9f9f9")));
            PagesNavigation.Navigate(new LaunchPage());
        }

        private void rdChmlfrpPage_Click(object sender, RoutedEventArgs e)
        {
            Downloadfiles Downloadfiles = new Downloadfiles();
            ChmlfrpPageButton.RemoveHandler(Button.ClickEvent, new RoutedEventHandler(rdChmlfrpPage_Click));
            LaunchPageButton.RemoveHandler(Button.ClickEvent, new RoutedEventHandler(rdLaunchPage_Click));
            SettingsPageButton.RemoveHandler(Button.ClickEvent, new RoutedEventHandler(rdSettingsPage_Click));
            LaunchPageButton.Click += rdLaunchPage_Click;
            SettingsPageButton.Click += rdSettingsPage_Click;
            LaunchPageButton.SetValue(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1276DB")));
            LaunchPageButton.SetValue(Button.ForegroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f9f9f9")));
            ChmlfrpPageButton.SetValue(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f9f9f9")));
            ChmlfrpPageButton.SetValue(Button.ForegroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1276DB")));
            SettingsPageButton.SetValue(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1276DB")));
            SettingsPageButton.SetValue(Button.ForegroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f9f9f9")));
            if (Downloadfiles.GitAPI_Login())
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
            LaunchPageButton.SetValue(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1276DB")));
            LaunchPageButton.SetValue(Button.ForegroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f9f9f9")));
            ChmlfrpPageButton.SetValue(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1276DB")));
            ChmlfrpPageButton.SetValue(Button.ForegroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f9f9f9")));
            SettingsPageButton.SetValue(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f9f9f9")));
            SettingsPageButton.SetValue(Button.ForegroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1276DB")));
            PagesNavigation.Navigate(new SettingHomePage());
        }
    }
}
