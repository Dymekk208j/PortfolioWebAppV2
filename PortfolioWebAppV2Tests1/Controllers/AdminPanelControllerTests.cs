using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using PortfolioWebAppV2.Models.DatabaseModels;
using Xunit;
using Xunit.Abstractions;

namespace PortfolioWebAppV2.Tests.Unit.Controllers
{
    public class AdminPanelControllerTests
    {
        private readonly ITestOutputHelper output;
        public AdminPanelControllerTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact()]
        public void API_TEST()
        {

            var result = GetTelemetry
            ("metrics", "users/count", "timespan=2018-12%2FP1M");//FP1MK wyniki z miesiaca  
            result = result.Replace("users/count", "userscount");

            Visitior test = JsonConvert.DeserializeObject<Visitior>(result);
            if(test != null) output.WriteLine("t: " + test.value.userscount.unique);

            output.WriteLine(result);
            Assert.NotNull(result);

        }
        public static string GetTelemetry(string queryType, string queryPath, string parameterString)
        {
            string appid = "a34a5b6c-d76e-408f-b4a7-10adf79bbe17";
            string apikey = "45g8cauv03pjo3760kkeu35h7qx86nz7lo4r5vcw";
            string URL = "https://api.applicationinsights.io/v1/apps/{0}/{1}/{2}?{3}";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("x-api-key", apikey);
            var req = string.Format(URL, appid, queryType, queryPath, parameterString);
            HttpResponseMessage response = client.GetAsync(req).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                return response.ReasonPhrase;
            }
        }
    
    }
}