﻿/*
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
// ChmlFrp_Professional_Launcher/DataClass.cs
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ChmlFrp_Professional_Launcher.Pages;
using IniParser;
using IniParser.Model;
using Newtonsoft.Json.Linq;

namespace ChmlFrp_Professional_Launcher
{
    internal class SetPath
    {
        //定义路径
        public string directoryPath;
        public string IniPath;
        public string frpExePath;
        public string setupIniPath;
        public string temp_path;
        public string temp_api_path;
        public string CPLPath;
        public string pictures_path;
        public string logfilePath;
        public string temp_api_tunnel_path;

        public SetPath()
        {
            directoryPath = Directory.GetCurrentDirectory();
            CPLPath = Path.Combine(directoryPath, "CPL");
            IniPath = Path.Combine(CPLPath, "ini");
            frpExePath = Path.Combine(CPLPath, "frpc.exe");
            setupIniPath = Path.Combine(CPLPath, "Setup.ini");
            temp_path = Path.Combine(CPLPath, "temp");
            temp_api_path = Path.Combine(temp_path, "login_chmlfrp_api.json");
            pictures_path = Path.Combine(CPLPath, "pictures");
            logfilePath = Path.Combine(CPLPath, "Debug.logs");
            temp_api_tunnel_path = Path.Combine(temp_path, "temp_api_tunnel.json");
        }
    }

    internal class InitializeClass
    {
        SetPath SetPath = new();
        Reminding Reminding = new();

        //初始化
        public void Initialize()
        {
            try
            {
                //创建ini实例
                var parser = new FileIniDataParser();
                IniData data;
                //检测是否有相关配置文件
                if (!File.Exists(SetPath.CPLPath))
                {
                    Directory.CreateDirectory(SetPath.CPLPath);
                }
                if (!File.Exists(SetPath.pictures_path))
                {
                    Directory.CreateDirectory(SetPath.pictures_path);
                }
                if (!File.Exists(SetPath.temp_path))
                {
                    Directory.CreateDirectory(SetPath.temp_path);
                }
                if (!File.Exists(SetPath.IniPath))
                {
                    Directory.CreateDirectory(SetPath.IniPath);
                }
                if (!File.Exists(SetPath.setupIniPath))
                {
                    data = new IniData();
                    data["ChmlFrp_Professional_Launcher Setup"]["Versions"] = "0.0.0.5";
                    parser.WriteFile(SetPath.setupIniPath, data);
                }
                //创建日志文件
                for (int i = 1; i < 6; i++)
                {
                    if (!File.Exists(Path.Combine(SetPath.CPLPath, i + ".logs")))
                    {
                        File.Create(Path.Combine(SetPath.CPLPath, i + ".logs"));
                    }
                }
            }
            catch
            {
                Reminding.LogsOutputting("文件占用无法创建");
            }
        }
    }

    internal class Downloadfiles
    {
        private Reminding Reminding = new();
        private SetPath SetPath = new();

        public async Task<bool> Downloadasync(string url, string path)
        {
            if (url == null)
            {
                Reminding.LogsOutputting("下载失败：url不能为null。");
                return false;
            }
            if (path == null)
            {
                Reminding.LogsOutputting("下载失败：path不能为null。");
                return false;
            }

            try
            {
                using (WebClient client = new())
                {
                    await client.DownloadFileTaskAsync(new Uri(url), path);
                }
            }
            catch
            {
                Reminding.LogsOutputting(
                    "下载失败：路径或文件占用或网络错误?path=" + path + "&url=" + url
                );
                return false;
            }
            Reminding.LogsOutputting("下载成功：已下载到" + path);
            return true;
        }

        public string Download(string url, string path, string fileclass)
        {
            if (url == null)
            {
                Reminding.LogsOutputting("下载失败：url不能为null。");
                return "下载失败：url不能为null。";
            }

            if (fileclass == "txt")
            {
                using (HttpClient client = new())
                {
                    try
                    {
                        WebClient webClient = new();
                        webClient.Encoding = Encoding.UTF8;
                        File.WriteAllText(path, webClient.DownloadString(url));
                    }
                    catch
                    {
                        Reminding.LogsOutputting(
                            "下载失败：路径或文件占用或网络错误?path=" + path + "&url=" + url
                        );
                        return "下载失败：路径或文件占用或网络错误";
                    }
                    Reminding.LogsOutputting("下载成功：已下载到" + path);
                    return "下载成功";
                }
            }
            else if (fileclass == "others")
            {
                try
                {
                    using (WebClient client = new())
                    {
                        client.DownloadFile(new Uri(url), path);
                    }
                }
                catch
                {
                    Reminding.LogsOutputting(
                        "下载失败：路径或文件占用或网络错误?path=" + path + "&url=" + url
                    );
                    return "下载失败：路径或文件占用或网络错误";
                }
                Reminding.LogsOutputting("下载成功：已下载到" + path);
                return "下载成功";
            }
            else
            {
                Reminding.LogsOutputting("下载失败：fileclass错误。");
                return "下载失败：fileclass错误。";
            }
        }

        public bool GetAPItoLogin(bool Remind)
        {
            IniData data;
            var parser = new FileIniDataParser();
            data = parser.ReadFile(SetPath.setupIniPath);
            Downloadfiles Downloadfiles = new();
            if (
                Downloadfiles.Download(
                    "https://cf-v2.uapis.cn/login?username="
                        + data["ChmlFrp_Professional_Launcher Setup"]["Username"]
                        + "&password="
                        + data["ChmlFrp_Professional_Launcher Setup"]["Password"],
                    SetPath.temp_api_path,
                    "txt"
                ) == "下载成功"
            )
            {
                var jsonObject = JObject.Parse(File.ReadAllText(SetPath.temp_api_path));
                string msg = jsonObject["msg"]?.ToString();
                Reminding.LogsOutputting("API提醒：" + msg);
                if (msg == "登录成功" && Remind == false)
                    return true;
                else if (Remind && msg == "登录成功")
                {
                    Reminding.RemindingShow(msg, "green");
                    return true;
                }
                else if (Remind)
                {
                    Reminding.RemindingShow(msg, "red");
                    return false;
                }
                else
                    return false;
            }
            else
            {
                if (Remind)
                    Reminding.RemindingShow("网络错误", "red");
                return false;
            }
        }
    }

    internal class Reminding
    {
        private SetPath SetPath = new();
        MainWindow MainWindow = Application.Current.MainWindow as MainWindow;

        public void LogsOutputting(string logEntry)
        {
            logEntry = $"[{DateTime.Now}] " + logEntry;
            Console.WriteLine(logEntry);

            FileInfo logFileInfo = new(SetPath.logfilePath);
            if (logFileInfo.Exists && logFileInfo.Length > 50 * 1024)
            {
                File.WriteAllText(SetPath.logfilePath, string.Empty);
            }

            File.AppendAllText(SetPath.logfilePath, logEntry + Environment.NewLine);
        }

        public void RemindingtwoShow(string subject, string message)
        {
            RemindingtwoPage RemindingtwoPage = new();
            RemindingtwoPage.SubjectTextBlock.Text = subject;
            RemindingtwoPage.TextTextBlock.Text = message;
            MainWindow.PagesNavigationtwo.Navigate(RemindingtwoPage);
        }

        public void RemindingShow(string message, string color)
        {
            RemindingPage RemindingPage = new();
            if (color == "green")
            {
                RemindingPage.RemindingBorder.Background = new SolidColorBrush(
                    (Color)ColorConverter.ConvertFromString("#3DB43E")
                );
            }
            else if (color == "blue")
            {
                RemindingPage.RemindingBorder.Background = new SolidColorBrush(
                    (Color)ColorConverter.ConvertFromString("#349EF7")
                );
            }
            else if (color == "red")
            {
                RemindingPage.RemindingBorder.Background = new SolidColorBrush(
                    (Color)ColorConverter.ConvertFromString("#FF0000")
                );
            }
            else if (color == "yellow")
            {
                RemindingPage.RemindingBorder.Background = new SolidColorBrush(Colors.Yellow);
            }
            else
            {
                LogsOutputting("RemindingShow颜色参数错误");
                return;
            }
            RemindingPage.RemidingTextBlock.Text = message;
            MainWindow.PagesNavigationthree.Navigate(RemindingPage);
        }
    }

    //自建控件

    public class CornerIconRadioButton : RadioButton
    {
        public object Data
        {
            get { return GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
            "Data",
            typeof(object),
            typeof(CornerIconRadioButton)
        );
    }

    public class TransparentIconRadioButton : CornerIconRadioButton { }

    public class CornerButten : Button
    {
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CornerButten));

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
            "IsSelected",
            typeof(bool),
            typeof(CornerButten)
        );
    }

    public class CornerTextBox : TextBox, INotifyPropertyChanged
    {
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(
                "CornerRadius",
                typeof(CornerRadius),
                typeof(CornerTextBox)
            );

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty TextTwoProperty = DependencyProperty.Register(
            "TextTwo",
            typeof(string),
            typeof(CornerTextBox)
        );

        public string TextTwo
        {
            get { return (string)GetValue(TextTwoProperty); }
            set { SetValue(TextTwoProperty, value); }
        }

        public static new readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(CornerTextBox),
            new PropertyMetadata(string.Empty, OnTextChanged)
        );

        public new string Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
                OnPropertyChanged(nameof(Text));
            }
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as CornerTextBox;
            control?.OnPropertyChanged(nameof(Text));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
