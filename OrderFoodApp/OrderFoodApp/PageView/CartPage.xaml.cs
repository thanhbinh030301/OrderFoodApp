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
    public partial class CartPage : ContentPage
    {
        API firebase = new API();
        public CartPage()
        {
            InitializeComponent(); 
        }
        public int total;

        private ObservableCollection<Cart> _LstCart = new ObservableCollection<Cart> { };
        public ObservableCollection<Cart> LstCart { 
            get { return _LstCart; }
            set
            {
                _LstCart = value;
                OnPropertyChanged();
            }
        }

        protected async override void OnAppearing()
        {
            total = 0;
            base.OnAppearing();
            var CartItems = await firebase.GetCartItem();
            ObservableCollection<Cart> collection = new ObservableCollection<Cart>(CartItems);
            LstCart = collection;
            this.BindingContext = this;

            foreach(Cart cart in CartItems)
            {
                total += cart.Price;
            }
            PriceTotal.Text = total.ToString();
        }
        private async void ChangeSucessPage_Clicked(object sender, EventArgs e)
        {
            var CartItems = await firebase.GetCartItem();
            foreach (Cart cart in CartItems)
            {
                await firebase.AddToOrder(cart);
                await firebase.DeleteCart(cart.Name);
            }
           await Navigation.PushAsync(new OrderSuccessPage());
        }

        private async void DeleteCart_Tapped(object sender, EventArgs e)
        {
            total = 0;
            Image img = (Image)sender;
            await img.ScaleTo(1.1, 100);
            await img.ScaleTo(1.0, 100);
            Cart selectedItem = (Cart)img.BindingContext;

            await firebase.DeleteCart(selectedItem.Name);
            var CartItems = await firebase.GetCartItem();
            ObservableCollection<Cart> collection = new ObservableCollection<Cart>(CartItems);
            LstCart = collection;
            foreach (Cart cart in CartItems)
            {
                total += cart.Price;
            }
            PriceTotal.Text = total.ToString();
        }
    }
}