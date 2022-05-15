namespace MiBocata.Features.Cart.Templates;

public partial class CartItemTemplate
{
    public CartItemTemplate()
    {
        InitializeComponent();
    }

    private void NumericUpDown_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(NumericUpDown.Value))
        {
            QuantityLabel.Text = NumericUpDown.Value.ToString();
            (this.Parent?.BindingContext as CartViewModel)?.RefresView();
        }
    }
}