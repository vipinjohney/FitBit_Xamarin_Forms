using System;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Linq;
using FitBitWebAuthenticator.FirBit;

namespace FitBitWebAuthenticator
{
    public class WebAuthenticatorHandler
    {
        public async Task<string> FetchFitbitCode()
        {
            var code = "";
            try
            {
                var callbackUrl = new Uri(FitbitConfiguration.Callback);
                var loginUrl = new Uri(FitbitServices.BuildAuthenticationUrl());
                var authenticationResult = await WebAuthenticator.AuthenticateAsync(loginUrl, callbackUrl);

                code = authenticationResult.Properties["code"];
                code = code.Replace("#_=_", "");

            }
            catch (TaskCanceledException)
            {

            }
            catch (Exception)
            {

            }
            Console.WriteLine($"FetchFitbitCode : {code}");
            return code;
        } 
    }

    
}
