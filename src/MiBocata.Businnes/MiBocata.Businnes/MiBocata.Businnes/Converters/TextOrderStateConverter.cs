using Models.Core;
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
            var state = (OrderStates)value;
            var text = string.Empty;
            switch (state)
            {
                case OrderStates.ACCEPTED:
                    text = "Aceptado";
                    break;
                case OrderStates.REJECTED:
                    text = "Rechazado";
                    break;
                case OrderStates.AUTOACCEPTED:
                    text = "Auto aceptado";
                    break;
                case OrderStates.STARTED:
                    text = "Pendiente";
                    break;
                case OrderStates.DEFAULT:
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