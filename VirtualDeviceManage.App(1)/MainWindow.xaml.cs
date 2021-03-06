﻿using GalaSoft.MvvmLight.Ioc;
using System;
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
using VirtualDeviceTestingManage.Common;
using VirtualDeviceTestingManage.Dal;

namespace VirtualDeviceManage.App
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        protected override void OnClosed(EventArgs e)
        {
            NetWorkProvider.setNetWorkDHCP();
            System.Environment.Exit(0);
            base.OnClosed(e);
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
     


        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
        }
    }
}
