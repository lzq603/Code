using RestSharp;

namespace HisInterfaceValidate
{
    class HttpClient
    {
        public const string HTTPHOST = "http://localhost:8080";
        //POST方法
        public static IRestResponse HttpPost(string Url, string postDataStr)
        {

            var client = new RestClient(Url);
            client.Timeout = 2000;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", postDataStr, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response;
        }
        //GET方法
        public static IRestResponse HttpGet(string Url, string postDataStr)
        {
            var client = new RestClient(Url);
            client.Timeout = -2000;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return response;
        }
    }
}
