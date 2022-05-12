using System.Windows.Input;

namespace MiBocata.Features.Stores;

public class AddProductViewModel : CoreViewModel
{
    private OrderProduct product;
    private readonly ISessionService sessionService;

    public AddProductViewModel(ISessionService sessionService)
    {
        this.sessionService = sessionService;
    }

    public OrderProduct Product
    {
        get => product;
        set => SetAndRaisePropertyChanged(ref product, value);
    }

    public ICommand AddCommand => new AsyncCommand(AddCommandExecute);

    private async Task AddCommandExecute()
    {
        //await Hub.PublishAsync(Product);
        await Shell.Current.GoToAsync("..");
    }

    public override Task OnAppearingAsync()
    {
        //TODO resolve navigationData 
        //var p = navigationData as Product;
        //if (p != null)
        //{
        //    Product = OrderProduct.Parse(p);
        //}
        var p = sessionService.Get<Product>("AddProduct");
        Product = OrderProduct.Parse(p);

        return base.OnAppearingAsync();
    }
}
