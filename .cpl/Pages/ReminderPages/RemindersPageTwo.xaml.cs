﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChmlFrp_Professional_Launcher.Pages
{
    /// <summary>
    /// RemindersPageTwo.xaml 的交互逻辑
    /// </summary>
    public partial class RemindersPageTwo : Page
    {
        public RemindersPageTwo()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow MainWindow = Application.Current.MainWindow as MainWindow;
            MainWindow.DragMove();
        }

        private void Yes_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
