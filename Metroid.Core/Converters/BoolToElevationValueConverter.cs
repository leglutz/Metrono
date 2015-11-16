using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;

namespace DiodeTeam.Metroid.Core.Converters
{
    public class BoolToElevationValueConverter : MvxValueConverter<bool, int>
    {
        protected override int Convert (bool value, Type targetType, object parameter, CultureInfo culture)
        {
            var elevation = int.Parse(parameter.ToString());
            return value ? elevation : 0;
        }
    }
}

