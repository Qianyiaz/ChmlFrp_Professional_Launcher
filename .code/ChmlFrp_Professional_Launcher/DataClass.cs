/*
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


using System.IO;
using System.Net.Http;
using System.Net;
using IniParser.Model;
using IniParser;
using Newtonsoft.Json.Linq;
using System.Text;
using System;
using System.Windows;
using ChmlFrp_Professional_Launcher.Pages;


namespace ChmlFrp_Professional_Launcher
{
    internal class SetPath
    {
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
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        WebClient webClient = new WebClient();
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
                    using (WebClient client = new WebClient())
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

        public bool GitAPI_Login()
        {
            IniData data;
            var parser = new FileIniDataParser();
            data = parser.ReadFile(SetPath.setupIniPath);
            Downloadfiles Downloadfiles = new Downloadfiles();
            if (Downloadfiles.Download("https://cf-v2.uapis.cn/login?username=" + data["ChmlFrp_Professional_Launcher Setup"]["Username"] + "&password=" + data["ChmlFrp_Professional_Launcher Setup"]["Password"], SetPath.temp_api_path, "txt") == "下载成功")
            {
                var jsonObject = JObject.Parse(File.ReadAllText(SetPath.temp_api_path));
                string msg = jsonObject["msg"]?.ToString();
                Reminding.LogsOutputting(msg);
                Reminding.RemindingShow(msg);

                if (msg == "登录成功")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
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

            FileInfo logFileInfo = new FileInfo(SetPath.logfilePath);
            if (logFileInfo.Exists && logFileInfo.Length > 15 * 1024)
            {
                File.WriteAllText(SetPath.logfilePath, string.Empty);
            }

            File.AppendAllText(SetPath.logfilePath, logEntry + Environment.NewLine);
        }

        public void RemindingShow(string message)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            RemindingPage remindingPage = new RemindingPage();
            remindingPage.RemidingTextBlock.Text = message;
            mainWindow.PagesNavigationtwo.Navigate(remindingPage);
        }
    }
}