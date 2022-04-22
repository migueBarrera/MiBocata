using Models.Core;
using System;
using Xamarin.Forms;

namespace MiBocata.Businnes.Converters
{
    public class TextColorStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var state = (OrderStates)value;
            var color = Color.Red;
            switch (state)
            {
                case OrderStates.ACCEPTED:
                    color = Color.Green;
                    break;
                case OrderStates.REJECTED:
                    color = Color.Red;
                    break;
                case OrderStates.AUTOACCEPTED:
                    color = Color.Green;
                    break;
                case OrderStates.STARTED:
                    color = Color.Orange;
                    break;
                case OrderStates.DEFAULT:
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