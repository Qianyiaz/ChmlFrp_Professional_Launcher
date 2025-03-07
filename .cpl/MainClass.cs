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
        Reminders Reminders = new();
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
                Reminders.LogsOutputting("文件占用无法创建");
            }
        }
    }

    internal class Downloadfiles
    {
        private Reminders Reminders = new();

        public bool Download(string url, string path)
        {
            if (url == null)
            {
                Reminders.LogsOutputting("下载失败：url不能为null。");
                return false;
            }
            if (path == null)
            {
                Reminders.LogsOutputting("下载失败：path不能为null。");
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
                Reminders.LogsOutputting($"下载失败：文件占用或网络错误?path={path}&url={url}");
                return false;
            }
            Reminders.LogsOutputting($"下载成功：已下载?path={path}");
            return true;
        }

        public async Task<bool> Downloadasync(string url, string path)
        {
            if (url == null)
            {
                Reminders.LogsOutputting("下载失败：url不能为null。");
                return false;
            }
            if (path == null)
            {
                Reminders.LogsOutputting("下载失败：path不能为null。");
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
                Reminders.LogsOutputting($"下载失败：文件占用或网络错误?path={path}&url={url}");
                return false;
            }
            Reminders.LogsOutputting($"下载成功：已下载?path={path}");
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
                Reminders.LogsOutputting("API提醒：" + msg);
                if (msg == "登录成功")
                {
                    if (Remind)
                        Reminders.Reminder_Box_Show(msg, "green");
                    return true;
                }
                else
                {
                    if (Remind)
                        Reminders.Reminder_Box_Show(msg, "red");
                    return false;
                }
            }
            else
            {
                if (Remind)
                    Reminders.Reminder_Box_Show("网络错误", "red");
                return false;
            }
        }
    }

    internal class Reminders
    {
        MainWindow MainWindow = Application.Current.MainWindow as MainWindow;

        public void LogsOutputting(string logEntry)
        {
            logEntry = $"[{DateTime.Now}] " + logEntry;
            Console.WriteLine(logEntry);
            File.AppendAllText(App.logfilePath, logEntry + Environment.NewLine);
        }

        public void Reminder_Box_Show(string message, string color)
        {
            RemindersPage RemindersPage = new();

                RemindersPage.RemidingTextBlock.Foreground = new SolidColorBrush(Colors.White);

                if (color == "green")
                {
                    RemindersPage.RemindersBorder.Background = new SolidColorBrush(
                        (Color)ColorConverter.ConvertFromString("#3DB43E")
                    );
                }
                else if (color == "blue")
                {
                    RemindersPage.RemindersBorder.Background = new SolidColorBrush(
                        (Color)ColorConverter.ConvertFromString("#349EF7")
                    );
                }
                else if (color == "red")
                {
                    RemindersPage.RemindersBorder.Background = new SolidColorBrush(
                        (Color)ColorConverter.ConvertFromString("#FF0000")
                    );
                }
                else if (color == "yellow")
                {
                    RemindersPage.RemidingTextBlock.Foreground = new SolidColorBrush(Colors.Green);
                    RemindersPage.RemindersBorder.Background = new SolidColorBrush(Colors.Yellow);
                }
                else
                {
                    LogsOutputting("Reminder_Box_Show颜色参数错误");
                    return;
                }

                RemindersPage.RemidingTextBlock.Text = message;
                MainWindow.RemindersNavigationTwo.Navigate(RemindersPage);
        }

        public void Reminder_interface_Show(string subject, string message)
        {
            RemindersPageTwo RemindersPageTwo = new();
            RemindersPageTwo.SubjectTextBlock.Text = subject;
            RemindersPageTwo.TextTextBlock.Text = message;
            MainWindow.RemindersNavigation.Navigate(RemindersPageTwo);
        }
    }
}

//Qusay Diaz©2024-2025
