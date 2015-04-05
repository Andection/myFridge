using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
