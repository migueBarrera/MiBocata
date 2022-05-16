namespace MiBocata.Businnes.Features.Orders;

public partial class OrderPage
{
    public OrderPage(OrderViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}