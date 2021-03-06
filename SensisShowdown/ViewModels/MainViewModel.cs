﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SensisShowdown.Annotations;
using SensisShowdown.Helpers;
using SensisShowdown.Models;

namespace SensisShowdown.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ResultTotal> ResultTotalCollection { get; set; }

        public ObservableCollection<SearchResultData> Results1 { get; set; }
        public ObservableCollection<SearchResultData> Results2 { get; set; }

        private int _results1Total;
        public int Results1Total
        {
            get { return _results1Total; }
            set
            {
                if (value == _results1Total) return;
                _results1Total = value;
                OnPropertyChanged();
            }
        }

        private int _results2Total;
        public int Results2Total
        {
            get { return _results2Total; }
            set
            {
                if (value == _results2Total) return;
                _results2Total = value;
                OnPropertyChanged();
            }
        }

        private string _searchTerm1;
        public string SearchTerm1
        {
            get { return _searchTerm1; }
            set
            {
                if (value == _searchTerm1) return;
                _searchTerm1 = value;
                OnPropertyChanged();
            }
        }

        private string _searchTerm2;

        public string SearchTerm2
        {
            get { return _searchTerm2; }
            set
            {
                if (value == _searchTerm2) return;
                _searchTerm2 = value;
                OnPropertyChanged();
            }
        }

        private bool _doneAtLeast1Search;
        public bool DoneAtLeast1Search
        {
            get { return _doneAtLeast1Search; }
            set
            {
                if (value.Equals(_doneAtLeast1Search)) return;
                _doneAtLeast1Search = value;
                OnPropertyChanged();
            }
        }

        public string Location { get; set; }

        public MainViewModel()
        {
            Results1 = new ObservableCollection<SearchResultData>();
            Results2 = new ObservableCollection<SearchResultData>();
            ResultTotalCollection = new ObservableCollection<ResultTotal>();

            ResultTotalCollection.Add(new ResultTotal { Name = "Term 1", Total = 4 });
            ResultTotalCollection.Add(new ResultTotal { Name = "Term 2", Total = 7 });

            Location = "melbourne";
            SearchTerm1 = "pizza";
            SearchTerm2 = "pasta";

            //GetSampleData();
        }

        public void ShowDown()
        {
            GetShowdownResults(SearchTerm1, SearchTerm2, Location);
        }

        public async void GetShowdownResults(string term1, string term2, string location)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(term1) || string.IsNullOrWhiteSpace(term2) || string.IsNullOrWhiteSpace(location))
                    return;

                var results1 = await DoSearch(term1, location);
                var results2 = await DoSearch(term2, location);

                Results1.Clear();
                foreach (var listing in results1.results)
                {
                    if (listing.primaryAddress != null)
                        Results1.Add(new SearchResultData { IsResult1 = true, Latitude = listing.primaryAddress.latitude, Longitude = listing.primaryAddress.longitude, LocationName = listing.name });
                }
                Results1Total = results1.totalResults;

                Results2.Clear();
                foreach (var listing in results2.results)
                {
                    if (listing.primaryAddress != null)
                        Results2.Add(new SearchResultData { IsResult1 = false, Latitude = listing.primaryAddress.latitude, Longitude = listing.primaryAddress.longitude, LocationName = listing.name });
                }
                Results2Total = results2.totalResults;

                ResultTotalCollection.Clear();
                ResultTotalCollection.Add(new ResultTotal { Name = "Term 1", Total = Results1Total });
                ResultTotalCollection.Add(new ResultTotal { Name = "Term 2", Total = Results2Total });

                DoneAtLeast1Search = true;
            }
            catch (WebException)
            {
                var dlg = new Windows.UI.Popups.MessageDialog("There was a network error");
                dlg.ShowAsync();
            }
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

            return searchResponse;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ResultTotal
    {
        public string Name { get; set; }
        public int Total { get; set; }
    }
}
