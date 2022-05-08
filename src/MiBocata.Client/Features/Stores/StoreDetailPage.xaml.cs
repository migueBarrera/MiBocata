namespace MiBocata.Features.Stores;

public partial class StoreDetailPage
{
    public StoreDetailPage(StoreDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}