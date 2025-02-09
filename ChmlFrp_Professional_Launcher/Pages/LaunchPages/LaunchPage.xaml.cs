using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json.Linq;

namespace ChmlFrp_Professional_Launcher.Pages
{
    public partial class LaunchPage : Page
    {
        private Reminding Reminding = new();
        private SetPath SetPath = new();
        private Process process;

        public LaunchPage()
        {
            InitializeComponent();
            if (File.Exists(SetPath.temp_api_tunnel_path))
            {
                string jsonContent = File.ReadAllText(SetPath.temp_api_tunnel_path);
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
            LaunchButton.Click -= Launch;

            string frpciniFilePath =
                "E:\\Qianyiaz\\ChmlFrp_Professional_Launcher\\ChmlFrp_Professional_Launcher\\bin\\Debug\\CPL\\1.logs";

            i++;
            i = (i == 6) ? 1 : i;
            string logFilePath = Path.Combine(SetPath.CPLPath, $"{i}.logs");

            process = new Process();

            ProcessStartInfo processInfo = new(
                "cmd.exe",
                $"/c {SetPath.frpExePath} -c {frpciniFilePath} > {logFilePath} 2>&1"
            ) // 命令
            {
                UseShellExecute = false,
                CreateNoWindow = true,
            }; // 配置
            process.StartInfo = processInfo; // 使用

            try
            {
                process.Start(); // 启动
            }
            catch (Exception ex)
            {
                Reminding.RemindingShow($"启动进程失败: {ex.Message}", "red");

                LaunchButton.Click += Launch;
                return;
            }
            Reminding.RemindingShow("启动成功", "green");

            LaunchButton.Click += Killfrp;
            LaunchButton.Content = "关闭FRPC";
        }

        private void Killfrp(object sender, RoutedEventArgs e)
        {
            LaunchButton.Click -= Killfrp;

            if (process.HasExited)
            {
                Reminding.RemindingShow("进程已退出", "red");
            }
            else
            {
                process.Kill(); // 关闭
                process.Dispose();
                Reminding.RemindingShow("关闭成功", "green");
            }

            LaunchButton.Click += Launch;
            LaunchButton.Content = "启动FRPC";
        }
    }
}
