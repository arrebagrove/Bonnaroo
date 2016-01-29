using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

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
