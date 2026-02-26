using DTO;
using Newtonsoft.Json;
using RestSharp;
using Flurl.Http;

namespace AppLogic
{
    public interface IRHConnector
    {
        Task<List<Employee>> RetrieveAllEmployees();
        Task<List<Employee>> RetrieveAllEmployeesRest();
        Task<List<Employee>> RetrieveAllEmployeesFlurl();
        Task<List<string>> RetrieveAllSpecialties();
    }

    public class RHConnector : IRHConnector
    {
        private static HttpClient _httpClient;
        private const string _baseUrl = "https://rh-central.azurewebsites.net/";

        public RHConnector()
        {
            if (_httpClient is null)
            {
                _httpClient = new HttpClient()
                {
                    BaseAddress = new Uri(_baseUrl),
                    Timeout = TimeSpan.FromSeconds(15)
                };
            }
        }

        public async Task<List<Employee>> RetrieveAllEmployees()
        {
            string serviceUrl = "/api/RH/GetAllEmployees";
            string result = await InvokeGetAsync(serviceUrl);

            if (string.IsNullOrEmpty(result))
            {
                return new List<Employee>();
            }

            var dtoEmployees = JsonConvert.DeserializeObject<List<Employee>>(result);
            if (dtoEmployees is null)
            {
                return new List<Employee>();
            }

            return dtoEmployees;
        }

        public async Task<List<Employee>> RetrieveAllEmployeesRest()
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("/api/RH/GetAllEmployees", Method.Get);

            var response = await client.ExecuteAsync(request);

            if (response is null)
            {
                return new List<Employee>();
            }

            if (!response.IsSuccessful)
            {
                return new List<Employee>();
            }

            if (string.IsNullOrEmpty(response.Content))
            {
                return new List<Employee>();
            }

            var dtoEmployees = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            if (dtoEmployees is null)
            {
                return new List<Employee>();
            }

            return dtoEmployees;
        }

        public async Task<List<Employee>> RetrieveAllEmployeesFlurl()
        {
            try
            {
                string url = _baseUrl + "api/RH/GetAllEmployees";
                string result = await url.GetStringAsync();

                if (string.IsNullOrEmpty(result))
                {
                    return new List<Employee>();
                }

                var dtoEmployees = JsonConvert.DeserializeObject<List<Employee>>(result);
                if (dtoEmployees is null)
                {
                    return new List<Employee>();
                }

                return dtoEmployees;
            }
            catch
            {
                return new List<Employee>();
            }
        }

        public async Task<List<string>> RetrieveAllSpecialties()
        {
            string serviceUrl = "/api/RH/GetSpecialties";
            string result = await InvokeGetAsync(serviceUrl);
            var dtoEmployees = JsonConvert.DeserializeObject<List<string>>(result);

            return dtoEmployees;


        }

        #region Metodos Helpers

        private async Task<String> InvokeGetAsync(string uri)
        {
            try
            {
                string responseString = string.Empty;
                var results = await _httpClient.GetAsync(uri);
                if (results.IsSuccessStatusCode)
                {
                    responseString = await results.Content.ReadAsStringAsync();
                }
                return responseString;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task<String> InvokePutAsync(string uri, StringContent content)
        {
            try
            {
                string responseString = string.Empty;
                var results = await _httpClient.PutAsync(uri, content);
                if (results.IsSuccessStatusCode)
                {
                    responseString = await results.Content.ReadAsStringAsync();
                }
                return responseString;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task<String> InvokePostAsync(string uri, StringContent content)
        {
            try
            {
                string responseString = string.Empty;
                var results = await _httpClient.PostAsync(uri, content);
                if (results.IsSuccessStatusCode)
                {
                    responseString = await results.Content.ReadAsStringAsync();
                }
                return responseString;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
        #endregion