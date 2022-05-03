using System.Threading.Tasks;
using System.Windows.Input;
using Mibocata.Core.Features.Products;
using Mibocata.Core.Features.Refit;
using Mibocata.Core.Framework;
using Mibocata.Core.Services.Interfaces;
using MiBocata.Businnes.Framework;
using MiBocata.Businnes.Services.Commons.Navigation;
using Models.Core;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace MiBocata.Businnes.Features.Products
{
    public class NewProductViewModel : CoreViewModel
    {
        private readonly IPreferencesService preferencesService;
        private readonly INavigationService navigationService;
        private readonly ILoggingService loggingService;
        private readonly IDialogService dialogService;
        private readonly ITaskHelperFactory taskHelperFactory;
        private readonly IProductApi productApi;

        private Store store;

        private MediaFile mediaFile;
        private ImageSource productimage;
        private string productName;
        private string productDescription;
        private double productprice;

        public NewProductViewModel(
           IRefitService refitService,
           IPreferencesService preferencesService,
           INavigationService navigationService,
           ILoggingService loggingService,
           IDialogService dialogService,
           ITaskHelperFactory taskHelperFactory)
        {
            this.preferencesService = preferencesService;
            this.navigationService = navigationService;
            this.loggingService = loggingService;
            this.dialogService = dialogService;
            this.taskHelperFactory = taskHelperFactory;
            this.productApi = refitService.InitRefitInstance<IProductApi>(isAutenticated: true);
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

        public ICommand TakeImageCommand => new AsyncCommand(() => TakeImageCommandAsync());

        public ICommand CreateProductCommand => new AsyncCommand(() => CreateProductCommandAsync());

        public override Task InitializeAsync(object navigationData)
        {
            store = preferencesService.GetStore();
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

            var result = await taskHelperFactory.CreateInternetAccessViewModelInstance(loggingService, this).
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

            var result = await taskHelperFactory.CreateInternetAccessViewModelInstance(loggingService, this).
                TryExecuteAsync(() => CreateProductAsync());

            if (result)
            {
                if (mediaFile != null)
                {
                    await UploadImage(result.Value.Id, mediaFile);
                }

                preferencesService.SetStore(store);
                await navigationService.NavigateBackAsync();
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
                await dialogService.ShowMessage("Compruebe el nombre del producto", string.Empty);
                return false;
            }

            if (Productprice <= 0)
            {
                await dialogService.ShowMessage("Compruebe el precio", string.Empty);
                return false;
            }

            return true;
        }
    }
}
