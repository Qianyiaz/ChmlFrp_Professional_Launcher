using System.Windows.Controls;

namespace ChmlFrp_Professional_Launcher.Pages
{
    /// <summary>
    /// SettingHomePage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingHomePage : Page
    {
        private Reminders Reminders = new();

        public SettingHomePage()
        {
            InitializeComponent();
            Reminders.LogsOutputting("进入SettingHomePage");
        }
    }
}