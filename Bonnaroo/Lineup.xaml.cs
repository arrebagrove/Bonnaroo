using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class Lineup : Page
    {
        public ObservableCollection<HTMLData> HTMLStrings = new ObservableCollection<HTMLData>();
        public Library library;
        public Lineup()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            library = new Library();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            /*bool exists = await library.checkIfFileExists("lineupLandingPage");
            if (!exists)
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://lineup.bonnaroo.com/");
                myRequest.Method = "GET";
                myRequest.Headers["User-Agent"] = "Mozilla / 5.0(Linux; Android 4.0.4; Galaxy Nexus Build / IMM76B) AppleWebKit / 535.19(KHTML, like Gecko) Chrome / 18.0.1025.133 Mobile Safari/ 535.19";
                WebResponse myResponse = await myRequest.GetResponseAsync();
                StreamReader sr = new StreamReader(myResponse.GetResponseStream());
                string html = sr.ReadToEnd();
                HTMLStrings.Add(new HTMLData(html));
                HtmlSource.Source = HTMLStrings;
            }
            else
            {
                string res = await library.readFile("lineupLandingPage");
                HTMLStrings.Add(new HTMLData(res));
                HtmlSource.Source = HTMLStrings;
            }*/
            //string res = await library.readFile("");
            string path = @"Files\lineupLandingPage";
            StorageFolder InstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            StorageFile file = await InstalledFolder.GetFileAsync(path);

        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MainPage));
        }

        private void Lineup_Click(object sender, RoutedEventArgs e)
        {
            // do nothing.
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
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(involved));
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
    }
}
