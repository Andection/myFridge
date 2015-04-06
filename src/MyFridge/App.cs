using MyFridge.Services;
using MyFridge.ViewModel;
using MyFridge.Views;
using Toasts.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace MyFridge
{
    public class App
    {
        public static Page GetMainPage()
        {
            var mainViewModel = new MainViewModel(DependencyService.Get<IScannerService>(), new ProductService(), DependencyService.Get<IToastNotificator>());

            var navigationPage = new NavigationPage(new MainPage
            {
                BindingContext = mainViewModel
            });
            return navigationPage;
        }
    }
}