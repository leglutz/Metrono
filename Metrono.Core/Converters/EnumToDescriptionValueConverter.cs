using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;
using DiodeCompany.Metroid.Core.Attributes;
using DiodeCompany.Metroid.Core.Extensions;

namespace DiodeCompany.Metroid.Core.Converters
{
    public class EnumToDescriptionValueConverter : MvxValueConverter<Enum, string>
    {
        protected override string Convert (Enum enumValue, Type targetType, object parameter, CultureInfo culture)
        {
            return enumValue.GetAttribute<EnumDescriptionAttribute> ().Description;
        }
    }
}

