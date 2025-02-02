using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json.Linq;

namespace ChmlFrp_Professional_Launcher.Pages
{
    public partial class LaunchPage : Page
    {
        private Reminding Reminding = new();
        private SetPath SetPath = new();

        public LaunchPage()
        {
            InitializeComponent();
            try
            {
                string jsonContent = System.IO.File.ReadAllText(SetPath.temp_api_tunnel_path);
                var jsonObject = JObject.Parse(jsonContent);
                foreach (var tunnel in jsonObject["data"])
                {
                    comboBox.Items.Add(tunnel["name"]?.ToString());
                }
            }
            catch
            {
                Reminding.RemindingShow("获取隧道列表失败", "red");
            }
        }

        //private int i = 0;

        private void Launch(object sender, RoutedEventArgs e)
        {
            Reminding.RemindingShow("正在施工中...", "yellow");
            //LaunchButton.Click -= Launch;
            //if (!File.Exists(SetPath.frpPath))
            //{
            //    MainWindow MainWindow = Application.Current.MainWindow as MainWindow;
            //    RemindingthreePage RemindingthreePage = new();
            //    MainWindow.PagesNavigationtwo.Navigate(RemindingthreePage);
            //    LaunchButton.Click += Launch;
            //    return;
            //}
            ////创建ini实例
            //var parser = new FileIniDataParser();
            //IniData data = parser.ReadFile(SetPath.frpIniPath);
            //LaunchButton.Content = "正在启动中...";
            //if (i == 5)
            //{
            //    i = 0;
            //}
            //i++;
            //string logs = Path.Combine(SetPath.CPLPath, i + ".logs");
            //ProcessStartInfo processInfo = new(
            //    "cmd.exe",
            //    "/c " + SetPath.frpPath + " -c " + SetPath.frpIniPath + " >" + logs + " 2>&1"
            //)
            //{
            //    RedirectStandardOutput = true, //重定向标准输出
            //    UseShellExecute = false, //不使用系统外壳程序启动
            //    CreateNoWindow = true, //不显示窗口
            //};
            //using (Process process = new())
            //{
            //    process.StartInfo = processInfo; //设置进程启动信息
            //    process.Start(); //启动进程
            //    LaunchButton.Content = "点击关闭 frpc";
            //    LaunchButton.Click += Killfrp;
            //}
        }

        //private void Killfrp(object sender, RoutedEventArgs e)
        //{
        //    LaunchButton.Click -= Killfrp;
        //    string name = "frpc";
        //    Process[] processes = Process.GetProcesses();
        //    foreach (Process process in processes)
        //    {
        //        if (process.ProcessName.Equals(name, StringComparison.OrdinalIgnoreCase))
        //        {
        //            process.Kill();
        //            process.WaitForExit();
        //        }
        //    }
        //    LaunchButton.Content = "启动FRP";
        //    LaunchButton.Click += Launch;
        //}
    }
}
