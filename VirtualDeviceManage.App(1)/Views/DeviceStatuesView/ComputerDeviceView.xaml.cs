using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
using System.Windows.Shapes;
using VirtualDeviceManage.App.Interface;
using VirtualDeviceManage.App.ViewModel;

namespace VirtualDeviceManage.App.Views
{
    /// <summary>
    /// WComputerDeviceView.xaml 的交互逻辑
    /// </summary>
    /// 
    [Export(typeof(IDeviceStatusView))]
    public partial class ComputerDeviceView : Window, IDeviceStatusView
    {
        public ComputerDeviceView()
        {
            base.Name = "Computer";
            InitializeComponent();
        }
        public ComputerDeviceView(VrDeviceDbugViewModel vrDeviceDbugViewModel)
        {

            InitializeComponent();
            base.Name = "Computer";
            DeBugUserControl.DataContext = vrDeviceDbugViewModel;


            this.RandomButton_Click(null, null);
        }

        private void RandomButton_Click(object sender, RoutedEventArgs e)
        {
            Random ran = new Random();
            this.SoftWareOnlineRadioButton.IsChecked = (ran.Next() % 2 == 0);
            this.CpuRateTextBox.Text = (ran.Next(0, 1000)/10.0).ToString()+"%";
            this.CpuTempTextBox.Text = (ran.Next(0, 1000) / 10.0).ToString();
            this.MemoryRateTextBox.Text = (ran.Next(0, 1000) / 10.0).ToString() + "%";
            this.HardDiskRateTextBox.Text = (ran.Next(0, 1000) / 10.0).ToString() + "%";
        }

        private void NorMalButton_Click(object sender, RoutedEventArgs e)
        {
            this.SoftWareOnlineRadioButton.IsChecked = true;
            this.CpuRateTextBox.Text =  "50.0%";
            this.CpuTempTextBox.Text = "50.0";
            this.MemoryRateTextBox.Text =  "50.0%";
            this.HardDiskRateTextBox.Text =  "50.0%";
        }
    }
}
