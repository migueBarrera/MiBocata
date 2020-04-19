using MiBocata.Framework;
using Models;
using Models.Helpers;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiBocata.Features.Stores
{
    public class AddProductViewModel : BaseViewModel
    {
        private OrderProduct product;

        public OrderProduct Product
        {
            get => product;
            set => SetAndRaisePropertyChanged(ref product, value);
        }

        public ICommand AddCommand => new AsyncCommand(AddCommandExecute);

        private async Task AddCommandExecute()
        {
            await Hub.PublishAsync(Product);
            await PopupNavigation.Instance.PopAllAsync();
        }

        public override Task InitializeAsync(object navigationData = null)
        {
            var p = navigationData as Product;
            if (p != null)
            {
                Product = ProductMapper.Map(p);
            }

            return base.InitializeAsync(navigationData);
        }
    }
}
