namespace MiBocataAPI.Helpers;

public static class ImageHelper
{
    public static async Task<string> UploadImage(string container, IFormFile file)
    {
        var url = string.Empty;

        //try
        //{
        //    CloudBlockBlob resultBlob = null;
        //    if (file.Length > 0)
        //    {
        //        using (var stream = file.OpenReadStream())
        //        {
        //            resultBlob = await AzureStorageManager.AzureStorageManager.SaveBlockBlobAsync($"mibocatacontainer/{container}", stream);
        //        }

        //        if (resultBlob != null)
        //        {
        //            url = resultBlob.Uri.OriginalString;
        //        }
        //    }
        //}
        //catch (Exception)
        //{
        //}

        return url;
    }
}
