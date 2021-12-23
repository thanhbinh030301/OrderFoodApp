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
    public partial class DetailFoodPage : ContentPage
    {
        API firebase = new API();

        public DetailFoodPage()
        {
            InitializeComponent();
        }

        public Item temp;
        public DetailFoodPage(Item item)
        {
            InitializeComponent();
            ImgItem.Source = item.Img;
            DescrItem.Text = item.Descr;
            NameItem.Text = item.Name;
            heart_click.Source = (item.Favourite) ? "CompleteHeart.png" : "EmptyHeart.png";
            PriceItem.Text = item.Price.ToString();
            temp = item;
        }

        private async void HeartTapped(object sender, EventArgs e)
        {
            
            Image img = (Image)sender;
            await img.ScaleTo(1.1, 100);
            await img.ScaleTo(1.0, 100);

            await firebase.UpdateCart(temp);
            if (heart_click.Source.ToString() == "File: CompleteHeart.png")
            {
                heart_click.Source = "EmptyHeart.png";
            }
            else
            {
                if (heart_click.Source.ToString() == "File: EmptyHeart.png")
                {
                    heart_click.Source = "CompleteHeart.png";
                }
            }
        }

        private void CartTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CartPage());
        }
    }
}