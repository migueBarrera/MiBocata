namespace MiBocata.Businnes.Features.Stores;

public partial class StorePage
{
    public StorePage(StoreViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}