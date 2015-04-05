using MyFridge.ViewModel;
using MyFridge.Views;
using Xamarin.Forms;

namespace MyFridge
{
    public class App
    {
        public static Page GetMainPage()
        {
            var navigationPage = new NavigationPage(new MainPage()
            {
                BindingContext = new MainViewModel()
            });
            return navigationPage;
        }
    }
}