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

 
 

        private void RandomButton_Click(object sender, RoutedEventArgs e)
        {
            Random ran = new Random();
            this.LiveControlRadioButton.IsChecked = (ran.Next() % 2 == 0);
          
            this.ShowIdTextBox.Text = ran.Next(0, 15).ToString();
            
            this.DeviceIdTextBox.Text = "0"+ran.Next(0, 6).ToString();

            this.DeviceIdTextBox.Text = "01";
            this.ModeComboBox.SelectedIndex = ran.Next(0, this.ModeComboBox.Items.Count);
            this.EStopComboBox.SelectedIndex = ran.Next(0, this.EStopComboBox.Items.Count);
            this.StatusComboBox.SelectedIndex = ran.Next(0, this.StatusComboBox.Items.Count);
            this.APUComboBox.SelectedIndex = ran.Next(0, this.APUComboBox.Items.Count);
            this.VolumeComboBox.SelectedIndex = ran.Next(0, this.VolumeComboBox.Items.Count);
            this.Statue_Oiler1.IsChecked = (ran.Next() % 2 == 0);
            this.Statue_Oiler2.IsChecked = (ran.Next() % 2 == 0);
            this.Statue_Oiler3.IsChecked = (ran.Next() % 2 == 0);
            this.Statue_Oiler4.IsChecked = (ran.Next() % 2 == 0);
            this.Statue_Motor1.IsChecked = (ran.Next() % 2 == 0);
            this.Statue_Motor2.IsChecked = (ran.Next() % 2 == 0);
            this.Statue_Motor3.IsChecked = (ran.Next() % 2 == 0);
            this.Statue_Motor4.IsChecked = (ran.Next() % 2 == 0);
            this.Statue_Motor5.IsChecked = (ran.Next() % 2 == 0);
            this.Statue_Motor6.IsChecked = (ran.Next() % 2 == 0);
            this.Statue_Motor7.IsChecked = (ran.Next() % 2 == 0);
            this.Statue_Motor8.IsChecked = (ran.Next() % 2 == 0);
            this.Statue_Motor9.IsChecked = (ran.Next() % 2 == 0);
            this.Statue_Motor10.IsChecked = (ran.Next() % 2 == 0);
            this.Statue_Motor11.IsChecked = (ran.Next() % 2 == 0);
            this.Statue_Motor12.IsChecked = (ran.Next() % 2 == 0);
            this.Statue_Motor13.IsChecked = (ran.Next() % 2 == 0);
            this.Statue_Motor14.IsChecked = (ran.Next() % 2 == 0);
            this.Statue_Motor15.IsChecked = (ran.Next() % 2 == 0);
            this.Statue_Motor16.IsChecked = (ran.Next() % 2 == 0);
            this.Warm1.IsChecked = (ran.Next() % 2 == 0);
            this.Warm2.IsChecked = (ran.Next() % 2 == 0);
            this.Warm3.IsChecked = (ran.Next() % 2 == 0);
            this.Warm4.IsChecked = (ran.Next() % 2 == 0);
            this.Warm5.IsChecked = (ran.Next() % 2 == 0);
            this.Warm6.IsChecked = (ran.Next() % 2 == 0);
            this.Warm7.IsChecked = (ran.Next() % 2 == 0);
            this.Warm8.IsChecked = (ran.Next() % 2 == 0);
            this.Warm9.IsChecked = (ran.Next() % 2 == 0);
            this.Warm10.IsChecked = (ran.Next() % 2 == 0);
            this.Warm11.IsChecked = (ran.Next() % 2 == 0);
            this.Warm12.IsChecked = (ran.Next() % 2 == 0);
            this.Warm13.IsChecked = (ran.Next() % 2 == 0);
            this.Warm14.IsChecked = (ran.Next() % 2 == 0);
            this.Warm15.IsChecked = (ran.Next() % 2 == 0);
            this.Warm16.IsChecked = (ran.Next() % 2 == 0);
            


        }

        private void NorMalButton_Click(object sender, RoutedEventArgs e)
        {
            this.LiveControlRadioButton.IsChecked = true;
            this.ShowIdTextBox.Text = "01";
            this.DeviceIdTextBox.Text = "01"  ;
            this.ModeComboBox.SelectedIndex = this.ModeComboBox.Items.Count-1;
            this.EStopComboBox.SelectedIndex =  this.EStopComboBox.Items.Count-1;
            this.StatusComboBox.SelectedIndex =  this.StatusComboBox.Items.Count-1;
            this.APUComboBox.SelectedIndex =this.APUComboBox.Items.Count-1;
            this.VolumeComboBox.SelectedIndex = this.VolumeComboBox.Items.Count-1;
            this.Statue_Oiler1.IsChecked = false;
            this.Statue_Oiler2.IsChecked = false;
            this.Statue_Oiler3.IsChecked = false;
            this.Statue_Oiler4.IsChecked = false;
            this.Statue_Motor1.IsChecked = false;
            this.Statue_Motor2.IsChecked = false;
            this.Statue_Motor3.IsChecked = false;
            this.Statue_Motor4.IsChecked = false;
            this.Statue_Motor5.IsChecked = false;
            this.Statue_Motor6.IsChecked = false;
            this.Statue_Motor7.IsChecked = false;
            this.Statue_Motor8.IsChecked = false;
            this.Statue_Motor9.IsChecked = false;
            this.Statue_Motor10.IsChecked = false;
            this.Statue_Motor11.IsChecked = false;
            this.Statue_Motor12.IsChecked = false;
            this.Statue_Motor13.IsChecked = false;
            this.Statue_Motor14.IsChecked = false;
            this.Statue_Motor15.IsChecked = false;
            this.Statue_Motor16.IsChecked = false;
            this.Warm1.IsChecked = false;
            this.Warm2.IsChecked = false;
            this.Warm3.IsChecked = false;
            this.Warm4.IsChecked = false;
            this.Warm5.IsChecked = false;
            this.Warm6.IsChecked = false;
            this.Warm7.IsChecked = false;
            this.Warm8.IsChecked = false;
            this.Warm9.IsChecked = false;
            this.Warm10.IsChecked = false;
            this.Warm11.IsChecked = false;
            this.Warm12.IsChecked = false;
            this.Warm13.IsChecked = false;
            this.Warm14.IsChecked = false;
            this.Warm15.IsChecked = false;
            this.Warm16.IsChecked = false;
        }
    }
}
