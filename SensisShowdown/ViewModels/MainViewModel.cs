using System;
using System.Collections.Generic;
using System.Linq;
using SensisShowdown.Helpers;

namespace SensisShowdown.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            FakeSearch();
        }

        public async void FakeSearch()
        {
            var searchEndPoint = "http://api.sensis.com.au/ob-20110511/test/search";
            var apiKey = "jeyrqd8syqag4xp9v7m46g47";

            var searcher = new SsapiSearcher(searchEndPoint, apiKey);

            // Perform a search and check the response
            var searchResponse = await searcher.SearchFor("asian", "melbourne");
            if (searchResponse.code < 200 || searchResponse.code > 299)
            {
                //Console.WriteLine("Search failed - Error " + searchResponse.code + ": " + searchResponse.message);
                return;
            }

            var total = searchResponse.totalResults;
            
            // Display the results
            foreach (var result in searchResponse.results)
            {
                
            }
        }





        //public void GetSearchResults(string term1, string term2)
        //{
            
        //}
    }
}
