using System.Windows.Input;

namespace MiBocata.Features.Stores;

public class AddProductViewModel : CoreViewModel
{
    private OrderProduct product;

    public AddProductViewModel()
    {
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
        //await PopupNavigation.Instance.PopAllAsync();
    }

    public override Task OnAppearingAsync()
    {
        //TODO resolve navigationData 
        //var p = navigationData as Product;
        //if (p != null)
        //{
        //    Product = OrderProduct.Parse(p);
        //}

        return base.OnAppearingAsync();
    }
}
