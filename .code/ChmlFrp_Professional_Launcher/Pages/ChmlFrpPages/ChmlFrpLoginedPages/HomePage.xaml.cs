using Newtonsoft.Json.Linq;
using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
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

        public HomePage()
        {
            InitializeComponent();
            SetPath = new SetPath();
            string jsonContent = System.IO.File.ReadAllText(SetPath.temp_api_path);
            var jsonObject = JObject.Parse(jsonContent);
            temp_UserImage = Path.Combine(SetPath.temp_path, "temp_UserImage.png");
            if (System.IO.File.Exists(temp_UserImage))
            {
                InitializesetPaths();
            }
            else
            {
                Downloadfiles Downloadfiles = new();
                Downloadfiles.Download(jsonObject["data"]["userimg"]?.ToString(), temp_UserImage, "others");
                InitializesetPaths();
            }
        }

        private void InitializesetPaths()
        {
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
            UserTunnel_restrictions.Text = jsonObject["data"]["tunnelCount"]?.ToString() + " / " + jsonObject["data"]["tunnel"]?.ToString();
            UserBandwidth_throttling.Text = "国内" + jsonObject["data"]["bandwidth"]?.ToString() + "m";
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