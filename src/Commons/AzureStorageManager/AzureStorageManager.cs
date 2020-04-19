using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureStorageManager
{
    public class AzureStorageManager
    {
        private static readonly CloudStorageAccount cloudStorageAccount = CloudStorageAccount.
                                                                        Parse("__YOUR_AZURE_STORAGE_URL__");

        private static readonly CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();

        public static async Task<List<T>> GetBlobsAsync<T>(
            string containerName,
            string prefix = "",
            int? maxresultsPerQuery = null,
            BlobListingDetails blobListingDetails = BlobListingDetails.None)
            where T : ICloudBlob
        {
            var blobContainer = blobClient.GetContainerReference(containerName);

            var blobList = new List<T>();
            BlobContinuationToken continuationToken = null;

            try
            {
                do
                {
                    var response = await blobContainer.ListBlobsSegmentedAsync(prefix, true, blobListingDetails, maxresultsPerQuery, continuationToken, null, null);

                    continuationToken = response?.ContinuationToken;

                    foreach (var blob in response?.Results?.OfType<T>())
                    {
                        blobList.Add(blob);
                    }
                }
                while (continuationToken != null);
            }
            catch (Exception e)
            {
                throw e;
            }

            return blobList;
        }

        public static async Task<CloudBlockBlob> SaveBlockBlobAsync(
            string containerName,
            Stream stream)
        {
            var blobContainer = blobClient.GetContainerReference(containerName);

            string uniqueName = Guid.NewGuid().ToString();
            var blockBlob = blobContainer.GetBlockBlobReference($"{uniqueName}.jpg");
            await blockBlob.UploadFromStreamAsync(stream);

            return blockBlob;
        }
    }
}
