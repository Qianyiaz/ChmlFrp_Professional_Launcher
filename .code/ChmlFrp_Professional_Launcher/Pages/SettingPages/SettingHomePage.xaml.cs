﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChmlFrp_Professional_Launcher.Pages
{
    /// <summary>
    /// SettingHomePage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingHomePage : Page
    {
        private Reminding Reminding = new();
        private SetPath SetPath = new();

        public SettingHomePage()
        {
            InitializeComponent();
            Reminding.LogsOutputting("进入SettingHomePage");
        }
    }
}
