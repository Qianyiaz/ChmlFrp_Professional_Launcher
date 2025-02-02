using System.IO;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json.Linq;

namespace ChmlFrp_Professional_Launcher.Pages.ChmlFrpPages.Register
{
    /// <summary>
    /// RegisterPageTwo.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterPageTwo : Page
    {
        MainWindow MainWindow = Application.Current.MainWindow as MainWindow;
        Reminding Reminding = new();
        Downloadfiles Downloadfiles = new();
        SetPath SetPath = new();
        string code;
        string register;

        public RegisterPageTwo()
        {
            InitializeComponent();
            code = Path.Combine(SetPath.temp_path, "temp_code.json");
            register = Path.Combine(SetPath.temp_path, "temp_register.json");
        }

        private void Last(object sender, RoutedEventArgs e)
        {
            MainWindow.ChmlFrpHomePage.ChmlFrpLoginPage.PagesNavigation.Navigate(
                MainWindow.ChmlFrpHomePage.ChmlFrpLoginPage.LoginPage.RegisterPageOne
            );
            return;
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            if (TextBox_Mail.Text != "")
            {
                Reminding.RemindingShow("未填写邮箱", "red");
                return;
            }
            string username = MainWindow
                .ChmlFrpHomePage
                .ChmlFrpLoginPage
                .LoginPage
                .RegisterPageOne
                .TextBox_Username
                .Text;
            string password = MainWindow
                .ChmlFrpHomePage
                .ChmlFrpLoginPage
                .LoginPage
                .RegisterPageOne
                .TextBox_password
                .Text;
            string qq = MainWindow
                .ChmlFrpHomePage
                .ChmlFrpLoginPage
                .LoginPage
                .RegisterPageOne
                .TextBox_QQ
                .Text;

            if (
                Downloadfiles.Download(
                    "http://cf-v2.uapis.cnregister?username="
                        + username
                        + "&password="
                        + password
                        + "&mail="
                        + TextBox_Code.Text
                        + "&qq="
                        + qq
                        + "&code="
                        + TextBox_Code.Text,
                    register,
                    "text"
                ) != "下载成功"
            )
            {
                Reminding.RemindingShow("网络错误", "red");
                return;
            }
            var jsonObject = JObject.Parse(File.ReadAllText(register));
            string msg = jsonObject["msg"]?.ToString();
            if (msg == "注册成功")
            {
                Reminding.RemindingShow("注册成功", "green");
                MainWindow.ChmlFrpHomePage.ChmlFrpLoginPage.PagesNavigation.Navigate(
                    MainWindow.ChmlFrpHomePage.ChmlFrpLoginPage.LoginPage
                );
                return;
            }
            else if (msg == "验证码错误")
            {
                Reminding.RemindingShow("验证码错误", "red");
                return;
            }
            else
            {
                Reminding.RemindingShow("注册失败", "red");
                return;
            }
        }

        private void GetCode(object sender, RoutedEventArgs e)
        {
            if (
                Downloadfiles.Download(
                    "http://cf-v2.uapis.cn/sendmailcode?type=retoken&mail=" + TextBox_Mail.Text,
                    code,
                    "text"
                ) != "下载成功"
            )
            {
                Reminding.RemindingShow("网络错误", "red");
                return;
            }
            var jsonObject = JObject.Parse(File.ReadAllText(code));
            string msg = jsonObject["msg"]?.ToString();
            if (msg == "发送成功")
            {
                Reminding.RemindingShow("发送成功", "green");
                return;
            }
            else
            {
                Reminding.RemindingShow("邮箱格式错误", "red");
                return;
            }
        }

        private void TextBox_Mail_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox_Mail.TextTwo = "";
        }

        private void TextBox_Mail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_Mail.Text == "")
            {
                TextBox_Mail.TextTwo = "邮箱";
            }
        }

        private void TextBox_password_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox_password.TextTwo = "";
        }

        private void TextBox_password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_password.Text == "")
            {
                TextBox_password.TextTwo = "确认密码";
            }
        }

        private void TextBox_Code_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox_Code.TextTwo = "";
        }

        private void TextBox_Code_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TextBox_Code.Text == "")
            {
                TextBox_Code.TextTwo = "验证码";
            }
        }
    }
}
