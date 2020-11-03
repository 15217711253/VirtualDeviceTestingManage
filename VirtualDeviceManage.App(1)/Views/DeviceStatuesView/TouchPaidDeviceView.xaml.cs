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
    /// TouchPaidDeviceView.xaml 的交互逻辑
    /// </summary>
    /// 
    [Export(typeof(IDeviceStatusView))]
    public partial class TouchPaidDeviceView : Window, IDeviceStatusView
    {
        public TouchPaidDeviceView()
        {
            base.Name = "TouchPaid";
            InitializeComponent();
        }
         public TouchPaidDeviceView(VrDeviceDbugViewModel vrDeviceDbugViewModel)
        {

            InitializeComponent();
            base.Name = "TouchPaid";
            DeBugUserControl.DataContext = vrDeviceDbugViewModel;
            this.RandomButton_Click(null, null);

        }

        private void RandomButton_Click(object sender, RoutedEventArgs e)
        {
            Random ran = new Random();
            this.PowerCheckBox.IsChecked = (ran.Next() % 2 == 0);
        }

        private void NorMalButton_Click(object sender, RoutedEventArgs e)
        {
            this.PowerCheckBox.IsChecked = true;
        }
    }
}
