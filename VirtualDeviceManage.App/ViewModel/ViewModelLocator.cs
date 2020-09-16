
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System.Linq;
using VirtualDeviceTestingManage.Dal;

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
        }

        private static VirtualDeviceManageContext context;


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
        
        public static void Cleanup()
        {
            context.DisposeAsync();
        }
    }
}