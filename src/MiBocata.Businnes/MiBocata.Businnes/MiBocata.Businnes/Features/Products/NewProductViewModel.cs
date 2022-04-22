using System.Threading.Tasks;
using System.Windows.Input;
using Mibocata.Core.Features.Products;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Services.Commons.Navigation;
using MiBocata.Businnes.Services.Commons.Preferences;
using Models.Core;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace MiBocata.Businnes.Features.Products
{
    public class NewProductViewModel : BaseViewModel
    {
        private readonly IImagesService imagesService;
        private readonly IProductApi productApi;

        private Store store;

        private MediaFile mediaFile;
        private ImageSource productimage;
        private string productName;
        private string productDescription;
        private double productprice;

        public NewProductViewModel(
           IImagesService imagesService,
           INavigationService navigationService,
           IMiBocataNavigationService miBocataNavigationService,
           IPreferencesService preferencesService,
           ISessionService sessionService,
           ILoggingService loggingService,
           IDialogService dialogService,
           IConnectivityService connectivityService,
           ITaskHelper taskHelper,
           IRefitService refitService,
           ITaskHelperFactory taskHelperFactory)
           : base(
                 navigationService,
                 miBocataNavigationService,
                 preferencesService,
                 sessionService,
                 loggingService,
                 dialogService,
                 connectivityService,
                 taskHelper,
                 refitService,
                 taskHelperFactory)
        {
            this.imagesService = imagesService;
            this.productApi = RefitService.InitRefitInstance<IProductApi>(isAutenticated: true);
        }

        public string ProductName
        {
            get => productName; set => SetAndRaisePropertyChanged(ref productName, value);
        }

        public double Productprice
        {
            get => productprice; set => SetAndRaisePropertyChanged(ref productprice, value);
        }
        
        public string ProductDescription
        {
            get => productDescription; set => SetAndRaisePropertyChanged(ref productDescription, value);
        }

        public ImageSource Productimage
        {
            get => productimage; set => SetAndRaisePropertyChanged(ref productimage, value);
        }

        public ICommand TakeImageCommand => new AsyncCommand(_ => TakeImageCommandAsync());

        public ICommand CreateProductCommand => new AsyncCommand(_ => CreateProductCommandAsync());

        public override Task InitializeAsync(object navigationData)
        {
            store = PreferencesService.GetStore();
            Productimage = "profile_placeholder.png";
            ProductName = string.Empty;
            Productprice = 0;
            ProductDescription = string.Empty;
            return base.InitializeAsync(navigationData);
        }

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

        private async Task UploadImage(int id, MediaFile file)
        {
            if (file == null)
            {
                return;
            }

            var stream = new Refit.StreamPart(file.GetStream(), "image.jpeg", "image/jpeg"); ////TODO no estoy seguro si usar este formato

            var result = await TaskHelperFactory.CreateInternetAccessViewModelInstance(LoggingService, this).
                                TryExecuteAsync(() => productApi.UploadPhoto(id, stream));
        }

        private async Task CreateProductCommandAsync()
        {
            if (!await ValidateAsync())
            {
                return;
            }

            if (IsBusy)
            {
                return;
            }

            var result = await TaskHelperFactory.CreateInternetAccessViewModelInstance(LoggingService, this).
                TryExecuteAsync(() => CreateProductAsync());

            if (result)
            {
                if (mediaFile != null)
                {
                    await UploadImage(result.Value.Id, mediaFile);
                }

                PreferencesService.SetStore(store);
                await NavigationService.NavigateBackAsync();
            }
        }

        private async Task<Product> CreateProductAsync()
        {
            var response = await productApi.Create(new Models.Requests.CreateProductRequest()
            {
                Name = ProductName,
                UnitPrice = Productprice,
                StoreId = store.Id,
                Description = ProductDescription,
            });

            var product = new Product()
            {
                Id = response.Id,
                Name = response.Name,
                UnitPrice = response.UnitPrice,
                StoreId = response.Id,
                Description = response.Description,
            };

            store.Products.Add(product);

            return product;
        }

        private async Task<bool> ValidateAsync()
        {
            if (string.IsNullOrWhiteSpace(ProductName))
            {
                await DialogService.ShowMessage("Compruebe el nombre del producto", string.Empty);
                return false;
            }

            if (Productprice <= 0)
            {
                await DialogService.ShowMessage("Compruebe el precio", string.Empty);
                return false;
            }

            return true;
        }
    }
}
