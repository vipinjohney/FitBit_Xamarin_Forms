using System;
using System.Text;

namespace FitBitWebAuthenticator.FirBit
{
    public class FitbitServices
    {
        public static string BuildAuthenticationUrl()
        {
            return $"{FitbitConfiguration.Auth2Url}" +
                $"response_type=code" +
                $"&client_id={FitbitConfiguration.ClientId}" +
                $"&redirect_uri={FitbitConfiguration.CallbackExcaped}" +
                $"&scope={FitbitConfiguration.ScopesEscaped}" +
                $"&expires_in={FitbitConfiguration.ExpireIn}";
        }

        public static string Base64String()
        {
            var authString = FitbitConfiguration.ClientId + ":" + FitbitConfiguration.ClientSecret;
            var bytes = Encoding.UTF8.GetBytes(authString);
            var base64String = Convert.ToBase64String(bytes);

            return base64String;
        }
    }
}
