/*
//                            _ooOoo_  
//                           o8888888o  
//                           88" . "88  
//                           (| -_- |)  
//                            O\ = /O  
//                        ____/`---'\____  
//                      .   ' \\| |// `.  
//                       / \\||| : |||// \  
//                     / _||||| -:- |||||- \  
//                       | | \\\ - /// | |  
//                     | \_| ''\---/'' | |  
//                      \ .-\__ `-` ___/-. /  
//                   ___`. .' /--.--\ `. . __  
//                ."" '< `.___\_<|>_/___.' >'"".  
//               | | : `- \`.;`\ _ /`;.`/ - ` : | |  
//                 \ \ `-. \_ __\ /__ _/ .-` / /  
//         ======`-.____`-.___\_____/___.-`____.-'======  
//                            `=---='  
//  
//         .............................................  
//                  佛祖保佑             永无BUG 
//          佛曰:  
//                  写字楼里写字间，写字间里程序员；  
//                  程序人员写程序，又拿程序换酒钱。  
//                  酒醒只在网上坐，酒醉还来网下眠；  
//                  酒醉酒醒日复日，网上网下年复年。  
//                  但愿老死电脑间，不愿鞠躬老板前；  
//                  奔驰宝马贵者趣，公交自行程序员。  
//                  别人笑我忒疯癫，我笑自己命太贱；  
//                  不见满街漂亮妹，哪个归得程序员？
*/

//using System;
//using System.Windows;

//namespace ChmlFrp_Professional_Launcher
//{
//    /// <summary>
//    /// Lógica de interacción para MainWindow.xaml
//    /// </summary>
//    public partial class MainWindow : Window
//    {

//        public MainWindow()
//        {
//            InitializeComponent();
//            PagesNavigation.Navigate(new Uri("Pages/LaunchPage.xaml", UriKind.RelativeOrAbsolute));
//        }

//        private void btnClose_Click(object sender, RoutedEventArgs e)
//        {
//            Close();
//        }

//        private void btnRestore_Click(object sender, RoutedEventArgs e)
//        {
//            if (WindowState == WindowState.Normal)
//                WindowState = WindowState.Maximized;
//            else
//                WindowState = WindowState.Normal;
//        }

//        private void btnMinimize_Click(object sender, RoutedEventArgs e)
//        {
//            WindowState = WindowState.Minimized;
//        }

//        private void rdLaunchPage_Click(object sender, RoutedEventArgs e)
//        {
//            rdNotes.IsChecked = false;
//            PagesNavigation.Navigate(new Uri("Pages/LaunchPage.xaml", UriKind.RelativeOrAbsolute));
//        }
//        private void rdNotes_Click(object sender, RoutedEventArgs e)
//        {
//            rdHome.IsChecked = false;
//            PagesNavigation.Navigate(new System.Uri("Pages/ConfigPage.xaml", UriKind.RelativeOrAbsolute));
//        }
//    }
//}
