﻿namespace MiBocata.Businnes.Features.Products;

public partial class ProductsPage
{
    public ProductsPage(ProductsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}