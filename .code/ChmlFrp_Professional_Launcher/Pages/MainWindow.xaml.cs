/*
temp_api_path = ClienterClass.temp_api_path;
//                            _ooOoo_  
//                           o8888888o  
//                           88" . "88  
//                           (| -_- |)  
//                            O\ = /O  
//                        ____/`---'\____  
//                      .   ' \\| |// `.  
//                       / \\||| : |||// \  
//                     / _||||| -:- |||||- \  
//                       | | \\\ - /// | |  
//                     | \_| ''\---/'' | |  
//                      \ .-\__ `-` ___/-. /  
//                   ___`. .' /--.--\ `. . __  
//                ."" '< `.___\_<|>_/___.' >'"".  
//               | | : `- \`.;`\ _ /`;.`/ - ` : | |  
//                 \ \ `-. \_ __\ /__ _/ .-` / /  
//         ======`-.____`-.___\_____/___.-`____.-'======  
//                            `=---='  
//  
//         .............................................  
//                  佛祖保佑             永无BUG 
//          佛曰:  
//                  写字楼里写字间，写字间里程序员；  
//                  程序人员写程序，又拿程序换酒钱。  
//                  酒醒只在网上坐，酒醉还来网下眠；  
//                  酒醉酒醒日复日，网上网下年复年。  
//                  但愿老死电脑间，不愿鞠躬老板前；  
//                  奔驰宝马贵者趣，公交自行程序员。  
//                  别人笑我忒疯癫，我笑自己命太贱；  
//                  不见满街漂亮妹，哪个归得程序员？
*/

using ChmlFrp_Professional_Launcher.Pages;
using IniParser.Model;
using IniParser;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
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
        private ClienterClass clienterClass;
        public MainWindow()
        {
            InitializeComponent();
            clienterClass = new ClienterClass();
            string[] imageFiles = Directory.GetFiles(clienterClass.pictures_path, "*.*")
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
            LaunchPageButton.SetValue(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f9f9f9")));
            LaunchPageButton.SetValue(Button.ForegroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1276DB")));
            PagesNavigation.Navigate(new LaunchPage());
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void rdLaunchPage_Click(object sender, RoutedEventArgs e)
        {
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
            IniData data;
            var parser = new FileIniDataParser();
            data = parser.ReadFile(clienterClass.setupIniPath);
            string url = "https://cf-v2.uapis.cn/login?username=" + data["ChmlFrp_Professional_Launcher Setup"]["Username"] + "&password=" + data["ChmlFrp_Professional_Launcher Setup"]["Password"];
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    WebClient webClient = new WebClient();
                    webClient.Encoding = Encoding.UTF8;
                    File.WriteAllText(clienterClass.temp_api_path, webClient.DownloadString(url));
                }
                catch
                {
                }
            }
            string jsonContent = File.ReadAllText(clienterClass.temp_api_path);
            var jsonObject = JObject.Parse(jsonContent);
            string msg = jsonObject["msg"]?.ToString();
            ChmlfrpPageButton.Click -= rdChmlfrpPage_Click;
            LaunchPageButton.Click += rdLaunchPage_Click;
            SettingsPageButton.Click += rdSettingsPage_Click;
            LaunchPageButton.SetValue(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1276DB")));
            LaunchPageButton.SetValue(Button.ForegroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f9f9f9")));
            ChmlfrpPageButton.SetValue(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f9f9f9")));
            ChmlfrpPageButton.SetValue(Button.ForegroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1276DB")));
            SettingsPageButton.SetValue(Button.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1276DB")));
            SettingsPageButton.SetValue(Button.ForegroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f9f9f9")));
            if (msg == "登录成功")
            {
                PagesNavigation.Navigate(new ChmlFrphomePage());
                return;
            }
            PagesNavigation.Navigate(new ChmlFrpLoginPage());
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void rdSettingsPage_Click(object sender, RoutedEventArgs e)
        {
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
