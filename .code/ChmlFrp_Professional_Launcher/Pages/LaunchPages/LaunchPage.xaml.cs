using IniParser;
using IniParser.Model;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Path = System.IO.Path;

namespace ChmlFrp_Professional_Launcher.Pages
{
    /// <summary>
    /// Lógica de interacción para HomePage.xaml
    /// </summary>
    public partial class LaunchPage : Page
    {
        private Reminding Reminding = new();
        private SetPath SetPath = new();

        public LaunchPage()
        {
            InitializeComponent();
            Reminding.LogsOutputting("进入LaunchPage");
        }

        private int i = 0;

        private void Launch(object sender, RoutedEventArgs e)
        {
            LaunchButton.Click -= Launch;
            if (!File.Exists(SetPath.frpIniPath) && !File.Exists(SetPath.frpPath))
            {
                LaunchButton.Content = "未找到配置文件";
                return;
            }
            //创建ini实例
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(SetPath.frpIniPath);
            LaunchButton.Content = "正在启动中...";
            if (i == 5) { i = 0; }
            i++; string logs = Path.Combine(SetPath.CPLPath, i + ".logs");
            ProcessStartInfo processInfo = new("cmd.exe", "/c " + SetPath.frpPath + " -c " + SetPath.frpIniPath + " >" + logs + " 2>&1")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using (Process process = new())
            {
                process.StartInfo = processInfo;
                process.Start();
                LaunchButton.Content = "点击关闭 frpc";
                LaunchButton.Click += Killfrp;
            }
        }

        private static bool IsProcessRunning(string processName, int count)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length >= count;
        }

        private void Killfrp(object sender, RoutedEventArgs e)
        {
            LaunchButton.Click -= Killfrp;
            if (!IsProcessRunning("frpc", 0))
            {
                LaunchButton.Content = "启动 frpc";
                LaunchButton.Click += Launch;
                return;
            }
            string name = "frpc";
            Process[] processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                if (process.ProcessName.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    process.Kill();
                    process.WaitForExit();
                }
            }
            LaunchButton.Content = "启动 frpc";
            LaunchButton.Click += Launch;
        }
    }
}