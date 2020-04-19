using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MiBocata.Businnes.Features.Products.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductItemView : ContentView
    {
        public ProductItemView()
        {
            InitializeComponent();
        }
    }
}