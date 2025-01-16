using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using IniParser;
using IniParser.Model;
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
            Launch(null, null);
        }

        private int i = 0;

        private void Launch(object sender, RoutedEventArgs e)
        {
            LaunchButton.Click -= Launch;
            if (!File.Exists(SetPath.frpPath))
            {
                LaunchButton.Content = "未发现FRP文件";
                Reminding.RemindingtwoShow(
                    "未发现FRP文件",
                    "请检查以下几点：\n1.FRP未安装。\n2.杀毒软件误删。\n\n请把FRP文件放在CPL/frp文件。"
                );
                LaunchButton.Click += Launch;
                return;
            }
            if (!File.Exists(SetPath.frpIniPath))
            {
                LaunchButton.Content = "未发现配置文件";
                LaunchButton.Click += Launch;
                return;
            }
            //创建ini实例
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(SetPath.frpIniPath);
            LaunchButton.Content = "正在启动中...";
            if (i == 5)
            {
                i = 0;
            }
            i++;
            string logs = Path.Combine(SetPath.CPLPath, i + ".logs");
            ProcessStartInfo processInfo = new(
                "cmd.exe",
                "/c " + SetPath.frpPath + " -c " + SetPath.frpIniPath + " >" + logs + " 2>&1"
            )
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            using (Process process = new())
            {
                process.StartInfo = processInfo;
                process.Start();
                LaunchButton.Content = "点击关闭 frpc";
                LaunchButton.Click += Killfrp;
            }
        }

        private void Killfrp(object sender, RoutedEventArgs e)
        {
            LaunchButton.Click -= Killfrp;
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
            LaunchButton.Content = "启动FRP";
            LaunchButton.Click += Launch;
        }
    }
}
