using Newtonsoft.Json;
using RestApi.Client.Interfaces;
using RestApi.Client.Results;
using RestApi.Shared.Errors;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RestApi.Client.Clients
{
    public class ClientBase : IClientBase
    {
        private const string Authorization = "Authorization";
        private string _token;
        private readonly string _apiRoute;
        private string _server;

        protected HttpClient Client;

        public string ServerUrl
        {
            get { return _server; }
            set
            {
                Client.BaseAddress = new Uri(value);
                _server = value;
            }
        }
        protected string ApiUrl => string.Format("{0}/api/{1}/", ServerUrl, _apiRoute);

        protected ClientBase(string apiRoute)
        {
            _apiRoute = apiRoute;
            Client = new HttpClient(GetInsecureHandler());
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public string Token
        {
            get { return _token; }
            set
            {
                _token = value;
                if (Client.DefaultRequestHeaders.Contains(Authorization))
                    Client.DefaultRequestHeaders.Remove(Authorization);
                Client.DefaultRequestHeaders.Add(Authorization, "Bearer " + value);
            }
        }

        protected async Task<ActionResult<T>> GetAsync<T>(string url)
        {
            var response = await Client.GetAsync(url);
            return await GetResult<T>(response);
        }

        protected async Task<ActionResult<T>> DeleteAsync<T>(string url)
        {
            var response = await Client.DeleteAsync(url);
            return await GetResult<T>(response);
        }

        protected async Task<ActionResult<T>> PostAsync<T>(string url, object content)
        {
            var response = await Client.PostAsJsonAsync(url, content);
            return await GetResult<T>(response);
        }

        protected async Task<ActionResult<T>> PutAsync<T>(string url, object content)
        {
            var response = await Client.PutAsJsonAsync(url, content);
            return await GetResult<T>(response);
        }

        protected async Task<ActionResult> GetOkResult(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return new OkResult();
            return await GetResult(response);
        }

        protected async Task<ActionResult<T>> GetResult<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return new OkResultWithObject<T>(await response.Content.ReadAsAsync<T>());
            return new OkResultWithObject<T>(await GetResult(response));
        }

        protected async Task<ActionResult> GetResult(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<ActionResult>();

            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    return await GetBadRequestResult(response);
                case HttpStatusCode.NotFound:
                    return new NotFoundResult();
                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedResult();
                default:
                    return new StatusCodeResult((int)response.StatusCode);
            }
        }

        private static async Task<ActionResult> GetBadRequestResult(HttpResponseMessage response)
        {
            var jsonValue = await response.Content.ReadAsStringAsync();
            int.TryParse(jsonValue, out int badRequestReason);
            if (badRequestReason != 0)
                return new BadRequestResult((BadRequestReason)badRequestReason);
            return JsonConvert.DeserializeObject<ValidationErrorResult>(jsonValue);
        }

        private HttpClientHandler GetInsecureHandler()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                {
                    if (cert.Issuer.Equals("CN=localhost"))
                        return true;
                    return errors == System.Net.Security.SslPolicyErrors.None;
                }
            };
            return handler;
        }
    }
}
