using System.IO;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json.Linq;

namespace ChmlFrp_Professional_Launcher.Pages
{
    public partial class LaunchPage : Page
    {
        private Reminders Reminders = new();
        MainWindow MainWindow = Application.Current.MainWindow as MainWindow;

        //private Process process;

        public LaunchPage()
        {
            InitializeComponent();

            if (File.Exists(App.temp_api_tunnel_path))
            {
                string jsonContent = File.ReadAllText(App.temp_api_tunnel_path);
                var jsonObject = JObject.Parse(jsonContent);
                foreach (var tunnel in jsonObject["data"])
                {
                    comboBox.Items.Add(tunnel["name"]?.ToString());
                }
            }
        }

        int i;

        private void Launch(object sender, RoutedEventArgs e)
        {
            Reminders.Reminder_Box_Show("正在施工...", "yellow");
            return;

            //LaunchButton.Click -= Launch;
            //string frpciniFilePath = "";

            //i++;
            //i = (i == 6) ? 1 : i;
            //string logFilePath = Path.Combine(App.CPLPath, $"{i}.logs");

            //process = new Process();

            //ProcessStartInfo processInfo = new(
            //    "cmd.exe",
            //    $"/c {App.frpExePath} -c {frpciniFilePath} > {logFilePath} 2>&1"
            //) // 命令
            //{
            //    UseShellExecute = false,
            //    CreateNoWindow = true,
            //}; // 配置
            //process.StartInfo = processInfo; // 使用

            //try
            //{
            //    process.Start(); // 启动
            //}
            //catch (Exception ex)
            //{
            //    Reminders.Reminder_Box_Show($"启动进程失败: {ex.Message}", "red");

            //    LaunchButton.Click += Launch;
            //    return;
            //}
            //Reminders.Reminder_Box_Show("启动成功", "green");

            //LaunchButton.Click += Killfrp;
            //LaunchButton.Content = "关闭FRPC";
        }

        //private void Killfrp(object sender, RoutedEventArgs e)
        //{
        //    LaunchButton.Click -= Killfrp;

        //    if (process.HasExited)
        //        Reminders.Reminder_Box_Show("进程已退出", "red");
        //    else
        //    {
        //        process.Kill(); // 关闭
        //        process.Dispose();
        //        Reminders.Reminder_Box_Show("关闭成功", "green");
        //    }

        //    LaunchButton.Click += Launch;
        //    LaunchButton.Content = "启动FRPC";
        //}
    }
}
