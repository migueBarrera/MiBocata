using System.IO;
using System.Threading.Tasks;

namespace Mibocata.Core.Services.Interfaces
{
    public interface IImagesService
    {
        Task<string> UploadImage(Stream simageStream);
    }
}
