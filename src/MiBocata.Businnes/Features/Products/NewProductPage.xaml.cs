namespace MiBocata.Businnes.Features.Products;

public partial class NewProductPage
{
    public NewProductPage(NewProductViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}