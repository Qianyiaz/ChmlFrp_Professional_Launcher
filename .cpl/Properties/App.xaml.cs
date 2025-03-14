using System.Windows;

namespace ChmlFrp_Professional_Launcher
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        // 入口
        static App()
        {
            new MainClass.Paths();
        }
    }
}
