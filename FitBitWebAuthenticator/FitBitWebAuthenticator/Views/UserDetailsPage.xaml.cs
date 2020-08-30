using System;
using FitBitWebAuthenticator.FirBit;
using Xamarin.Forms;

namespace FitBitWebAuthenticator.Views
{
    public partial class UserDetailsPage : ContentPage
    {
        public UserDetailsPage()
        {
            InitializeComponent();
            this.Title = "User Details";

            FetchUserActivity();
            FetchUserProfile();
        }

        void Logout_Button_Clicked(Object sender, EventArgs e)
        {
            //TODO: Pending to implement this
            // Basically what you will need to revoke tokens
        }

        void FetchUserProfile()
        {
            var _tokenResponse = FitbitConfiguration.TokenResponse;
            if (!string.IsNullOrEmpty(_tokenResponse.access_token) && !string.IsNullOrEmpty(_tokenResponse.user_id))
                new FitbitAPIService().FetchUserProfileAsync();
        }

        void FetchUserActivity()
        {
            var _tokenResponse = FitbitConfiguration.TokenResponse;
            var todayDate = DateTime.Now.ToString("yyyy-MM-dd");
            if (!string.IsNullOrEmpty(_tokenResponse.access_token) && !string.IsNullOrEmpty(_tokenResponse.user_id))
                new FitbitAPIService().FetchUserActivityForDateAsync(_tokenResponse.user_id, todayDate);
        }
    }
}
