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
    public partial class HomePage : ContentPage
    {
        API firebase = new API();


        public HomePage()
        {
            InitializeComponent();

        }
        public List<Item> LstItems { get; set; }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var allItems = await firebase.GetAllItem();
            LstItems = allItems;
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
        public Frame frame;
        private void categoryItem_Tapped(object sender, EventArgs e)
        {
            if (frame == null)
            {
                frame = frame1;
            }
            Frame tappedFrame = (sender as Frame);
            var stackLayout = tappedFrame.Content as StackLayout;
            var label = stackLayout.Children[2] as Label;

            frame.BackgroundColor = Color.White;
            tappedFrame.BackgroundColor = Color.FromHex("#6CB0C5");
            frame = tappedFrame;
        }

        private async void Addtocart_Tapped(object sender, EventArgs e)
        {
            Image img = (Image)sender;
            await img.ScaleTo(1.1, 100);
            await img.ScaleTo(1.0, 100);
            Item selectedItem = (Item)img.BindingContext;

            var addtocart = await firebase.AddToCart(selectedItem);
            if (addtocart)
            {
                await DisplayAlert("Thông báo", "Thêm thành công", "Ok");
            }
        }
    }
}
