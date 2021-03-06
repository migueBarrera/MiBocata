﻿using MiBocata.Businnes.Framework;
using Xamarin.Forms;

namespace MiBocata.Businnes.Features.Home
{
    public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ((BaseViewModel)BindingContext)?.InitializeAsync(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ((BaseViewModel)BindingContext)?.UnitializeAsync(null);
        }
    }
}