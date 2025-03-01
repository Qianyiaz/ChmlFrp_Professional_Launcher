using System.Windows.Controls;

namespace ChmlFrp_Professional_Launcher.Pages
{
    /// <summary>
    /// SettingHomePage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingHomePage : Page
    {
        private Reminding Reminding = new();
        private App App = new();

        public SettingHomePage()
        {
            InitializeComponent();
            Reminding.LogsOutputting("进入SettingHomePage");
        }
    }
}