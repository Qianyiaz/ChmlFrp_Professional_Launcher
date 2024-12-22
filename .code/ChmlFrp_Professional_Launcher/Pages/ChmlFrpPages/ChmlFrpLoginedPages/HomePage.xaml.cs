using System.IO;
using System.Windows.Controls;
using Path = System.IO.Path;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Windows.Media.Imaging;
using System.Net;
using System.Windows;
using static System.Net.WebRequestMethods;
using System.Threading;

namespace ChmlFrp_Professional_Launcher.Pages.ChmlFrp
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : Page
    {
        private ClienterClass clienterClass;

        public HomePage()
        {
            InitializeComponent();
            clienterClass = new ClienterClass();
            InitializesetPaths();
        }


        private void InitializesetPaths()
        {
            string jsonContent = System.IO.File.ReadAllText(clienterClass.temp_api_path);
            var jsonObject = JObject.Parse(jsonContent);
            string url = jsonObject["data"]["userimg"]?.ToString();
            string temp_UserImage = Path.Combine(clienterClass.temp_path, "temp_UserImage.png");
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(new Uri(url), temp_UserImage);
                }
            }
            catch
            {
            }
            UserImage.ImageSource = new BitmapImage(new Uri(temp_UserImage));
            UserTextBlock.Text = UserTextBlock.Text + jsonObject["data"]["username"]?.ToString();
            Usermailbox.Text = jsonObject["data"]["email"]?.ToString();
            UserRegistration_time.Text = jsonObject["data"]["regtime"]?.ToString();
            UserQQ.Text = jsonObject["data"]["qq"]?.ToString();
            Userright.Text = jsonObject["data"]["usergroup"]?.ToString();
            UserExpiration_time.Text = jsonObject["data"]["term"]?.ToString();
            UserReal_name_status.Text = jsonObject["data"]["realname"]?.ToString();
            UserPoints_remaining.Text = jsonObject["data"]["integral"]?.ToString();
            UserTunnel_restrictions.Text = jsonObject["data"]["tunnelCount"]?.ToString() + " / " + jsonObject["data"]["tunnel"]?.ToString();
            UserBandwidth_throttling.Text = "国内" + jsonObject["data"]["bandwidth"]?.ToString() + "m";
        }

        private void TokenClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Token.Click -= TokenClick;
            string jsonContent = System.IO.File.ReadAllText(clienterClass.temp_api_path);
            var jsonObject = JObject.Parse(jsonContent);
            if (Token.Content.ToString() == jsonObject["data"]["usertoken"]?.ToString())
            {
                Clipboard.SetDataObject(Token.Content.ToString());
                Token.Content = "已复制到的剪切板点击重新显示";
                Token.Click += TokenClick;
                return;
            }
            Token.Content = jsonObject["data"]["usertoken"]?.ToString();
            Token.Click += TokenClick;
        }
    }
}

