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
    /// ArtechDeviceView.xaml 的交互逻辑
    /// </summary>
    [Export(typeof(IDeviceStatusView))]
    public partial class ArtechDeviceView : Window, IDeviceStatusView
    {
 
        public ArtechDeviceView( )
        {
            base.Name = "Artech";
 
            InitializeComponent();
            
        }
        public ArtechDeviceView(VrDeviceDbugViewModel vrDeviceDbugViewModel)
        {

            InitializeComponent();
            base.Name = "Artech";
            DeBugUserControl.DataContext = vrDeviceDbugViewModel;

        }

 

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Select = (e.AddedItems[0] as ComboBoxItem).Content.ToString();
            switch (Select.ToString())
            {
                case "A1":
                    this.Statue_Mover_Grid.Visibility = Visibility.Visible;
                    this.Statue_SERVO_Grid.Visibility = Visibility.Collapsed;
                    break;
                case "A2":
                    this.Statue_Mover_Grid.Visibility = Visibility.Visible;
                    this.Statue_SERVO_Grid.Visibility = Visibility.Collapsed;
                    break;
                case "A3":
                    this.Statue_Mover_Grid.Visibility = Visibility.Collapsed;
                    this.Statue_SERVO_Grid.Visibility = Visibility.Collapsed;
                    break;
                case "A4":
                    this.Statue_SERVO_Grid.Visibility = Visibility.Visible;
                    this.Statue_Mover_Grid.Visibility = Visibility.Collapsed;
                    break;
                default:
                    break;
            }
        }


    }
}
