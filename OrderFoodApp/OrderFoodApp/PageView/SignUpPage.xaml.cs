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
    public partial class SignUpPage : ContentPage
    {
        API firebase = new API();
        public SignUpPage()
        {

            InitializeComponent();
        }

        private async void Signup_Clicked(object sender, EventArgs e)
        {
            var register = await firebase.RegisterUser(Email.Text,UsrName.Text,Pwd.Text);
            if (register)
            {
                await DisplayAlert("Thông báo", "Đăng ký thành công", "Ok");
                await Navigation.PushAsync(new LoginPage());
            }
        }
    }
}