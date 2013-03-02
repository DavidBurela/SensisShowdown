using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using SensisShowdown.Helpers;
using SensisShowdown.Models;

namespace SensisShowdown.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<SearchResultData> Results1 { get; set; }
        public ObservableCollection<SearchResultData> Results2 { get; set; }
        public int Results1Total { get; set; }
        public int Results2Total { get; set; }

        public MainViewModel()
        {
            Results1 = new ObservableCollection<SearchResultData>();
            Results2 = new ObservableCollection<SearchResultData>();

            GetSampleData();
        }

        private async void GetSampleData()
        {
            var results = await DoSearch("asian", "melbourne");

            var results1 = results.results.Take(5);
            var results2 = results.results.Skip(5).Take(7);

            Results1.Clear();
            foreach (var listing in results1)
            {
                Results1.Add(new SearchResultData{IsResult1 = true, Latitude = listing.primaryAddress.latitude, Longitude = listing.primaryAddress.longitude, LocationName = listing.name});
            }
            Results1Total = 5;

            Results2.Clear();
            foreach (var listing in results2)
            {
                Results2.Add(new SearchResultData { IsResult1 = false, Latitude = listing.primaryAddress.latitude, Longitude = listing.primaryAddress.longitude, LocationName = listing.name });
            }
            Results2Total = 7;
        }

        public async Task<SearchResponse> DoSearch(string searchTerm, string location)
        {
            var searchEndPoint = "http://api.sensis.com.au/ob-20110511/test/search";
            var apiKey = "jeyrqd8syqag4xp9v7m46g47";

            var searcher = new SsapiSearcher(searchEndPoint, apiKey);

            // Perform a search and check the response
            var searchResponse = await searcher.SearchFor(searchTerm, location);
            if (searchResponse.code < 200 || searchResponse.code > 299)
            {
                //Console.WriteLine("Search failed - Error " + searchResponse.code + ": " + searchResponse.message);
                return null;
            }

            var total = searchResponse.totalResults;


            // Display the results
            foreach (var result in searchResponse.results)
            {
                
            }

            return searchResponse;

        }


        public async void GetSearchResults(string term1, string term2, string location)
        {
            var results1 = await DoSearch(term1, location);
            var results2 = await DoSearch(term2, location);

            Results1.Clear();
            foreach (var listing in results1.results)
            {
                Results1.Add(new SearchResultData { IsResult1 = true, Latitude = listing.primaryAddress.latitude, Longitude = listing.primaryAddress.longitude, LocationName = listing.name });
            }
            Results1Total = results1.totalResults;

            Results2.Clear();
            foreach (var listing in results2.results)
            {
                Results2.Add(new SearchResultData { IsResult1 = false, Latitude = listing.primaryAddress.latitude, Longitude = listing.primaryAddress.longitude, LocationName = listing.name });
            }
            Results2Total = results2.totalResults;

        }
    }
}
