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
            number.Text = "1";
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

        private async void CartTapped(object sender, EventArgs e)
        {
            Frame img = (Frame)sender;
            await img.ScaleTo(1.1, 100);
            await img.ScaleTo(1.0, 100);
            var addtocart = await firebase.AddToCart(temp,count);
            if (addtocart)
            {
                await DisplayAlert("Thông báo", "Thêm thành công", "Ok");
            }
        }

        public int count = 1;
        private void minus_Tapped(object sender, EventArgs e)
        {
            if (count>1)
            {
                count--;
            }
            number.Text = count.ToString();
            PriceItem.Text = (temp.Price * count).ToString();
        }

        private void plus_Tapped(object sender, EventArgs e)
        {
            count++;
            number.Text = count.ToString();
            PriceItem.Text = (temp.Price * count).ToString();
        }
    }
}