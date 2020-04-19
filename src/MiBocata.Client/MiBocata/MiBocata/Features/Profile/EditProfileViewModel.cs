using MiBocata.Framework;
using MiBocata.Services.API.Interfaces;
using Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MiBocata.Features.Profile
{
    public class EditProfileViewModel : BaseViewModel
    {
        private MediaFile mediaFile;
        private Client client;
        private ImageSource productimage;
        private readonly IClientApi clientApi;

        public EditProfileViewModel()
        {
            clientApi = RefitService.InitRefitInstance<IClientApi>(isAutenticated: true);
        }

        public ImageSource Productimage
        {
            get => productimage; set => SetAndRaisePropertyChanged(ref productimage, value);
        }

        public Client Client { get => client; set => SetAndRaisePropertyChanged(ref client, value); }

        public ICommand TakeImageCommand => new AsyncCommand(_ => TakeImageCommandAsync());

        public ICommand SaveCommand => new AsyncCommand(SaveCommandExecute);

        private async Task TakeImageCommandAsync()
        {
            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Small,
            });

            if (file == null)
            {
                return;
            }

            mediaFile = file;
            Productimage = ImageSource.FromStream(() => file.GetStream());
        }

        public override Task InitializeAsync(object navigationData)
        {
            Client = PreferencesService.GetUser();
            mediaFile = null;
            Productimage = ImageSource.FromUri(new Uri(Client.Image));

            return base.InitializeAsync(navigationData);
        }

        private async Task SaveCommandExecute(object arg)
        {
            var result = await TaskHelperFactory.
                                    CreateInternetAccessViewModelInstance(LoggingService, this).
                                    TryExecuteAsync(
                                    () => clientApi.UploadClient(Client.Id, Client));

            if (result)
            {
                if (mediaFile != null)
                {
                    var urlImage = await UploadImage(Client.Id, mediaFile);
                    if (!string.IsNullOrWhiteSpace(urlImage))
                    {
                        Client.Image = urlImage;
                    }
                }

                PreferencesService.SetUser(Client);

                await DialogService.ShowAlertAsync("Usuario actualizado correctamente", string.Empty);
            }
        }

        private async Task<string> UploadImage(int id, MediaFile file)
        {
            if (file == null)
            {
                return string.Empty;
            }

            var stream = new Refit.StreamPart(file.GetStream(), "image.jpeg", "image/jpeg"); ////TODO no estoy seguro si usar este formato

            var result = await TaskHelperFactory.CreateInternetAccessViewModelInstance(LoggingService, this).
                                TryExecuteAsync(() => clientApi.UploadPhoto(id, stream));

            return result.IsSuccess ? result.Value : string.Empty;
        }
    }
}
