using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class news : Page
    {
        public ObservableCollection<HTMLData> HTMLStrings = new ObservableCollection<HTMLData>();
        public Library library;
        public news()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            library = new Library();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            bool exists = await library.checkIfFileExists("lineupLandingPage");
            if (!exists)
            {
                WebRequest request = WebRequest.Create("http://www.bonnaroo.com/");
                WebResponse response = await request.GetResponseAsync();
                Stream data = response.GetResponseStream();
                string html = String.Empty;
                using (StreamReader sr = new StreamReader(data))
                {
                    html = sr.ReadToEnd();
                }
                await library.writeFile("lineupLandingPage", html);
            }
            string res = await library.readFile("lineupLandingPage");
            HTMLStrings.Add(new HTMLData(res));
            HtmlSource.Source = HTMLStrings;

        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Lineup_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Gallery_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {

        }

        private void About_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tickets_Click(object sender, RoutedEventArgs e)
        {

        }

        private void activity_Click(object sender, RoutedEventArgs e)
        {

        }

        private void involved_Click(object sender, RoutedEventArgs e)
        {

        }

        private void news_Click(object sender, RoutedEventArgs e)
        {

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
