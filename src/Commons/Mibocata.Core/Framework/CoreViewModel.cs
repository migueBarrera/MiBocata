using System.Threading.Tasks;

namespace Mibocata.Core.Framework
{
    public class CoreViewModel : BaseBindable
    {
        private bool isBusy;

        public bool IsBusy
        {
            get => isBusy;
            set => SetAndRaisePropertyChanged(ref isBusy, value);
        }

        public virtual Task InitializeAsync(object navigationData = null)
        {
            return Task.FromResult(false);
        }

        public virtual Task UnitializeAsync(object navigationData = null)
        {
            return Task.FromResult(false);
        }
    }
}
