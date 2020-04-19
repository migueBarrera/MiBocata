using System.IO;
using System.Threading.Tasks;

namespace MiBocata.Businnes.Services.ImagesService
{
    public interface IImagesService
    {
        Task<string> UploadImage(Stream simageStream);
    }
}
