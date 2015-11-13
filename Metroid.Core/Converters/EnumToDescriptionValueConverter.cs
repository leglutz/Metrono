using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;
using DiodeTeam.Metroid.Core.Attributes;
using DiodeTeam.Metroid.Core.Extensions;

namespace DiodeTeam.Metroid.Core.Converters
{
    public class EnumToDescriptionValueConverter : MvxValueConverter<Enum, string>
    {
        protected override string Convert (Enum enumValue, Type targetType, object parameter, CultureInfo culture)
        {
            return enumValue.GetAttribute<EnumDescriptionAttribute> ().Description;
        }
    }
}

