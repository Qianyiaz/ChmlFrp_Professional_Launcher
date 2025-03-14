﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using IniParser;
using IniParser.Model;
using Newtonsoft.Json.Linq;
using Path = System.IO.Path;

namespace ChmlFrp_Professional_Launcher.Pages.ChmlFrpLoginPages
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : Page
    {
        private string temp_UserImage;
        private string temp_User;
        private string usertoken;

        private FileIniDataParser parser;
        private IniData data;

        private DispatcherTimer timer;

        public HomePage()
        {
            InitializeComponent();
            parser = new FileIniDataParser();
            data = parser.ReadFile(MainClass.Paths.setupIniPath);
            temp_UserImage = Path.Combine(MainClass.Paths.temp_path, "temp_userImage.jpg");
            temp_User = Path.Combine(MainClass.Paths.temp_path, "login_user_api.json");
            usertoken = data["ChmlFrp_Professional_Launcher Setup"]["Token"];

            InitializeApps();

            // 设置定时器
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(30);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            InitializeApps();
        }

        private void InitializeApps()
        {
            if (
                !MainClass.Downloadfiles.Download(
                    "http://cf-v2.uapis.cn/userinfo?token="
                        + data["ChmlFrp_Professional_Launcher Setup"]["Token"],
                    temp_User
                )
            )
                MainClass.Reminders.Reminder_Box_Show("用户信息加载失败", "red");
            if (!System.IO.File.Exists(temp_UserImage))
            {
                string jsonContent1 = System.IO.File.ReadAllText(MainClass.Paths.temp_api_path);
                var jsonObject1 = JObject.Parse(jsonContent1);
                MainClass.Downloadfiles.Download(
                    jsonObject1["data"]["userimg"]?.ToString(),
                    temp_UserImage
                );
            }

            string jsonContent = System.IO.File.ReadAllText(MainClass.Paths.temp_api_path);
            var jsonObject = JObject.Parse(jsonContent);

            UserImage.ImageSource = new BitmapImage(new Uri(temp_UserImage));
            UserTextBlock.Text = jsonObject["data"]["username"]?.ToString();
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

        int i;

        private void TokenClick(object sender, RoutedEventArgs e)
        {
            Token.Click -= TokenClick;
            i++;

            if (i == 1)
                MainClass.Reminders.Reminder_Box_Show(usertoken, "green");
            if (i == 2)
            {
                Clipboard.SetDataObject(usertoken);
                MainClass.Reminders.Reminder_Box_Show("Token已复制到的剪切板点击重新显示", "green");
                Token.Content = "点击查看Token";
                i = 0;
            }
            Token.Click += TokenClick;
        }
    }
}
