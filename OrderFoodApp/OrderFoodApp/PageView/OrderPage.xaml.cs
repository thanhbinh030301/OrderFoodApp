using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace OrderFoodApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderPage : ContentPage
    {
        API firebase = new API();
        public OrderPage()
        {
            InitializeComponent();
        }
        private ObservableCollection<Order> _LstOrder = new ObservableCollection<Order> { };
        public ObservableCollection<Order> LstOrder
        {
            get { return _LstOrder; }
            set
            {
                _LstOrder = value;
                OnPropertyChanged();
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var OrderItems = await firebase.GetOrderItem();
            ObservableCollection<Order> collection = new ObservableCollection<Order>(OrderItems);
            LstOrder = collection;
            this.BindingContext = this;
        }
    }
}