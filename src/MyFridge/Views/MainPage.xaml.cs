using Xamarin.Forms;

namespace MyFridge.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var itemsListView = this.FindByName<ListView>("itemsListView");

            itemsListView.ItemTapped += (s, e) =>
            {
                if (e.Item != null)
                {
                    itemsListView.SelectedItem = null;
                }
            };
        }
    }
}
