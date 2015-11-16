using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;

namespace DiodeTeam.Metroid.Core.Converters
{
    public class BoolToAlphaValueConverter : MvxValueConverter<bool, double>
    {
        protected override double Convert (bool value, Type targetType, object parameter, CultureInfo culture)
        {
            var alpha = double.Parse(parameter.ToString());
            return value ? 1.0 : alpha;
        }
    }
}

