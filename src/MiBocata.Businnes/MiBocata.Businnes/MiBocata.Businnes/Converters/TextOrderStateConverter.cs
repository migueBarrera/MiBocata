using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MiBocata.Businnes.Converters
{
    public class TextOrderStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var state = (Models.OrderStates)value;
            var text = string.Empty;
            switch (state)
            {
                case Models.OrderStates.ACCEPTED:
                    text = "Aceptado";
                    break;
                case Models.OrderStates.REJECTED:
                    text = "Rechazado";
                    break;
                case Models.OrderStates.AUTOACCEPTED:
                    text = "Auto aceptado";
                    break;
                case Models.OrderStates.STARTED:
                    text = "Pendiente";
                    break;
                case Models.OrderStates.DEFAULT:
                    text = "DEFAULT";
                    break;
            }

            return text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}