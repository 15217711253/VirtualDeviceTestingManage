/************************************************************************

*Copyright  (c) 2020   All Rights Reserved .
*CLR版本    ：4.0.30319.42000
*机器名称   ：JSOUND
*公司名称   : 
*命名空间   ：VirtualDeviceManage.App.ViewModel
*文件名称   ：VirtualDeviceListViewModel.cs
*版本号     : 2020|V1.0.0.0 

*=================================

*创 建 者    ：kayga.mo
*创建日期    ：2020/9/8 星期二 19:00:50 
*电子邮箱    ：mo.jj@topauthor.com
*个人主站    ：http://www.topauthor-tech.com
*功能描述    ：
*使用说明    ：

*=================================

*修改日期    ：2020/9/8 星期二 19:00:50 
*修改者      ：kayga.mo
*修改描述    ：
*版本号      : 2020|V1.0.0.0 

***********************************************************************/


using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualDeviceManage.App.Views;
using VirtualDeviceTestingManage.Common;
using VirtualDeviceTestingManage.Dal;
using VirtualDeviceTestingManage.Domain;

namespace VirtualDeviceManage.App.ViewModel
{
    public class VrDeviceLsViewModel : ViewModelBase
    {
        /*-------------------------------------- Fields ----------------------------------------*/

        /// <summary>
        /// 虚拟设备列表
        /// </summary>
        private ObservableCollection<VrDeviceViewModelBase> _VirtualDevices = 
            new ObservableCollection<VrDeviceViewModelBase>();

        /// <summary>
        /// 数据库操作类
        /// </summary>
        private VirtualDeviceManageContext DbContext;

        /// <summary>
        /// 选中单个
        /// </summary>
        private VrDeviceViewModelBase _SelectedItem;

        /*-------------------------------------Properties --------------------------------------*/

        /// <summary>
        /// 虚拟设备列表
        /// </summary>
        public ObservableCollection<VrDeviceViewModelBase> VirtualDevices
        {
            get 
            {
                return _VirtualDevices;
            }
            private set
            {
                _VirtualDevices = value;
            }
        }

        /// <summary>
        /// 选中单个
        /// </summary>
        public VrDeviceViewModelBase SelectedItem
        {
            get { return _SelectedItem; }
            set 
            { 
                Set(ref _SelectedItem, value); 
            }
        }

        public bool isAllSelected
        {
            get { return _isAllSelected; }
            set 
            {
                if (_isAllSelected != value)
                {
                    if (value)
                    {
                        VirtualDevices.ToList().ForEach(x => x.isSelected = true);

                    }
                    else
                    { 
                    
                        VirtualDevices.ToList().ForEach(x => x.isSelected = false);
                    }
                }
                this.RaisePropertyChanged("isAllSelected");
            }
        }

        private bool _isAllSelected;


        /// <summary>
        /// 生成测试列表
        /// </summary>
        public ObservableCollection<VrDeviceDbugViewModel> SelectedItems
        {
            get { return _SelectedItems; }
            set { Set(ref _SelectedItems, value); }
        }

        private ObservableCollection<VrDeviceDbugViewModel> _SelectedItems;





        /*------------------------------- Dependency Properties --------------------------------*/

        /*----------------------------------- Constructors -------------------------------------*/

        public VrDeviceLsViewModel()
        {
            DbContext = SimpleIoc.Default.GetInstance<VirtualDeviceManageContext>();
            ReNewViewModel();
        }

        /*---------------------------------- Public Methods ------------------------------------*/

        #region Device Methods

