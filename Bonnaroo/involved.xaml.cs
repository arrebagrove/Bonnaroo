﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Bonnaroo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class involved : Page
    {
        public ObservableCollection<HTMLData> HTMLStrings = new ObservableCollection<HTMLData>();
        public Library library;
        public involved()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            library = new Library();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            bool exists = await library.checkIfFileExists("involvedLandingPage");
            if (!exists)
            {
                //WebRequest request = WebRequest.Create("http://www.bonnaroo.com/get-involved");
                Debug.WriteLine("Involved page doesn't exist");
                string html = await library.makeWebRequest("http://www.bonnaroo.com/get-involved");
                await library.writeFile("involvedLandingPage", html);
                HTMLStrings.Add(new HTMLData(html));
                HtmlSource.Source = HTMLStrings;
            }
            else
            {
                string res = await library.readFile("involvedLandingPage");
                HTMLStrings.Add(new HTMLData(res));
                HtmlSource.Source = HTMLStrings;
            }

        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MainPage));
        }

        private void Lineup_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Gallery_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(gallery));
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(info));
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(about));
        }

        private void tickets_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(tickets));
        }

        private void activity_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(activity));
        }

        private void involved_Click(object sender, RoutedEventArgs e)
        {

        }

        private void news_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(news));
        }

        public class HTMLData
        {
            public HTMLData() { }
            public HTMLData(string _HTML)
            {
                HTML = _HTML;
            }

            public string HTML { get; set; }
        }

        private async void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string html = await library.makeWebRequest("http://www.bonnaroo.com/get-involved");
                await library.writeFile("involvedLandingPage", html);
                HTMLStrings.Add(new HTMLData(html));
                HtmlSource.Source = HTMLStrings;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
