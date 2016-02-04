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
using Bonnaroo.Collections;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Bonnaroo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Lineup : Page
    {
        public ObservableCollection<HTMLData> HTMLStrings = new ObservableCollection<HTMLData>();
        public ObservableCollection<lineupArtists> artists = new ObservableCollection<lineupArtists>();
        public Library library;
        public Lineup()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            library = new Library();
           
        }
        private void updateCollection(string html)
        {
            string htmllink = html;
            while (html.IndexOf("class=\"band \" id=") != -1)
            {
                int indexlink = htmllink.IndexOf("<a href=\"lineup.bonnaroo.com/band\"");
                int indexlink1 = htmllink.IndexOf("\"");
                string sublink = htmllink.Substring(indexlink + 1, indexlink1 - indexlink - 1);
                int index = html.IndexOf("class=\"band \" id=");
                string sub = html.Substring(index);
                index = sub.IndexOf(">");
                int index1 = sub.IndexOf("<");
                string sub1 = sub.Substring(index + 1, index1 - index - 1);
                if (sub1.Contains("&amp;"))
                {
                    sub1 = sub1.Substring(0, sub1.IndexOf("&amp;") - 1) + " & " + sub1.Substring(sub1.IndexOf("&amp;") + 1 + 4);
                }
                html = sub.Substring(index1);
                Debug.WriteLine(sub1);
                lineupArtists lart = new lineupArtists();
                lart.name = sub1;
                lart.link = "http://lineup.bonnaroo.com/band/" + sublink;
                lart.sublink = sublink;
                artists.Add(lart);
            }
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {

            string html = null;
            if(await library.checkIfFileExists("lineupLandingPage"))
            {
                html = await library.readFile("lineupLandingPage");
                string htmllink = html;
                HTMLStrings.Add(new HTMLData(html));
                HtmlSource.Source = HTMLStrings;
                Debug.WriteLine("Lineup webpage already exists. Reading and displaying html");
                updateCollection(html);
                myWebView.Visibility = Visibility.Collapsed;
                
            }
            else
            {
                Debug.WriteLine("File doesn't exist");
                myWebView.Navigate(new Uri("http://lineup.bonnaroo.com/?sort=alpha"));
            }
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

        private async void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            myWebView.Navigate(new Uri("http://lineup.bonnaroo.com/"));
            string pagecontent = await myWebView.InvokeScriptAsync("eval", new string[] { "document.documentElement.innerHTML;" });
            await library.writeFile("lineupLandingPage", pagecontent);
            updateCollection(pagecontent);
        }

        private async void myWebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
           
                Debug.WriteLine("In navigation completed");
                string pagecontent = await myWebView.InvokeScriptAsync("eval", new string[] { "document.documentElement.innerHTML;" });
                //Debug.WriteLine(pagecontent);
                await library.writeFile("lineupLandingPage", pagecontent);
                Debug.WriteLine("In lineup navigation completed : Writing html to file.");
            
        }

        private void myListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            lineupArtists art = (lineupArtists)e.ClickedItem;
            Frame rootFrame = Window.Current.Content as Frame;

        }
    }
}
