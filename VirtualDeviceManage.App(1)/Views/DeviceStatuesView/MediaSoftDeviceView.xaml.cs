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
    /// MediaSoftDeviceView.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(IDeviceStatusView))]
    public partial class MediaSoftDeviceView : Window, IDeviceStatusView
    {
        public MediaSoftDeviceView()
        {
            base.Name = "MediaSoft";
            InitializeComponent();
        }

        public MediaSoftDeviceView(VrDeviceDbugViewModel vrDeviceDbugViewModel)
        {

            InitializeComponent();
            base.Name = "MediaSoft";
            DeBugUserControl.DataContext = vrDeviceDbugViewModel;

        }

        private void RandomButton_Click(object sender, RoutedEventArgs e)
        {
            Random ran = new Random();
            this.MediaSoftComboBox.SelectedIndex = ran.Next(0, this.MediaSoftComboBox.Items.Count);
        }

        private void NorMalButton_Click(object sender, RoutedEventArgs e)
        {
            this.MediaSoftComboBox.SelectedIndex = 0;
        }
    }
}