        public void ReNewViewModel()
        {
            var alist = DbContext.VirtualDevices.ToList();

            GlobalData.Ipadds = DbContext.VirtualDevices.ToList().ConvertAll(x => x.IpAddrees).ToArray();
            int ip_count = GlobalData.Ipadds.Length;

            string[] SubMarks = new string[ip_count];
            string[] GateWays = new string[ip_count];

            for (int i = 0; i < ip_count; i++)
            {
                SubMarks[i] = "255.255.255.0";
                GateWays[i] = "192.168.1.1";
            }


            NetWorkProvider.UpdateNetWorkIpAddress(
                GlobalData.Ipadds,
               SubMarks, GateWays
                );

            Thread.Sleep(5000);

            var list = DbContext.VirtualDevices.ToList().ConvertAll(p=>new VrDeviceViewModelBase(p));
            VirtualDevices = new ObservableCollection<VrDeviceViewModelBase>(list);


            this.RaisePropertyChanged("VirtualDevices");
        }


        private RelayCommand _AddCommand;

        /// <summary>
        /// Gets the AddCommand.
        /// </summary>
        public RelayCommand AddCommand
        {
            get
            {
                return _AddCommand
                    ?? (_AddCommand = new RelayCommand(
                    () =>
                    {
                        var view = new AddOrEditData();
                        var model = new VirtualNetworkDevice();
                        var viewmodel = new VrDeviceSampleViewModel(model);
                        view.DataContext = model;
                        if (view.ShowDialog() == true)
                        {
                            DbContext.Add(model);
                            DbContext.SaveChanges();
                            ReNewViewModel();
                            MessageBox.Show("True");
                        }
                        else
                        {
                            MessageBox.Show("False");
                        }


                    }));
            }
        }

        private RelayCommand _UpdateCommand;

        /// <summary>
        /// Gets the UpdateCommand.
        /// </summary>
        public RelayCommand UpdateCommand
        {
            get
            {
                return _UpdateCommand
                    ?? (_UpdateCommand = new RelayCommand(
                    () =>
                    {
                        if (SelectedItem == null)
                        {
                            MessageBox.Show("没有选中设备");
                            return;
                        }
                        var view = new AddOrEditData();
                        var model = SelectedItem.Source;
                        view.DataContext = model;
                        if (view.ShowDialog() == true)
                        {
                            DbContext.Update(model);
                            DbContext.SaveChanges();

                            ReNewViewModel();
                            MessageBox.Show("True");
                        }
                        else
                        {
                            MessageBox.Show("False");
                        }

                    }));
            }
        }


        private RelayCommand _DelCommand;

        /// <summary>
        /// Gets the DelCommand.
        /// </summary>
        public RelayCommand DelCommand
        {
            get
            {
                return _DelCommand
                    ?? (_DelCommand = new RelayCommand(
                    () =>
                    {
                        var checkItems = VirtualDevices.Where(x => x.isSelected == true).ToList().ConvertAll(p=>p.Source);

                        if (checkItems.Count > 0)
                        {
                            DbContext.RemoveRange(checkItems);
                            DbContext.SaveChanges();
                        }

                        ReNewViewModel();
                    }));
            }
        }



        #endregion

        #region Device Debug

        private RelayCommand _DebugCommand;

        /// <summary>
        /// Gets the IsCheckCommand.
        /// </summary>
        public RelayCommand DebugCommand
        {
            get
            {
                return _DebugCommand
                    ?? (_DebugCommand = new RelayCommand(
                    () =>
                    {


                        VrDeviceViewModelBase vr = new VrDeviceViewModelBase(new VirtualNetworkDevice());


                        
                        var list = VirtualDevices.Where(x => x.isSelected == true).ToList();
                        SelectedItems = new ObservableCollection<VrDeviceDbugViewModel>();


                        foreach (var item in list)
                        {
                            SelectedItems.Add(new VrDeviceDbugViewModel(item.Source));
                        }

                        //SelectedItems = new ObservableCollection<VrDeviceDbugViewModel>(
                        //                            list.ConvertAll(p=>
                        //                            (VrDeviceDbugViewModel)p
                        //                            ));

                        string s = string.Empty;
                        foreach (var i in SelectedItems)
                        {
                            s += $"{i.Source.Name}：{i.GetHashCode()}\r\n";
                        }


                        MessageBox.Show(s);
                    }));
            }
        }


        #endregion

        /*---------------------------------- Private Method ------------------------------------*/
    }
}
