using Xamarin.Forms;

namespace MyFridge.Views
{
    public class ItemCell : ViewCell
    {
        public ItemCell()
        {
            var nameLabel = new Label
            {
                YAlign = TextAlignment.Center
            };
            nameLabel.SetBinding(Label.TextProperty, "Name");

            var priceLabel = new Label
            {
                YAlign = TextAlignment.Center
            };
            priceLabel.SetBinding(Label.TextProperty, "Price");

            var layout = new StackLayout
            {
                Padding = new Thickness(20, 0, 0, 0),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Children = {nameLabel, priceLabel}
            };
            View = layout;
        }

        protected override void OnBindingContextChanged()
        {
            // Fixme : this is happening because the View.Parent is getting 
            // set after the Cell gets the binding context set on it. Then it is inheriting
            // the parents binding context.
            View.BindingContext = BindingContext;
            base.OnBindingContextChanged();
        }
    }
}