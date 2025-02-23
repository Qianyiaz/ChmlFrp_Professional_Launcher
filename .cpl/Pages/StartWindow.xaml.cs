using System.Windows;

namespace ChmlFrp_Professional_Launcher
{
    public partial class StartWindow : Window
    {
        private InitializeClass InitializeClass = new();

        public StartWindow()
        {
            InitializeComponent();
            InitializeClass.Initialize();
        }
    }
}
