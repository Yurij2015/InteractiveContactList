using InteractiveContactList.Views.Forms;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InteractiveContactList
{
    public partial class App : Application
    {
        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("@31382e342e30QGScgoPzw/MTtjh4NgKPmbR5VMHN5sU+vsQUNM51YG8=");

            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = new NavigationPage(new MainPage());

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
