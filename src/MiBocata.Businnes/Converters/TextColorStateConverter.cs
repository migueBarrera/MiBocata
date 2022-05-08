using Models.Core;

namespace MiBocata.Businnes.Converters
{
    public class TextColorStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var state = (OrderStates)value;
            var color = Colors.Red;
            switch (state)
            {
                case OrderStates.ACCEPTED:
                    color = Colors.Green;
                    break;
                case OrderStates.REJECTED:
                    color = Colors.Red;
                    break;
                case OrderStates.AUTOACCEPTED:
                    color = Colors.Green;
                    break;
                case OrderStates.STARTED:
                    color = Colors.Orange;
                    break;
                case OrderStates.DEFAULT:
                    color = Colors.Orange;
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