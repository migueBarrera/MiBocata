namespace MiBocata.Features.Orders;

public partial class OrdersPage
{
    public OrdersPage(OrdersViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}