using System;
using System.Threading.Tasks;
using FitBitWebAuthenticator.FirBit;
using FitBitWebAuthenticator.Views;
using Xamarin.Forms;

namespace FitBitWebAuthenticator
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.Title = "Login";
        }

        void Login_Button_Clicked(Object sender, EventArgs e)
        {
            StartFitbitAthenticationAsync();
        }

        async Task StartFitbitAthenticationAsync()
        {
            var authenticator = new WebAuthenticatorHandler();

            var _fitbitCode = await authenticator.FetchFitbitCode();

            if (!string.IsNullOrEmpty(_fitbitCode))
            {
                var _tokenResponse = await new FitbitAPIService().CallTokenAPIAsync(_fitbitCode);
                if (!string.IsNullOrEmpty(_tokenResponse.access_token))
                {
                    Console.WriteLine($"Fitbit access token : {_tokenResponse.access_token}");

                    FitbitConfiguration.TokenResponse = _tokenResponse;

                    this.Navigation.PushAsync(new UserDetailsPage());
                }
                else
                    Console.WriteLine("Error fetching token!!");
            }
            else
                Console.WriteLine("Login failed !!");

        }

    }
}
