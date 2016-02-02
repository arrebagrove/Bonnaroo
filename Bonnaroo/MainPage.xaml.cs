using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using System.Net;
using System.Diagnostics;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Bonnaroo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<HTMLData> HTMLStrings = new ObservableCollection<HTMLData>();
        public Library library;
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            library = new Library();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                bool exists = await library.checkIfFileExists("webLandingPage");
                if (!exists)
                {
                    //WebRequest request = WebRequest.Create("http://www.bonnaroo.com/");
                    string html = await library.makeWebRequest("http://www.bonnaroo.com/");
                    await library.writeFile("webLandingPage", html);
                    HTMLStrings.Add(new HTMLData(html));
                    HtmlSource.Source = HTMLStrings;
                }
                else
                {
                    string res = await library.readFile("webLandingPage");
                    HTMLStrings.Add(new HTMLData(res));
                    HtmlSource.Source = HTMLStrings;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in Home :" + ex);
            }

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

        
        
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Lineup_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Lineup));
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

        private async void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string html = await library.makeWebRequest("http://www.bonnaroo.com/");
                await library.writeFile("webLandingPage", html);
                HTMLStrings.Add(new HTMLData(html));
                HtmlSource.Source = HTMLStrings;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void myWebView_NewWindowRequested(WebView sender, WebViewNewWindowRequestedEventArgs args)
        {
            Debug.WriteLine("In new window requested");

            newWebView.Navigate(args.Uri);
            //contentGrid.Children.Add(newWebView);
            args.Handled = true;
        }

        private void newWebView_NewWindowRequested(WebView sender, WebViewNewWindowRequestedEventArgs args)
        {
            Debug.WriteLine("In new webview window requested.");
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            //newWebView.Navigate(new Uri("https://www.facebook.com/v2.0/dialog/oauth?scope=user_birthday%2Cemail%2Cuser_likes%2Cuser_location&response_type=none&seen_revocable_perms_nux=false&ref=LoginButton&auth_type=&default_audience&redirect_uri=https:%2F%2Fwww.facebook.com%2Fdialog%2Freturn%2Farbiter%3Frelation%3Dopener%26close%3Dtrue%23origin%3Dhttp%253A%252F%252Flineup.bonnaroo.com%252Ff31b0d1331f7368&state=fc5ac660006638&app_id=192031150893987&client_id=192031150893987&display=touch"));
            newWebView.Navigate(new Uri("http://lineup.bonnaroo.com/user/3411531/bands"));
            Debug.WriteLine("How did it go?");
        }
    }
}
