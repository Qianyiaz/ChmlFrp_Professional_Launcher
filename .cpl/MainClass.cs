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
    class MainClass
    {
        static MainWindow MainWindow = Application.Current.MainWindow as MainWindow;

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
                if (!File.Exists(Paths.CPLPath))
                {
                    Directory.CreateDirectory(Paths.CPLPath);
                }
                if (!File.Exists(Paths.pictures_path))
                {
                    Directory.CreateDirectory(Paths.pictures_path);
                }
                if (!File.Exists(Paths.temp_path))
                {
                    Directory.CreateDirectory(Paths.temp_path);
                }
                if (!File.Exists(Paths.IniPath))
                {
                    Directory.CreateDirectory(Paths.IniPath);
                }
                if (!File.Exists(Paths.setupIniPath))
                {
                    IniData data = new();
                    FileIniDataParser parser = new();
                    data["ChmlFrp_Professional_Launcher Setup"]["Versions"] = "0.0.0.6";
                    parser.WriteFile(Paths.setupIniPath, data);
                }
                //创建日志文件
                for (int i = 1; i < 6; i++)
                {
                    if (!File.Exists(Path.Combine(Paths.CPLPath, i + ".logs")))
                    {
                        File.Create(Path.Combine(Paths.CPLPath, i + ".logs"));
                    }
                }
            }
            catch
            {
                Reminders.LogsOutputting("文件占用无法创建");
            }
        }

        internal class Paths
        {
            //定义路径
            public static string directoryPath;
            public static string IniPath;
            public static string frpExePath;
            public static string setupIniPath;
            public static string temp_path;
            public static string temp_api_path;
            public static string CPLPath;
            public static string pictures_path;
            public static string logfilePath;
            public static string temp_api_tunnel_path;

            public Paths()
            {
                directoryPath = Directory.GetCurrentDirectory();
                //文件夹路径
                CPLPath = Path.Combine(directoryPath, "CPL");
                IniPath = Path.Combine(CPLPath, "Ini");
                temp_path = Path.Combine(CPLPath, "Temp");
                pictures_path = Path.Combine(CPLPath, "Pictures");
                //文件路径
                frpExePath = Path.Combine(CPLPath, "frpc.exe");
                logfilePath = Path.Combine(CPLPath, "Debug.logs");
                setupIniPath = Path.Combine(CPLPath, "Setup.ini");
                temp_api_path = Path.Combine(temp_path, "login_chmlfrp_api.json");
                temp_api_tunnel_path = Path.Combine(temp_path, "temp_api_tunnel.json");
            }
        }

        internal static class Downloadfiles
        {
            public static bool Download(string url, string path)
            {
                if (url == null)
                {
                    MainClass.Reminders.LogsOutputting("下载失败：url不能为null。");
                    return false;
                }
                if (path == null)
                {
                    MainClass.Reminders.LogsOutputting("下载失败：path不能为null。");
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
                    MainClass.Reminders.LogsOutputting(
                        $"下载失败：文件占用或网络错误?path={path}&url={url}"
                    );
                    return false;
                }
                MainClass.Reminders.LogsOutputting($"下载成功：已下载?path={path}");
                return true;
            }

            public static async Task<bool> Downloadasync(string url, string path)
            {
                if (url == null)
                {
                    MainClass.Reminders.LogsOutputting("下载失败：url不能为null。");
                    return false;
                }
                if (path == null)
                {
                    MainClass.Reminders.LogsOutputting("下载失败：path不能为null。");
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
                    MainClass.Reminders.LogsOutputting(
                        $"下载失败：文件占用或网络错误?path={path}&url={url}"
                    );
                    return false;
                }
                MainClass.Reminders.LogsOutputting($"下载成功：已下载?path={path}");
                return true;
            }

            public static bool GetAPItoLogin(bool Remind)
            {
                var data = new FileIniDataParser().ReadFile(MainClass.Paths.setupIniPath);
                string username = data["ChmlFrp_Professional_Launcher Setup"]["Username"];
                string password = data["ChmlFrp_Professional_Launcher Setup"]["Password"];

                if (
                    Download(
                        $"https://cf-v2.uapis.cn/login?username={username}&password={password}",
                        Paths.temp_api_path
                    )
                )
                {
                    string msg = JObject
                        .Parse(File.ReadAllText(MainClass.Paths.temp_api_path))["msg"]
                        ?.ToString();
                    MainClass.Reminders.LogsOutputting("API提醒：" + msg);
                    if (msg == "登录成功")
                    {
                        if (Remind)
                            MainClass.Reminders.Reminder_Box_Show(msg, "green");
                        return true;
                    }
                    else
                    {
                        if (Remind)
                            MainClass.Reminders.Reminder_Box_Show(msg, "red");
                        return false;
                    }
                }
                else
                {
                    if (Remind)
                        MainClass.Reminders.Reminder_Box_Show("网络错误", "red");
                    return false;
                }
            }
        }

        internal static class Reminders
        {
            public static void LogsOutputting(string logEntry)
            {
                logEntry = $"[{DateTime.Now}] " + logEntry;
                Console.WriteLine(logEntry);
                File.AppendAllText(MainClass.Paths.logfilePath, logEntry + Environment.NewLine);
            }

            public static void Reminder_Box_Show(string message, string color)
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

            public static void Reminder_interface_Show(string subject, string message)
            {
                RemindersPageTwo RemindersPageTwo = new();
                RemindersPageTwo.SubjectTextBlock.Text = subject;
                RemindersPageTwo.TextTextBlock.Text = message;
                MainWindow.RemindersNavigation.Navigate(RemindersPageTwo);
            }
        }
    }
}

//Qusay Diaz©2024-2025
