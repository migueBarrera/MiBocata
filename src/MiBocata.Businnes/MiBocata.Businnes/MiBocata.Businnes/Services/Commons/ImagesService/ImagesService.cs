using System.IO;
using System.Threading.Tasks;

namespace MiBocata.Businnes.Services.ImagesService
{
    public class ImagesService : IImagesService
    {
        ////private string QUERY = "?sp=r&st=2019-10-16T13:43:39Z&se=2022-10-17T13:43:00Z&sv=2018-03-28&sig=ikaUwkDvQESTraiPlveNKMKtBlOt%2FkS%2F65aLTiFBwhc%3D&sr=c";

        public Task<string> UploadImage(Stream simageStream)
        {
            try
            {
                ////var bolb = await AzureStorageManager.AzureStorageManager.SaveBlockBlob(CONTAINER_NAME, simageStream);
                ////var simpleUrl = bolb.Uri.OriginalString;

                return Task.FromResult($"{string.Empty}{string.Empty}");
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
    }
}