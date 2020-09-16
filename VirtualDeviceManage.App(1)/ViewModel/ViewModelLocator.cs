
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using VirtualDeviceManage.App.CommProviders;
using VirtualDeviceTestingManage.Dal;
using System.Collections.Generic;
using VirtualDeviceManage.App.ViewModel.VirtualDevice;

namespace VirtualDeviceManage.App.ViewModel
{

    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register(GetDbContext);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<VrDeviceLsViewModel>();
            SimpleIoc.Default.Register<ProtocolCboxViewModel>();
            // Ioc ×¢Èë ICommProtocol
            //SimpleIoc.Default.Register(GetCommProtocols);
        }



        private static VirtualDeviceManageContext context;

        //private List<ICommProtocol> GetCommProtocols()
        //{
        //    var s = this.GetType().Assembly;
        //    var catalog = new AssemblyCatalog(this.GetType().Assembly);
        //    var container = new CompositionContainer(catalog);
        //    var commProtocols = container.GetExportedValues<ICommProtocol>();

        //    return commProtocols.ToList();
        //}

        private VirtualDeviceManageContext GetDbContext()
        {
            if (context == null)
                context = new VirtualDeviceManageContext();

            return context;
        }
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public VrDeviceLsViewModel VirtualDeviceList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<VrDeviceLsViewModel>();
            }
        }
        
        public ProtocolCboxViewModel ProtocolCbox
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ProtocolCboxViewModel>();
            }
        }

        public static void Cleanup()
        {
            context.DisposeAsync();
        }
    }
}