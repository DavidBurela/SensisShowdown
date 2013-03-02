using System;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace SensisShowdown.Helpers
{
    class SsapiSearcher
    {
        readonly Uri _endPoint;
        readonly string _apiKey;

        public SsapiSearcher(string endPoint, string apiKey)
        {
            _endPoint = new Uri(endPoint);
            _apiKey = apiKey;
        }

        public async Task<SearchResponse> SearchFor(string query, string location)
        {
            // Build the API request
            var url = new Uri(_endPoint, "?query=" + Uri.EscapeDataString(query) + "&location=" + Uri.EscapeDataString(location) + "&key=" + Uri.EscapeDataString(_apiKey));
            var req = WebRequest.Create(url);

            // Send the request and read the response
            using (var res = await req.GetResponseAsync())
            {
                var serializer = new DataContractJsonSerializer(typeof(SearchResponse));
                var resultStream = res.GetResponseStream();
                var result = serializer.ReadObject(resultStream);
                return result as SearchResponse;
            }
        }
    }


    public class SearchResponse
    {
        public int code;
        public string message;
        public int totalResults;
        public Listing[] results;
    }


    public class Listing
    {
        public string name;
        public Address primaryAddress;
    }


    public class Address
    {
        public string addressLine;
        public string suburb;
        public string state;
        public string postcode;
        public double latitude;
        public double longitude;
    }
}
