using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MiBocata.Controls
{
    public class CustomToggleButton : ContentView
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(CustomToggleButton), null);

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create("CommandParameter", typeof(object), typeof(CustomToggleButton), null);

        public static readonly BindableProperty CheckedProperty =
            BindableProperty.Create(
                "Checked",
                typeof(bool),
                typeof(CustomToggleButton),
                false,
                BindingMode.TwoWay,
                null,
                propertyChanged: OnCheckedChanged);

        public static readonly BindableProperty AnimateProperty =
            BindableProperty.Create("Animate", typeof(bool), typeof(CustomToggleButton), false);

        public static readonly BindableProperty CheckedImageProperty =
            BindableProperty.Create("CheckedImage", typeof(ImageSource), typeof(CustomToggleButton), null);

        public static readonly BindableProperty UnCheckedImageProperty =
            BindableProperty.Create("UnCheckedImage", typeof(ImageSource), typeof(CustomToggleButton), null);

        private ICommand toggleCommand;
        private Image toggleImage;

        public CustomToggleButton()
        {
            Initialize();
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public bool Checked
        {
            get { return (bool)GetValue(CheckedProperty); }
            set { SetValue(CheckedProperty, value); }
        }

        public bool Animate
        {
            get { return (bool)GetValue(AnimateProperty); }
            set { SetValue(CheckedProperty, value); }
        }

        public ImageSource CheckedImage
        {
            get { return (ImageSource)GetValue(CheckedImageProperty); }
            set { SetValue(CheckedImageProperty, value); }
        }

        public ImageSource UnCheckedImage
        {
            get { return (ImageSource)GetValue(UnCheckedImageProperty); }
            set { SetValue(UnCheckedImageProperty, value); }
        }

        public ICommand ToogleCommand
        {
            get
            {
                return toggleCommand
                ?? (toggleCommand = new Command(() =>
                {
                    if (Checked)
                    {
                        Checked = false;
                    }
                    else
                    {
                        Checked = true;
                    }

                    if (Command != null)
                    {
                        Command.Execute(CommandParameter);
                    }
                }));
            }
        }

        private void Initialize()
        {
            toggleImage = new Image();

            Animate = true;

            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = ToogleCommand,
            });

            toggleImage.Source = UnCheckedImage;
            Content = toggleImage;
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            toggleImage.Source = UnCheckedImage;
            Content = toggleImage;
        }

        private static async void OnCheckedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var toggleButton = (CustomToggleButton)bindable;

            if (Equals(newValue, null) && !Equals(oldValue, null))
            {
                return;
            }

            if (toggleButton.Checked)
            {
                toggleButton.toggleImage.Source = toggleButton.CheckedImage;
            }
            else
            {
                toggleButton.toggleImage.Source = toggleButton.UnCheckedImage;
            }

            toggleButton.Content = toggleButton.toggleImage;

            if (toggleButton.Animate)
            {
                await toggleButton.ScaleTo(0.9, 50, Easing.Linear);
                await Task.Delay(100);
                await toggleButton.ScaleTo(1, 50, Easing.Linear);
            }
        }
    }
}