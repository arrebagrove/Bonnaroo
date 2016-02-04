using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using Bonnaroo.Collections;
using HtmlAgilityPack;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Bonnaroo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ArtistPage : Page
    {
        public lineupArtists artist;
        private Library library;
        public ArtistPage()
        {
            this.InitializeComponent();
            artist = new lineupArtists();
            library = new Library();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            artist = (lineupArtists)e.Parameter;
            if(await library.checkIfFileExists("artist" + artist.sublink))
            {
                string html = await library.readFile("artist" + artist.sublink);
            }
            else
            {
                myWebView.Navigate(new Uri(artist.link));
            }
        }

        private void aboutButton_Click(object sender, RoutedEventArgs e)
        {
            aboutBorder.Visibility = Visibility.Visible;
            scheduleBorder.Visibility = Visibility.Collapsed;
        }

        private void scheduleButton_Click(object sender, RoutedEventArgs e)
        {
            scheduleBorder.Visibility = Visibility.Visible;
            aboutBorder.Visibility = Visibility.Collapsed;
        }

        private async void myWebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            string pagecontent = await myWebView.InvokeScriptAsync("eval", new string[] { "document.documentElement.innerHTML;" });
            //Debug.WriteLine(pagecontent);
            await library.writeFile("artist" + artist.sublink, pagecontent);
        }
    }
}
