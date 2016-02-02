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
            
           
            if(await library.checkIfFileExists("lineupLandingPage"))
            {
                string html = await library.readFile("lineupLandingPage");
                HTMLStrings.Add(new HTMLData(html));
                HtmlSource.Source = HTMLStrings;
                Debug.WriteLine("Lineup webpage already exists. Reading and displaying html");
            }
            else
            {
                Debug.WriteLine("File doesn't exist");
                myWebView.Navigate(new Uri("http://lineup.bonnaroo.com/?sort=alpha"));
            }
            /**/
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
        }

        private async void myWebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
           
                Debug.WriteLine("In navigation completed");
                string pagecontent = await myWebView.InvokeScriptAsync("eval", new string[] { "document.documentElement.innerHTML;" });
                //Debug.WriteLine(pagecontent);
                await library.writeFile("lineupLandingPage", pagecontent);
                Debug.WriteLine("In lineup navigation completed : Writing html to file.");
            
        }
    }
}
