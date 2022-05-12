namespace MiBocata.Features.Stores;

public partial class AddProductPage
{
    public AddProductPage(AddProductViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}