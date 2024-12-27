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
        private Reminding Reminding;
        private Timer timer;
        private SetPath SetPath;

        static bool IsProcessRunning(string processName, int count)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length >= count;
        }

        public StartWindow()
        {
            InitializeComponent();
            SetPath = new();Reminding = new();
            if (IsProcessRunning("ChmlFrpProfessionalLauncher", 2))
            {
                Reminding.LogsOutputting("检测到有两个ChmlFrpProfessionalLauncher退出"); Close();
            }
            //进入 2 s 的计时
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
                try
                {
                    if (!File.Exists(SetPath.CPLPath))
                    {
                        Directory.CreateDirectory(SetPath.CPLPath);
                    }
                    if (!File.Exists(SetPath.frpPath))
                    {
                        Directory.CreateDirectory(SetPath.frpPath);
                    }
                    if (!File.Exists(SetPath.pictures_path))
                    {
                        Directory.CreateDirectory(SetPath.pictures_path);
                    }
                    if (!File.Exists(SetPath.temp_path))
                    {
                        Directory.CreateDirectory(SetPath.temp_path);
                    }
                    if (!File.Exists(SetPath.setupIniPath))
                    {
                        data = new IniData();
                        data["ChmlFrp_Professional_Launcher Setup"]["Versions"] = "0.0.0.5";
                        parser.WriteFile(SetPath.setupIniPath, data);
                    }
                    File.WriteAllText(SetPath.logfilePath, string.Empty);
                }
                catch
                {
                    Reminding.LogsOutputting("文件夹或文件创建失败");
                }
                Reminding.LogsOutputting("文件夹已创建或创建成功");
                //创建日志文件
                try
                {
                    for (int i = 1; i < 6; i++)
                    {
                        if (!File.Exists(Path.Combine(SetPath.CPLPath, i + ".logs")))
                        {
                            File.Create(Path.Combine(SetPath.CPLPath, i + ".logs"));
                        }
                    }
                }
                catch
                {
                    Reminding.LogsOutputting("日志文件创建失败");
                }
                Reminding.LogsOutputting("日志文件已创建或创建成功");
                //界面退出，弹出MainWindow。
                Reminding.LogsOutputting("进入MainWindow");
                MainWindow MainWindow = new MainWindow();
                Window window = Window.GetWindow(this);
                window.Close();
                MainWindow.Show();
            });
        }
    }
}

