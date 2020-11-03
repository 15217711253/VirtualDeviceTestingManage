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
    /// HTXDeviceView.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(IDeviceStatusView))]
    public partial class HTXDeviceView : Window, IDeviceStatusView
    {
        public HTXDeviceView()
        {
            base.Name = "HTX";
            InitializeComponent();
        }
        public HTXDeviceView(VrDeviceDbugViewModel vrDeviceDbugViewModel)
        {

            InitializeComponent();
            base.Name = "HTX";
            DeBugUserControl.DataContext = vrDeviceDbugViewModel;

        }

        private void RandomButton_Click(object sender, RoutedEventArgs e)
        {
            Random ran = new Random();
            this.PowerCheckBox.IsChecked = (ran.Next() % 2 == 0);
            this.warm1.IsChecked = (ran.Next() % 2 == 0);
            this.warm2.IsChecked = (ran.Next() % 2 == 0);
            this.warm3.IsChecked = (ran.Next() % 2 == 0);
            this.warm4.IsChecked = (ran.Next() % 2 == 0);
            this.warm5.IsChecked = (ran.Next() % 2 == 0);
            this.warm6.IsChecked = (ran.Next() % 2 == 0);
            this.warm7.IsChecked = (ran.Next() % 2 == 0);
            this.warm8.IsChecked = (ran.Next() % 2 == 0);
            this.warm9.IsChecked = (ran.Next() % 2 == 0);
            this.warm10.IsChecked = (ran.Next() % 2 == 0);
            this.warm11.IsChecked = (ran.Next() % 2 == 0);
            this.warm12.IsChecked = (ran.Next() % 2 == 0);
            this.warm13.IsChecked = (ran.Next() % 2 == 0);
            this.warm14.IsChecked = (ran.Next() % 2 == 0);
            this.warm15.IsChecked = (ran.Next() % 2 == 0);
            this.warm16.IsChecked = (ran.Next() % 2 == 0);
            this.ModeWrong.IsChecked = (ran.Next() % 2 == 0);
        }

        private void NorMalButton_Click(object sender, RoutedEventArgs e)
        {
            this.PowerCheckBox.IsChecked = true;
            this.warm1.IsChecked = true;
            this.warm2.IsChecked = true;
            this.warm3.IsChecked = true;
            this.warm4.IsChecked = true;
            this.warm5.IsChecked = true;
            this.warm6.IsChecked = true;
            this.warm7.IsChecked = true;
            this.warm8.IsChecked = true;
            this.warm9.IsChecked = true;
            this.warm10.IsChecked =true;
            this.warm11.IsChecked =true;
            this.warm12.IsChecked =true;
            this.warm13.IsChecked =true;
            this.warm14.IsChecked = true;
            this.warm15.IsChecked = true;
            this.warm16.IsChecked = true;
            this.ModeWrong.IsChecked = false;
        }                        
    }                            
}                                
                                 