using Android.App;
using Android.OS;
using Toasts.Forms.Plugin.Droid;
using Xamarin.Forms.Platform.Android;

namespace MyFridge.Droid
{
    [Activity(Label = "MyFridge", MainLauncher = true)]
    public class MainActivity : FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);
            ToastNotificatorImplementation.Init(); //you can pass additional parameters here

            SetPage(App.GetMainPage());
        }
    }
}