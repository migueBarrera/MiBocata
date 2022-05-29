using Mibocata.Core.Features.Clients;
using Mibocata.Core.Features.Refit;
using Models.Requests;
using System.Windows.Input;

namespace MiBocata.Features.Profile;

public class EditProfileViewModel : CoreViewModel
{
    //private MediaFile mediaFile;
    private Models.Core.Client client;
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

    public Models.Core.Client Client { get => client; set => SetAndRaisePropertyChanged(ref client, value); }

    public ICommand TakeImageCommand => new AsyncCommand(() => TakeImageCommandAsync());

    public ICommand SaveCommand => new AsyncCommand(() => SaveCommandExecute());

    private Task TakeImageCommandAsync()
    {
        return Task.CompletedTask;
        //var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
        //{
        //    PhotoSize = PhotoSize.Small,
        //});

        //if (file == null)
        //{
        //    return;
        //}

        //mediaFile = file;
        //Productimage = ImageSource.FromStream(() => file.GetStream());
    }

    public override Task OnAppearingAsync()
    {
        Client = preferencesService.GetClient();
        //mediaFile = null;
        if (!string.IsNullOrEmpty(Client.Image))
        {
            Productimage = ImageSource.FromUri(new Uri(Client.Image));
        }

        return base.OnAppearingAsync();
    }

    private async Task SaveCommandExecute()
    {
        var result = await taskHelperFactory.
                                CreateInternetAccessViewModelInstance(loggingService, this).
                                TryExecuteAsync(
                                () => clientApi.UploadClient(Client.Id, ClientUpdateRequest.Parse(Client)));

        if (result)
        {
            //if (mediaFile != null)
            //{
            //    var urlImage = await UploadImage(Client.Id, mediaFile);
            //    if (!string.IsNullOrWhiteSpace(urlImage))
            //    {
            //        Client.Image = urlImage;
            //    }
            //}

            preferencesService.SetClient(Client);

            await dialogService.ShowAlertAsync("Usuario actualizado correctamente", string.Empty);
        }
    }

    //private async Task<string> UploadImage(int id, MediaFile file)
    //{
    //    if (file == null)
    //    {
    //        return string.Empty;
    //    }

    //    var stream = new Refit.StreamPart(file.GetStream(), "image.jpeg", "image/jpeg"); ////TODO no estoy seguro si usar este formato

    //    var result = await taskHelperFactory.CreateInternetAccessViewModelInstance(loggingService, this).
    //                        TryExecuteAsync(() => clientApi.UploadPhoto(id, stream));

    //    return result.IsSuccess ? result.Value : string.Empty;
    //}
}
