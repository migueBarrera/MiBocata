namespace MiBocata.Features.Cart;

public partial class CartPage
{
    public CartPage(CartViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}