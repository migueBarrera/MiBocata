using System;
using Xamarin.Forms;

namespace MiBocata.Businnes.Converters
{
    public class TextColorStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var state = (Models.OrderStates)value;
            var color = Color.Red;
            switch (state)
            {
                case Models.OrderStates.ACCEPTED:
                    color = Color.Green;
                    break;
                case Models.OrderStates.REJECTED:
                    color = Color.Red;
                    break;
                case Models.OrderStates.AUTOACCEPTED:
                    color = Color.Green;
                    break;
                case Models.OrderStates.STARTED:
                    color = Color.Orange;
                    break;
                case Models.OrderStates.DEFAULT:
                    color = Color.Orange;
                    break;
            }

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}