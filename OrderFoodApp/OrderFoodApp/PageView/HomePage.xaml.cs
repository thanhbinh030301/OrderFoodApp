﻿using System;
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
        public List<Item> LstItems1 { get; set; }
        public List<Item> LstItems2 { get; set; }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var allItems = await firebase.GetAllItem();
            var item = await firebase.GetType();
            LstItems1 = allItems;
            LstItems2 = item;
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

        private void categoryItem_Tapped(object sender, EventArgs e)
        {

            Frame tappedFrame = (sender as Frame);
            var stackLayout = tappedFrame.Content as StackLayout;
            var label = stackLayout.Children[2] as Label;
            Navigation.PushAsync(new ItemPage(label.Text));
            
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
