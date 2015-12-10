using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;
using DiodeCompany.Metrono.Core.Attributes;
using DiodeCompany.Metrono.Core.Extensions;

namespace DiodeCompany.Metrono.Core.Converters
{
    public class EnumToDescriptionValueConverter : MvxValueConverter<Enum, string>
    {
        protected override string Convert (Enum enumValue, Type targetType, object parameter, CultureInfo culture)
        {
            return enumValue.GetAttribute<EnumDescriptionAttribute> ().Description;
        }
    }
}

