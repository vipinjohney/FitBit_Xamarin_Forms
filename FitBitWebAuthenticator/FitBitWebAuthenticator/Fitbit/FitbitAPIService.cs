using System;
using System.Threading.Tasks;
using FitBitWebAuthenticator.Models;
using Newtonsoft.Json;
using RestSharp;

namespace FitBitWebAuthenticator.FirBit
{
    public class FitbitAPIService
    {
        public async Task<FitBitResponseModel> CallTokenAPIAsync(string code)
        {
            FitBitResponseModel tokenDetails = new FitBitResponseModel();
            try
            {
                var client = new RestClient(FitbitConfiguration.TokenApiUri)
                {
                    Timeout = -1
                };

                var _authorization = "Basic " + FitbitServices.Base64String();

                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", _authorization);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("grant_type", FitbitConfiguration.GrantType);
                request.AddParameter("clientId", FitbitConfiguration.ClientId);
                request.AddParameter("redirect_uri", FitbitConfiguration.Callback);
                request.AddParameter("code", code);
                IRestResponse response = await client.ExecuteAsync(request);
                Console.WriteLine($"CallTokenAPI : {response.Content}");

                if (response.IsSuccessful)
                {
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    var _jsonResult = JsonConvert.DeserializeObject(response.Content).ToString();

                    tokenDetails = JsonConvert.DeserializeObject<FitBitResponseModel>(_jsonResult, settings);
                }
            }
            catch (Exception)
            {

            }

            return tokenDetails;
        }

        private async Task APIRequestAsync(string requestUri)
        {
            var client = new RestClient(requestUri)
            {
                Timeout = -1
            };

            var _accessToken = FitbitConfiguration.TokenResponse.access_token;

            var request = new RestRequest(Method.GET);

            var bearerToken = "Bearer " + _accessToken;
            request.AddHeader("Authorization", bearerToken);

            IRestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
        }

        public async Task FetchUserProfileAsync()
        {
            var requestUri = "https://api.fitbit.com/1/user/-/profile.json";

            APIRequestAsync(requestUri);

        }

        public async Task FetchUserActivityForDateAsync(string userId, string date)
        {
            var requestUri = "https://api.fitbit.com/1/user/" + userId + "/activities/date/" + date + ".json";

            APIRequestAsync(requestUri);
        }
    }
}
