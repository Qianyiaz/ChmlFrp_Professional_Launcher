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

using ChmlFrp_Professional_Launcher.Pages;
using IniParser;
using IniParser.Model;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChmlFrp_Professional_Launcher
{
    internal class SetPath
    {
        //定义路径
        public string directoryPath;

        public string frpPath;
        public string frpIniPath;
        public string frpExePath;
        public string setupIniPath;
        public string temp_path;
        public string temp_api_path;
        public string CPLPath;
        public string pictures_path;
        public string logfilePath;

        public SetPath()
        {
            directoryPath = Directory.GetCurrentDirectory();
            CPLPath = Path.Combine(directoryPath, "CPL");
            frpPath = Path.Combine(CPLPath, "frp");
            frpIniPath = Path.Combine(frpPath, "frpc.ini");
            frpExePath = Path.Combine(frpPath, "frpc.exe");
            setupIniPath = Path.Combine(CPLPath, "Setup.ini");
            temp_path = Path.Combine(CPLPath, "temp");
            temp_api_path = Path.Combine(temp_path, "login_chmlfrp_api.json");
            pictures_path = Path.Combine(CPLPath, "pictures");
            logfilePath = Path.Combine(CPLPath, "Debug.logs");
        }
    }

    internal class Downloadfiles
    {
        private Reminding Reminding = new();
        private SetPath SetPath = new();

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
                        Reminding.LogsOutputting("下载失败：路径或文件占用或网络错误?path=" + path + "&url=" + url);
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
                    Reminding.LogsOutputting("下载失败：路径或文件占用或网络错误?path=" + path + "&url=" + url);
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

        public bool GitAPI_Login(bool Remind)
        {
            IniData data;
            var parser = new FileIniDataParser();
            data = parser.ReadFile(SetPath.setupIniPath);
            Downloadfiles Downloadfiles = new();
            if (Downloadfiles.Download("https://cf-v2.uapis.cn/login?username=" + data["ChmlFrp_Professional_Launcher Setup"]["Username"] + "&password=" + data["ChmlFrp_Professional_Launcher Setup"]["Password"], SetPath.temp_api_path, "txt") == "下载成功")
            {
                var jsonObject = JObject.Parse(File.ReadAllText(SetPath.temp_api_path));
                string msg = jsonObject["msg"]?.ToString();
                Reminding.LogsOutputting("API提醒：" + msg);
                if (msg == "登录成功" && Remind == false)
                {
                    return true;
                }
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
                {
                    return false;
                }
            }
            else
            {
                if (Remind) Reminding.RemindingShow("网络错误", "red");
                return false;
            }
        }
    }

    internal class Reminding
    {
        private SetPath SetPath = new();

        public void LogsOutputting(string logEntry)
        {
            logEntry = $"[{DateTime.Now}] " + logEntry;
            Console.WriteLine(logEntry);

            FileInfo logFileInfo = new(SetPath.logfilePath);
            if (logFileInfo.Exists && logFileInfo.Length > 15 * 1024)
            {
                File.WriteAllText(SetPath.logfilePath, string.Empty);
            }

            File.AppendAllText(SetPath.logfilePath, logEntry + Environment.NewLine);
        }

        public void RemindingShow(string message, string color)
        {
            MainWindow MainWindow = Application.Current.MainWindow as MainWindow;
            RemindingPage RemindingPage = new();
            if (color == "green")
            {
                RemindingPage.RemindingBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3DB43E"));
            }
            else if (color == "blue")
            {
                RemindingPage.RemindingBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#349EF7"));
            }
            else if (color == "red")
            {
                RemindingPage.RemindingBorder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0000"));
            }
            else
            {
                LogsOutputting("RemindingShow颜色参数错误");
                return;
            }
            RemindingPage.RemidingTextBlock.Text = message;
            LogsOutputting("显示提醒：" + message);
            MainWindow.PagesNavigationtwo.Navigate(RemindingPage);
        }

        //初始化
        public void Initialize()
        {
            //创建ini实例
            var parser = new FileIniDataParser();
            IniData data;
            //检测是否有相关配置文件
            try
            {
                if (!File.Exists(SetPath.CPLPath))
                {
                    Directory.CreateDirectory(SetPath.CPLPath);
                }
                if (!File.Exists(SetPath.frpPath))
                {
                    Directory.CreateDirectory(SetPath.frpPath);
                }
                if (!File.Exists(SetPath.pictures_path))
                {
                    Directory.CreateDirectory(SetPath.pictures_path);
                }
                if (!File.Exists(SetPath.temp_path))
                {
                    Directory.CreateDirectory(SetPath.temp_path);
                }
                if (!File.Exists(SetPath.setupIniPath))
                {
                    data = new IniData();
                    data["ChmlFrp_Professional_Launcher Setup"]["Versions"] = "0.0.0.5";
                    parser.WriteFile(SetPath.setupIniPath, data);
                }
                File.WriteAllText(SetPath.logfilePath, string.Empty);
            }
            catch
            {
                LogsOutputting("文件夹或文件创建失败");
            }
            LogsOutputting("文件夹已创建或创建成功");
            //创建日志文件
            try
            {
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
                LogsOutputting("日志文件创建失败");
            }
            LogsOutputting("日志文件已创建或创建成功");
            LogsOutputting("进入MainWindow");
        }
    }

    public class CornerButten : RadioButton
    {
    }

    public class LaunchingButten : Button
    {
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LaunchingButten));
    }
}