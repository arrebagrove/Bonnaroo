using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Bonnaroo
{
    public class Library
    {
        public async Task<bool> writeFile(string filename, string response)
        {
            var applicationData = Windows.Storage.ApplicationData.Current;
            var localFolder = applicationData.LocalFolder;
            Debug.WriteLine("In writeFile : " + filename);
            try
            {
                //Debug.WriteLine("In try of write.");
                StorageFile sampleFile = await localFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(sampleFile, response);
            }
            catch (System.UnauthorizedAccessException e)
            {
                Debug.WriteLine("in write to file : " + filename + " : " + e);
                return false;
            }
            return true;
        }

        public async Task<bool> downloadImage(string url, string filename)
        {
            Uri uri = new Uri(url);
            //string fname = member_id + ".png";
            var bitmapImage = new BitmapImage();
            var httpClient = new HttpClient();
            var httpResponse = await httpClient.GetAsync(uri);
            byte[] b = await httpResponse.Content.ReadAsByteArrayAsync();
            try
            {
                // create a new in memory stream and datawriter
                using (var stream = new InMemoryRandomAccessStream())
                {
                    using (DataWriter dw = new DataWriter(stream))
                    {
                        // write the raw bytes and store
                        dw.WriteBytes(b);
                        await dw.StoreAsync();

                        // set the image source
                        stream.Seek(0);
                        bitmapImage.SetSource(stream);

                        var storageFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                            filename,
                            CreationCollisionOption.OpenIfExists);

                        using (var storageStream = await storageFile.OpenAsync(FileAccessMode.ReadWrite))
                        {
                            await RandomAccessStream.CopyAndCloseAsync(stream.GetInputStreamAt(0), storageStream.GetOutputStreamAt(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception in downloading image : " + ex);
                return false;
            }

            return true;
        }


        public async Task<string> makeWebRequest(string uri)
        {
            WebRequest request = WebRequest.Create(uri);
            WebResponse response = await request.GetResponseAsync();
            Stream data = response.GetResponseStream();
            string html = String.Empty;
            using (StreamReader sr = new StreamReader(data))
            {
                html = sr.ReadToEnd();
            }
            return html;
        }
        public async Task<string> readFile(string filename)
        {
            var applicationData = Windows.Storage.ApplicationData.Current;
            var localFolder = applicationData.LocalFolder;
            string response = null;
            try
            {
                // Debug.WriteLine("in try of read");
                StorageFile sampleFile = await localFolder.GetFileAsync(filename);
                response = await FileIO.ReadTextAsync(sampleFile);
            }
            catch (System.UnauthorizedAccessException e)
            {
                Debug.WriteLine("In read file : " + e);
            }
            return response;
        }

        public async Task<bool> checkIfFileExists(string filename)
        {
            var item = await ApplicationData.Current.LocalFolder.TryGetItemAsync(filename);
            if (item == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


    }
}
