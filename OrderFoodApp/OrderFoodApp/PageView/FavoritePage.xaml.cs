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
    public partial class FavoritePage : ContentPage
    {
        API firebase = new API();
        public FavoritePage()
        {
            InitializeComponent();
        }
        private ObservableCollection<Item> _LstFavourite = new ObservableCollection<Item> { };
        public ObservableCollection<Item> LstFavourite
        {
            get { return _LstFavourite; }
            set
            {
                _LstFavourite = value;
                OnPropertyChanged();
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var FavouriteItems = await firebase.GetFavouriteItem();
            ObservableCollection<Item> collection = new ObservableCollection<Item>(FavouriteItems);
            LstFavourite = collection;

            int count = FavouriteItems.Count();
            countFavorItem.Text = count.ToString() + " Favorite Foods";
            this.BindingContext = this;

        }

        private async void Item_Tapped(object sender, EventArgs e)
        {
            Frame frame = (Frame)sender;
            await frame.ScaleTo(1.1, 100);
            await frame.ScaleTo(1.0, 100);
            Item selectedItem = (Item)frame.BindingContext;
            await Navigation.PushAsync(new DetailFoodPage(selectedItem));
        }
    }
}