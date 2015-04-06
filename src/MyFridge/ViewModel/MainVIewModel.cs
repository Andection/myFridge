using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MyFridge.Annotations;
using MyFridge.Services;
using Toasts.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace MyFridge.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IScannerService _scannerService;
        private readonly ProductService _productService;
        private readonly IToastNotificator _toastNotificationService;
        private string _itemToAdd;
        private ICommand _addCommand;
        private ICommand _scanCommand;
        public MainViewModel(IScannerService scannerService, ProductService productService, IToastNotificator toastNotificationService)
        {
            _scannerService = scannerService;
            _productService = productService;
            _toastNotificationService = toastNotificationService;
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
            get { return _addCommand ?? (_addCommand = new Command(OnAdd, p => !string.IsNullOrWhiteSpace(ItemToAdd))); }
        }

        public ICommand ScanCommand
        {
            get { return _scanCommand ?? (_scanCommand = new Command(OnScan)); }
        }

        private void OnAdd(object parameter)
        {
            Items.Add(new ItemViewModel(ItemToAdd, this));
            ItemToAdd = string.Empty;
        }

        private async void OnScan()
        {
            var scanResult = await _scannerService.Scan();

            if (scanResult.IsSuccess)
            {
                IsBusy = true;
                try
                {
                    var productName = await _productService.FindByBarcode(scanResult.Barcode);
                    if (!string.IsNullOrWhiteSpace(productName))
                    {
                        Items.Add(new ItemViewModel(productName, this));
                    }
                    else
                    {
                        await _toastNotificationService.Notify(ToastNotificationType.Warning, "", "unknown position", TimeSpan.FromSeconds(2));
                    }
                }
                catch
                {
                    _toastNotificationService.Notify(ToastNotificationType.Warning, "", "can not connect to remote server", TimeSpan.FromSeconds(2));
                }
                IsBusy = false;
            }
            else
            {
                await _toastNotificationService.Notify(ToastNotificationType.Warning, "", scanResult.ErrorMessage, TimeSpan.FromSeconds(2));
            }
        }

        public ObservableCollection<ItemViewModel> Items { get; private set; }
    }

    public class ItemViewModel : INotifyPropertyChanged
    {
        private readonly MainViewModel _host;
        private ICommand _removeItemCommand;

        public ItemViewModel(string name, MainViewModel host)
        {
            _host = host;
            Name = name;
        }

        public string Name { get; private set; }
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

    public class BaseViewModel : INotifyPropertyChanged
    {
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        protected void BusyIndication(Action action)
        {
            IsBusy = true;

            try
            {
                action();
            }
            finally
            {
                IsBusy = false;
            }
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