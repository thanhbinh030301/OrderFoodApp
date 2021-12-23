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
    public partial class OrderSuccessPage : ContentPage
    {
        public OrderSuccessPage()
        {
            InitializeComponent();
        }

        private void ChangeToHome_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TabPage());
        }

        private void ChangeToSuccessPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OrderPage());
        }
    }
}