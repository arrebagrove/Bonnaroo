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
using System.Diagnostics;
using Windows.UI.Xaml.Media.Imaging;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Bonnaroo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ArtistPage : Page
    {
        public lineupArt artist;
        private Library library;
        public ArtistPage()
        {
            this.InitializeComponent();
            //this.NavigationCacheMode = NavigationCacheMode.Required;

            artist = new lineupArt();
            library = new Library();
            this.Loaded += ArtistPage_Loaded;
        }

        private async void ArtistPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (await library.checkIfFileExists("artist" + artist.sublink))
            {
                string html = await library.readFile("artist" + artist.sublink);
                refreshUI(html);
            }
            else
            {
                try
                {
                    myWebView.Navigate(new Uri(artist.link));
                    Debug.WriteLine(artist.link);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            artist = (lineupArt)e.Parameter;
            //await library.makeWebRequest(artist.link);
            
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
            myWebView.Visibility = Visibility.Collapsed;
            //Debug.WriteLine(pagecontent);
            refreshUI(pagecontent);
            await library.writeFile("artist" + artist.sublink, pagecontent);
        }

        private async void refreshUI(string html)
        {
            if(await library.checkIfFileExists(artist.sublink + "profilepic"))
            {
                BitmapImage bi3 = new BitmapImage();
                bi3.UriSource = new Uri("ms-appdata:///local/" + artist.sublink + "profilepic", UriKind.RelativeOrAbsolute);
                artistImage.Source = bi3;
                artistName.Text = artist.name;
            }
            else
            {
                int index1 = html.IndexOf("<img alt=\"Custom\" src=\"");
                string sub = html.Substring(index1 + ("<img alt=\"Custom\" src=\"").Length);
                int index2 = sub.IndexOf("\"");
                string photolink = sub.Substring(0, index2);
                await library.downloadImage(photolink, artist.sublink + "profilepic");
                Debug.WriteLine(photolink);
                BitmapImage bi3 = new BitmapImage();
                bi3.UriSource = new Uri("ms-appdata:///local/" + artist.sublink + "profilepic", UriKind.RelativeOrAbsolute);
                artistImage.Source = bi3;
                artistName.Text = artist.name;
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if(rootFrame.CanGoBack)
            {
                Debug.WriteLine("Can go back");
                rootFrame.GoBack();
            }
            else
            {
                Debug.WriteLine("Can't go back");
            }
        }
    }
}
