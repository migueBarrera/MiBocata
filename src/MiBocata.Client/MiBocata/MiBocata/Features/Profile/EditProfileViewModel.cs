using Mibocata.Core.Features.Clients;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Framework;
using MiBocata.Services.PreferencesService;
using Models.Core;
using Models.Requests;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MiBocata.Features.Profile
{
    public class EditProfileViewModel : CoreViewModel
    {
        private MediaFile mediaFile;
        private Client client;
        private ImageSource productimage;
        private readonly IClientApi clientApi;
        private readonly IPreferencesService preferencesService;
        private readonly ILoggingService loggingService;
        private readonly IDialogService dialogService;
        private readonly ITaskHelperFactory taskHelperFactory;

        public EditProfileViewModel(
              IPreferencesService preferencesService,
              ILoggingService loggingService,
              IDialogService dialogService,
              IRefitService refitService,
              ITaskHelperFactory taskHelperFactory)
        {
            clientApi = refitService.InitRefitInstance<IClientApi>(isAutenticated: true);
            this.preferencesService = preferencesService;
            this.loggingService = loggingService;
            this.dialogService = dialogService;
            this.taskHelperFactory = taskHelperFactory;
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
            Client = preferencesService.GetUser();
            mediaFile = null;
            if (!string.IsNullOrEmpty(Client.Image))
            {
                Productimage = ImageSource.FromUri(new Uri(Client.Image));
            }

            return base.InitializeAsync(navigationData);
        }

        private async Task SaveCommandExecute(object arg)
        {
            var result = await taskHelperFactory.
                                    CreateInternetAccessViewModelInstance(loggingService, this).
                                    TryExecuteAsync(
                                    () => clientApi.UploadClient(Client.Id, ClientUpdateRequest.Parse(Client)));

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

                preferencesService.SetUser(Client);

                await dialogService.ShowAlertAsync("Usuario actualizado correctamente", string.Empty);
            }
        }

        private async Task<string> UploadImage(int id, MediaFile file)
        {
            if (file == null)
            {
                return string.Empty;
            }

            var stream = new Refit.StreamPart(file.GetStream(), "image.jpeg", "image/jpeg"); ////TODO no estoy seguro si usar este formato

            var result = await taskHelperFactory.CreateInternetAccessViewModelInstance(loggingService, this).
                                TryExecuteAsync(() => clientApi.UploadPhoto(id, stream));

            return result.IsSuccess ? result.Value : string.Empty;
        }
    }
}
