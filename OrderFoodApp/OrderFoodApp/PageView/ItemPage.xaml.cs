using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrderFoodApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemPage : ContentPage
    {
        API firebase = new API();
        public ItemPage()
        {
            InitializeComponent();
        }

        public  ItemPage(string label)
        {
            InitializeComponent();
            Title = label;
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.White;
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Gray;
            InitView(label);
        }
        public List<Item> Lst { get; set; }
        public async void InitView(string label)
        {
            var allItems = await firebase.GetItems(label);
            if (label=="Tất cả")
            {
                allItems = await firebase.GetAllItem();
            }
            
            Lst = allItems;
            this.BindingContext = this;
        }

        private async void foodFrame_Tapped(object sender, EventArgs e)
        {
            Frame frame = (Frame)sender;
            await frame.ScaleTo(1.1, 100);
            await frame.ScaleTo(1.0, 100);
            Item selectedItem = (Item)frame.BindingContext;
            await Navigation.PushAsync(new DetailFoodPage(selectedItem));
        }

        private async void Addtocart_Tapped(object sender, EventArgs e)
        {
            Image img = (Image)sender;
            await img.ScaleTo(1.1, 100);
            await img.ScaleTo(1.0, 100);
            Item selectedItem = (Item)img.BindingContext;

            var addtocart = await firebase.AddToCart(selectedItem,1);
            if (addtocart)
            {
                await DisplayAlert("Thông báo", "Thêm thành công", "Ok");
            }
        }
    }
}