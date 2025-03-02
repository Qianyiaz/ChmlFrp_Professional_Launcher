using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using ChmlFrp_Professional_Launcher.Pages;
using IniParser;
using IniParser.Model;
using Newtonsoft.Json.Linq;

namespace ChmlFrp_Professional_Launcher
{
    internal class InitializeClass
    {
        Reminding Reminding = new();
        MainWindow MainWindow = Application.Current.MainWindow as MainWindow;

        //初始化
        public void Initialize()
        {
            // 检测是否有两个ChmlFrp Professional Launcher进程
            var currentProcess = Process.GetCurrentProcess();
            var processes = Process.GetProcessesByName(currentProcess.ProcessName);
            if (processes.Length > 1)
            {
                MainWindow.btnClose_Click(null, null);
                return;
            }

            try
            {
                //检测是否有相关配置文件
                if (!File.Exists(App.CPLPath))
                {
                    Directory.CreateDirectory(App.CPLPath);
                }
                if (!File.Exists(App.pictures_path))
                {
                    Directory.CreateDirectory(App.pictures_path);
                }
                if (!File.Exists(App.temp_path))
                {
                    Directory.CreateDirectory(App.temp_path);
                }
                if (!File.Exists(App.IniPath))
                {
                    Directory.CreateDirectory(App.IniPath);
                }
                if (!File.Exists(App.setupIniPath))
                {
                    IniData data = new();
                    FileIniDataParser parser = new();
                    data["ChmlFrp_Professional_Launcher Setup"]["Versions"] = "0.0.0.6";
                    parser.WriteFile(App.setupIniPath, data);
                }
                //创建日志文件
                for (int i = 1; i < 6; i++)
                {
                    if (!File.Exists(Path.Combine(App.CPLPath, i + ".logs")))
                    {
                        File.Create(Path.Combine(App.CPLPath, i + ".logs"));
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

        public bool Download(string url, string path)
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
                using WebClient client = new();
                client.Encoding = Encoding.UTF8;
                client.DownloadFile(new Uri(url), path);
            }
            catch
            {
                Reminding.LogsOutputting($"下载失败：文件占用或网络错误?path={path}&url={url}");
                return false;
            }
            Reminding.LogsOutputting($"下载成功：已下载?path={path}");
            return true;
        }

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
                using WebClient client = new();
                client.Encoding = Encoding.UTF8;
                await client.DownloadFileTaskAsync(new Uri(url), path);
            }
            catch
            {
                Reminding.LogsOutputting($"下载失败：文件占用或网络错误?path={path}&url={url}");
                return false;
            }
            Reminding.LogsOutputting($"下载成功：已下载?path={path}");
            return true;
        }

        public bool GetAPItoLogin(bool Remind)
        {
            var data = new FileIniDataParser().ReadFile(App.setupIniPath);
            string username = data["ChmlFrp_Professional_Launcher Setup"]["Username"];
            string password = data["ChmlFrp_Professional_Launcher Setup"]["Password"];

            if (
                Download(
                    $"https://cf-v2.uapis.cn/login?username={username}&password={password}",
                    App.temp_api_path
                )
            )
            {
                string msg = JObject.Parse(File.ReadAllText(App.temp_api_path))["msg"]?.ToString();
                Reminding.LogsOutputting("API提醒：" + msg);
                if (msg == "登录成功")
                {
                    if (Remind)
                        Reminding.RemindingShow(msg, "green");
                    return true;
                }
                else
                {
                    if (Remind)
                        Reminding.RemindingShow(msg, "red");
                    return false;
                }
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
        MainWindow MainWindow = Application.Current.MainWindow as MainWindow;

        public void LogsOutputting(string logEntry)
        {
            logEntry = $"[{DateTime.Now}] " + logEntry;
            Console.WriteLine(logEntry);
            File.AppendAllText(App.logfilePath, logEntry + Environment.NewLine);
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
}

//Qusay Diaz©2024-2025
