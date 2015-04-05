using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MyFridge.Annotations;
using Xamarin.Forms;

namespace MyFridge.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _itemToAdd;
        private ICommand _addCommand;

        public MainViewModel()
        {
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

        private void OnAdd()
        {
            Items.Add(new ItemViewModel(ItemToAdd, this));
            ItemToAdd = string.Empty;
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