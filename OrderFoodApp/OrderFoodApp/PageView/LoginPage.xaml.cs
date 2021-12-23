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
    public partial class LoginPage : ContentPage
    {
        API firebase = new API();
        public LoginPage()
        {
            InitializeComponent();
        }

        private void ButtonSignUp_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }

        private void ButtonForgot_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ForgetPasswordPage());
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            var login = await firebase.LoginUser(UsrName.Text, Pwd.Text);
            if (login)
            {
                await DisplayAlert("Thông báo", "Đăng nhập thành công", "Ok");
                await Navigation.PushAsync(new TabPage());
            }
            else
            {
                await DisplayAlert("Thông báo", "Sai tên đăng nhập hoặc mật khẩu", "Ok");
            }
        }
    }
}