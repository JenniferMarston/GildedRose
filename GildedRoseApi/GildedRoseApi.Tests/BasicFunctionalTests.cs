using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using GildedRoseApi.Dto;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace GildedRoseApi.Tests.BasicTests
{
    public class BasicFunctionalTests : IClassFixture<WebApplicationFactory<GildedRoseApi.Startup>>
    {
        private readonly WebApplicationFactory<GildedRoseApi.Startup> _webApplicationFactory;

        public BasicFunctionalTests(WebApplicationFactory<GildedRoseApi.Startup> factory)
        {
            _webApplicationFactory = factory;
        }


        [Theory]
        [InlineData("api/items")]
        [InlineData("api/Items/1")]
        [InlineData("api/Items/Purchase")]
        [InlineData("api/Authentication/logins")]

        public async Task GetEndPointsReturnUnauthorized(string url)
        {
            var client = _webApplicationFactory.CreateClient();

            var response = await client.GetAsync(url);

            Assert.Equal( "Unauthorized", response.ReasonPhrase);

        }

        [Theory]
        [InlineData("api/Authentication/logins")]
        public async Task PostCreateLoginReturnAuthorized(string url)
        {
            var client = _webApplicationFactory.CreateClient();

            var authInfo = JsonConvert.SerializeObject(new { username = "jenna", password = "Jenna123!" });
            var content = new StringContent(authInfo, UnicodeEncoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            Assert.Equal("OK", response.ReasonPhrase);

        }


        [Theory]
        [InlineData("api/Authentication/logins", "api/items/purchase/1")]
        public async Task MakePurchaseSuccess(string url, string url2)
        {
            var client = _webApplicationFactory.CreateClient();
            BearerToken bearerToken = await SetUpAndGetBearerToken(client);


            var purchaseInfo = JsonConvert.SerializeObject(new {itemId = 1, quantityWanted = 3});
            var purchaseContent = new StringContent(purchaseInfo, UnicodeEncoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken.Token);

            HttpResponseMessage purchaseHttpResponse = await client.PutAsync(url2, purchaseContent);

            var stringPurchaseResponse = await purchaseHttpResponse.Content.ReadAsStringAsync();    

            var responseContent = JsonConvert.DeserializeObject<PurchaseResponse>(stringPurchaseResponse);

            Assert.Equal("OK", purchaseHttpResponse.ReasonPhrase);
            Assert.Equal("Successful Purchase", responseContent.Message);

        }


        [Theory]
        [InlineData("api/Authentication/logins", "api/items/purchase/1")]
        public async Task MakePurchaseFailureInsufficientInventory(string url, string url2)
        {
            var client = _webApplicationFactory.CreateClient();
            BearerToken bearerToken = await SetUpAndGetBearerToken(client);


            var purchaseInfo = JsonConvert.SerializeObject(new { itemId = 1, quantityWanted = 30 });
            var purchaseContent = new StringContent(purchaseInfo, UnicodeEncoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken.Token);

            HttpResponseMessage purchaseHttpResponse = await client.PutAsync(url2, purchaseContent);

            var stringPurchaseResponse = await purchaseHttpResponse.Content.ReadAsStringAsync();

            var responseContent = JsonConvert.DeserializeObject<PurchaseResponse>(stringPurchaseResponse);

            Assert.Equal("OK", purchaseHttpResponse.ReasonPhrase);
            Assert.Equal("Insufficient Inventory", responseContent.Message);

        }



        private async Task<BearerToken> SetUpAndGetBearerToken(HttpClient client )
        {
            var url = "api/Authentication/logins";
            var authInfo = JsonConvert.SerializeObject(new { username = "jenna", password = "Jenna123!" });
            var content = new StringContent(authInfo, UnicodeEncoding.UTF8, "application/json");
            var bearerToken = JsonConvert.DeserializeObject<BearerToken>(await client.PostAsync(url, content).Result.Content.ReadAsStringAsync());
            return bearerToken;
        }


        private class BearerToken
        {
            public string Token { get;  set; }
            //...
        }
    

    }
}


