using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using IniParser;
using IniParser.Model;

namespace ChmlFrp_Professional_Launcher
{
    public partial class StartWindow : Window
    {
        private Timer timer;
        private ClienterClass clienterClass;

        static bool IsProcessRunning(string processName, int count)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length >= count;
        }

        public StartWindow()
        {
            InitializeComponent();
            clienterClass = new ClienterClass();
            if (IsProcessRunning("ChmlFrpLauncher", 2)) Close(); //检测到有两个ChmlFrpLauncher就退出
                                                                 //进入 1 s 的计时
            timer = new Timer(TimerCallback, null, TimeSpan.FromSeconds(2), Timeout.InfiniteTimeSpan);
        }

        private void TimerCallback(object state)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                //创建ini实例
                var parser = new FileIniDataParser();
                IniData data;
                //检测是否有相关配置文件
                if (!File.Exists(clienterClass.CPLPath) && !File.Exists(clienterClass.frpPath) && !File.Exists(clienterClass.setupIniPath) && !File.Exists(clienterClass.pictures_path) && !File.Exists(clienterClass.temp_path))
                {
                    Directory.CreateDirectory(clienterClass.CPLPath); Directory.CreateDirectory(clienterClass.frpPath); Directory.CreateDirectory(clienterClass.pictures_path); Directory.CreateDirectory(clienterClass.temp_path); //创建文件夹
                    data = new IniData();
                    data["ChmlFrp_Professional_Launcher Setup"]["Versions"] = "0.0.0.0.3";
                    parser.WriteFile(clienterClass.setupIniPath, data);
                }
                for (int i = 1; i < 6; i++)
                {
                    if (!File.Exists(Path.Combine(clienterClass.CPLPath, i + ".logs")))
                    {
                        File.Create(Path.Combine(clienterClass.CPLPath, i + ".logs")); //创建logs日志文件
                    }
                }
                //界面退出，弹出MainWindow。
                MainWindow MainWindow = new MainWindow();
                Window window = Window.GetWindow(this);
                window.Close();
                MainWindow.Show();
            });
        }
    }
}

