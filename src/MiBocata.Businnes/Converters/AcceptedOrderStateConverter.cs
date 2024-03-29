﻿using Models.Core;

namespace MiBocata.Businnes.Converters
{
    public class AcceptedOrderStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var state = (OrderStates)value;

            return state == OrderStates.STARTED;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}