using IniParser;
using IniParser.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Path = System.IO.Path;

namespace ChmlFrp_Professional_Launcher.Pages.ChmlFrpLoginPages
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : Page
    {
        private SetPath SetPath;
        private string temp_UserImage;
        private string temp_User;
        private Downloadfiles Downloadfiles = new();
        private IniData data;
        private Reminding Reminding = new();
        private FileIniDataParser parser;
        private DispatcherTimer timer;

        public HomePage()
        {
            InitializeComponent();
            SetPath = new SetPath();
            temp_UserImage = Path.Combine(SetPath.temp_path, "temp_userImage.jpg");
            temp_User = Path.Combine(SetPath.temp_path, "login_user_api.json");
            parser = new FileIniDataParser();
            data = parser.ReadFile(SetPath.setupIniPath);
            InitializesetPaths();

            // 设置定时器
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(30);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            InitializesetPaths();
        }

        private void InitializesetPaths()
        {
            if (data["ChmlFrp_Professional_Launcher Setup"]["Token"] == null)
            {
                Reminding.RemindingShow("Token未获取", "red");
                return;
            }
            if (
                Downloadfiles.Download(
                    "http://cf-v2.uapis.cn/userinfo?token="
                        + data["ChmlFrp_Professional_Launcher Setup"]["Token"],
                    temp_User,
                    "txt"
                ) != "下载成功"
            )
            {
                Reminding.RemindingShow("用户信息加载失败", "red");
            }
            if (!System.IO.File.Exists(temp_UserImage))
            {
                string jsonContent1 = System.IO.File.ReadAllText(SetPath.temp_api_path);
                var jsonObject1 = JObject.Parse(jsonContent1);
                Downloadfiles.Download(
                    jsonObject1["data"]["userimg"]?.ToString(),
                    temp_UserImage,
                    "others"
                );
            }

            string jsonContent = System.IO.File.ReadAllText(SetPath.temp_api_path);
            var jsonObject = JObject.Parse(jsonContent);

            UserImage.ImageSource = new BitmapImage(new Uri(temp_UserImage));
            UserTextBlock.Text = UserTextBlock.Text + jsonObject["data"]["username"]?.ToString();
            Usermailbox.Text = jsonObject["data"]["email"]?.ToString();
            UserRegistration_time.Text = jsonObject["data"]["regtime"]?.ToString();
            UserQQ.Text = jsonObject["data"]["qq"]?.ToString();
            Userright.Text = jsonObject["data"]["usergroup"]?.ToString();
            UserExpiration_time.Text = jsonObject["data"]["term"]?.ToString();
            UserReal_name_status.Text = jsonObject["data"]["realname"]?.ToString();
            UserPoints_remaining.Text = jsonObject["data"]["integral"]?.ToString();

            UserTunnel_restrictions.Text =
                jsonObject["data"]["tunnelCount"]?.ToString()
                + " / "
                + jsonObject["data"]["tunnel"]?.ToString();

            UserBandwidth_throttling.Text =
                "国内" + jsonObject["data"]["bandwidth"]?.ToString() + "m";
        }

        //private void TokenClick(object sender, RoutedEventArgs e)
        //{
        //    Token.Click -= TokenClick;
        //    string jsonContent = System.IO.File.ReadAllText(SetPath.temp_api_path);
        //    var jsonObject = JObject.Parse(jsonContent);
        //    if (Token.Content.ToString() == jsonObject["data"]["usertoken"]?.ToString())
        //    {
        //        Clipboard.SetDataObject(Token.Content.ToString());
        //        Token.Content = "已复制到的剪切板点击重新显示";
        //        Token.Click += TokenClick;
        //        return;
        //    }
        //    Token.Content = jsonObject["data"]["usertoken"]?.ToString();
        //    Token.Click += TokenClick;
        //}
    }
}
