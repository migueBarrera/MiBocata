using MiBocata.Services.API.RefitServices;
using MiBocata.Services.ConnectivityServices;
using MiBocata.Services.DialogService;
using MiBocata.Services.KeyboardService;
using MiBocata.Services.LoggingService;
using MiBocata.Services.NavigationService;
using MiBocata.Services.PreferencesService;
using MiBocata.Services.SessionService;
using MiBocata.Services.TasksServices;
using PubSub;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MiBocata.Framework
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected readonly IMiBocataNavigationService NavigationService;
        protected readonly IPreferencesService PreferencesService;
        protected readonly ISessionService SessionService;
        protected readonly IKeyboardService KeyboardService;
        protected readonly ILoggingService LoggingService;
        protected readonly IDialogService DialogService;
        protected readonly IConnectivityService ConnectivityService;
        protected readonly IRefitService RefitService;
        protected Hub Hub = Hub.Default;

        private bool isBusy;
        protected readonly TaskHelperFactory TaskHelperFactory;

        public BaseViewModel()
        {
            this.NavigationService = Locator.Resolve<IMiBocataNavigationService>();
            this.PreferencesService = Locator.Resolve<IPreferencesService>();
            this.SessionService = Locator.Resolve<ISessionService>();
            this.LoggingService = Locator.Resolve<ILoggingService>();
            this.KeyboardService = DependencyService.Get<IKeyboardService>();
            this.DialogService = Locator.Resolve<IDialogService>();
            this.RefitService = Locator.Resolve<IRefitService>();
            this.ConnectivityService = Locator.Resolve<IConnectivityService>();

            TaskHelperFactory = new TaskHelperFactory(DialogService, ConnectivityService);
        }

        public event PropertyChangedEventHandler PropertyChanged;

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

        protected void SetAndRaisePropertyChanged<TRef>(
     ref TRef field, TRef value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetAndRaisePropertyChangedIfDifferentValues<TRef>(
            ref TRef field, TRef value, [CallerMemberName] string propertyName = null)
            where TRef : class
        {
            if (field == null || !field.Equals(value))
            {
                SetAndRaisePropertyChanged(ref field, value, propertyName);
            }
        }
    }
}
