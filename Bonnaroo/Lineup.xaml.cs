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
        public ObservableCollection<lineupArt> artists = new ObservableCollection<lineupArt>();
        public Library library;
        public Lineup()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            library = new Library();
           
        }
        private async void updateCollection(string html, bool fromFreshDownloadFlag)
        {
            string htmllink = html;
            //Debug.WriteLine(htmllink);
            Debug.WriteLine("in UpdateCollection");
            artists.Clear();
            RootObjectArtist rartist = new RootObjectArtist();
            rartist.art = new List<lineupArt>();
            bool artistsFileExists = await library.checkIfFileExists("artistsFile");
            if (fromFreshDownloadFlag == true || artistsFileExists == false)
            {
                while (html.IndexOf("class=\"band \" id=") != -1)
                {
                    //Debug.WriteLine("In While loop");
                    htmllink = html;
                    int indexlink = htmllink.IndexOf("class=\"band \" id");
                    string subtemp = htmllink.Substring(indexlink);
                    indexlink = subtemp.IndexOf("href=\"");
                    string sublink = subtemp.Substring(indexlink + 38);
                    sublink = sublink.Substring(0, sublink.IndexOf("\""));

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
                    lineupArt lart = new lineupArt();
                    lart.name = sub1;
                    lart.link = "http://lineup.bonnaroo.com/band/" + sublink;
                    lart.sublink = sublink;
                    if (await library.checkIfFileExists(sublink + "profilepic"))
                    {
                        lart.artistPhotoPath = "ms-appdata:///local/" + sublink + "profilepic";
                    }
                    else
                    {
                        lart.artistPhotoPath = "/assets/member_photo.jpg";
                    }
                    artists.Add(lart);
                    rartist.art.Add(lart);
                }
                string res = lineupArtists.artistDataSerializer(rartist);
                Debug.WriteLine(res);
                await library.writeFile("artistsFile", res);
            }
            else          // if the artistsFile exists and we don't need to fo a fresh refresh, read from the file, deserialize and add to the observable collection
            {
                Debug.WriteLine("in else on lineup page");
                string res = await library.readFile("artistsFile");
                rartist = lineupArtists.artistDataDeserializer(res);
                var rartinfo = rartist.art;
                foreach (var rart in rartinfo)
                {
                    artists.Add(rart);
                }
            }
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {

            string html = null;
            if(await library.checkIfFileExists("lineupLandingPage"))
            {
                Debug.WriteLine("Lineup landing page file does exist");
                myWebView.Visibility = Visibility.Collapsed;
                html = await library.readFile("lineupLandingPage");
                //string htmllink = html;
                //HTMLStrings.Add(new HTMLData(html));
                //HtmlSource.Source = HTMLStrings;
                //Debug.WriteLine("Lineup webpage already exists. Reading and displaying html");
                updateCollection(html, false);
                //myWebView.Visibility = Visibility.Collapsed;
                
            }
            else
            {
                Debug.WriteLine("File doesn't exist : lineuplandingpage");
                myWebView.Visibility = Visibility.Visible;
                myWebView.Navigate(new Uri("http://lineup.bonnaroo.com/"));
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

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Refresh button in lineup.xaml.cs clicked");
            myWebView.Visibility = Visibility.Visible;
            myWebView.Navigate(new Uri("http://lineup.bonnaroo.com/"));
            //string pagecontent = await myWebView.InvokeScriptAsync("eval", new string[] { "document.documentElement.innerHTML;" });
            //myWebView.Visibility = Visibility.Collapsed;
            //await library.writeFile("lineupLandingPage", pagecontent);
            //updateCollection(pagecontent, true);
        }

        private async void myWebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            try
            {
                myWebView.Visibility = Visibility.Visible;
                Debug.WriteLine("In Lineup.xaml.cs webview navigation completed");
                string pagecontent = await myWebView.InvokeScriptAsync("eval", new string[] { "document.documentElement.innerHTML;" });
                //Debug.WriteLine(pagecontent);
                await library.writeFile("lineupLandingPage", pagecontent);
                Debug.WriteLine("In lineup navigation completed : Writing html to file.");
                myWebView.Visibility = Visibility.Collapsed;
                
                updateCollection(pagecontent, true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            
        }

        private void myListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            myWebView.Visibility = Visibility.Collapsed;
            lineupArt art = (lineupArt)e.ClickedItem;
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(ArtistPage), art);
        }
    }
}
