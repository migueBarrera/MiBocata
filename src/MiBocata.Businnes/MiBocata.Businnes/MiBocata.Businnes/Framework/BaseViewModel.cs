using MiBocata.Businnes.Services.API.RefitServices;
using MiBocata.Businnes.Services.ConnectivityServices;
using MiBocata.Businnes.Services.DialogService;
using MiBocata.Businnes.Services.KeyboardService;
using MiBocata.Businnes.Services.LoggingService;
using MiBocata.Businnes.Services.Navigation;
using MiBocata.Businnes.Services.Preferences;
using MiBocata.Businnes.Services.Session;
using MiBocata.Businnes.Services.TasksServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MiBocata.Businnes.Framework
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected readonly INavigationService NavigationService;
        protected readonly IMiBocataNavigationService MiBocataNavigationService;
        protected readonly IPreferencesService PreferencesService;
        protected readonly ISessionService SessionService;
        protected readonly ILoggingService LoggingService;
        protected readonly IDialogService DialogService;
        protected readonly IKeyboardService KeyboardService;
        protected readonly IConnectivityService ConnectivityService;
        protected readonly ITaskHelper TaskHelper;
        protected readonly IRefitService RefitService;

        private bool isBusy;
        protected readonly TaskHelperFactory TaskHelperFactory;

        public BaseViewModel()
        {
            this.NavigationService = Locator.Resolve<INavigationService>();
            this.PreferencesService = Locator.Resolve<IPreferencesService>();
            this.MiBocataNavigationService = Locator.Resolve<IMiBocataNavigationService>();
            this.SessionService = Locator.Resolve<ISessionService>();
            this.LoggingService = Locator.Resolve<ILoggingService>();
            this.DialogService = Locator.Resolve<IDialogService>();
            this.TaskHelper = Locator.Resolve<ITaskHelper>();
            this.KeyboardService = DependencyService.Get<IKeyboardService>();
            this.ConnectivityService = Locator.Resolve<IConnectivityService>();
            this.RefitService = Locator.Resolve<IRefitService>();

            TaskHelperFactory = new TaskHelperFactory(DialogService, ConnectivityService);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsBusy
        {
            get => isBusy;
            set => SetAndRaisePropertyChanged(ref isBusy, value);
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

        public virtual Task UnitializeAsync(object navigationData)
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
