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
    /// DeviceStauteSampleView.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(IDeviceStatusView))]
    public partial class PJLinkDeviceView : Window, IDeviceStatusView
    {
        public PJLinkDeviceView()
        {
            base.Name = "PJLink";
            InitializeComponent();
        }
        public PJLinkDeviceView(VrDeviceDbugViewModel vrDeviceDbugViewModel)
        {

            InitializeComponent();
            base.Name = "PJLink";
            DeBugUserControl.DataContext = vrDeviceDbugViewModel;

        }

        private void RandomButton_Click(object sender, RoutedEventArgs e)
        {
            Random ran = new Random();
            this.PowerRadioButton.IsChecked = (ran.Next() % 2 == 0);
            this.LampTimeTextBox.Text = ran.Next(0, 99999).ToString();
            this.FanErrorComboBox.SelectedIndex = ran.Next(0,this.FanErrorComboBox.Items.Count);
            this.FilterErrorComboBox.SelectedIndex = ran.Next(0, this.FilterErrorComboBox.Items.Count);
            this.LightErrorComboBox.SelectedIndex = ran.Next(0, this.LightErrorComboBox.Items.Count);
            this.OpenCoverErrorComboBox.SelectedIndex = ran.Next(0, this.OpenCoverErrorComboBox.Items.Count);
            this.OtherErrorComboBox.SelectedIndex = ran.Next(0, this.OtherErrorComboBox.Items.Count);
            this.TempErrorComboBox.SelectedIndex = ran.Next(0, this.TempErrorComboBox.Items.Count);
 
        }
        
        private void NorMalButton_Click(object sender, RoutedEventArgs e)
        {
            this.PowerRadioButton.IsChecked = true;
            this.LampTimeTextBox.Text = "6000";
            this.FanErrorComboBox.SelectedIndex = 0;
            this.FilterErrorComboBox.SelectedIndex = 0;
            this.LightErrorComboBox.SelectedIndex = 0;
            this.OpenCoverErrorComboBox.SelectedIndex = 0;
            this.OtherErrorComboBox.SelectedIndex = 0;
            this.TempErrorComboBox.SelectedIndex = 0;

        }
    }
}
