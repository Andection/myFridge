using Microsoft.Phone.Controls;
using Toasts.Forms.Plugin.WindowsPhone;
using Xamarin.Forms;


namespace MyFridge.WinPhone
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();

            Forms.Init();
            ToastNotificatorImplementation.Init(); //you can pass additional parameters here
            Content = MyFridge.App.GetMainPage().ConvertPageToUIElement(this);
        }
    }
}
