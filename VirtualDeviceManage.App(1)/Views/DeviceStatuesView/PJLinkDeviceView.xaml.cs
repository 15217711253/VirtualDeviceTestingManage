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
    }
}
