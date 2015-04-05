using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.Windows.Input;
using MyFridge.Annotations;
using MyFridge.Services;
using Xamarin.Forms;

namespace MyFridge.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IScannerService _scannerService;
        private readonly ProductService _productService;
        private string _itemToAdd;
        private ICommand _addCommand;
        private ICommand _scanCommand;
        public MainViewModel(IScannerService scannerService, ProductService productService)
        {
            _scannerService = scannerService;
            _productService = productService;
            Items = new ObservableCollection<ItemViewModel>();
        }

        public string ItemToAdd
        {
            get { return _itemToAdd; }
            set
            {
                _itemToAdd = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddItemCommand
        {
            get { return _addCommand ?? (_addCommand = new Command(OnAdd)); }
        }

        public ICommand ScanCommand
        {
            get { return _scanCommand ?? (_scanCommand = new Command(OnScan)); }
        }

        private void OnAdd()
        {
            Items.Add(new ItemViewModel(ItemToAdd, this));
            ItemToAdd = string.Empty;
        }

        private async void OnScan()
        {
            var scanResult = await _scannerService.Scan();

            if (scanResult.IsSuccess)
            {
                var productName = await _productService.FindByBarcode(scanResult.Barcode);

                Items.Add(new ItemViewModel(productName, this));
            }
            else
            {
                //todo: show toast
            }
        }

        public ObservableCollection<ItemViewModel> Items { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ItemViewModel : INotifyPropertyChanged
    {
        private readonly MainViewModel _host;
        private ICommand _removeItemCommand;

        public ItemViewModel(string name,MainViewModel host)
        {
            _host = host;
            Name = name;
        }

        public string Name { get;private set; }
        public ICommand RemoveCommand
        {
            get { return _removeItemCommand ?? (_removeItemCommand = new Command(OnRemove)); }
        }

        private void OnRemove()
        {
            _host.Items.Remove(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}