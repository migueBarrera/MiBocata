namespace Mibocata.Core.Services.Interfaces;

public interface IImagesService
{
    Task<string> UploadImage(Stream simageStream);
}
