using MyFridge.Domain;
using Xamarin.Forms;

namespace MyFridge.Views
{
    public class MainPage : ContentPage
    {
        private readonly ListView _listView;

        public MainPage()
        {
            var layout = new StackLayout();


            _listView = new ListView { RowHeight = 40, ItemTemplate = new DataTemplate(typeof(ItemCell)) };

            layout.Children.Add(_listView);
            layout.VerticalOptions = LayoutOptions.FillAndExpand;

            Content = layout;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _listView.ItemsSource = new[]
            {
                new Item()
                {
                    Name = "Название 1",
                    Price = 150.ToString()
                },
                new Item()
                {
                    Name = "Название 2",
                    Price = 250.ToString()
                },
                new Item()
                {
                    Name = "Название 3",
                    Price = 350.ToString()
                }
            };
        }
    }
}