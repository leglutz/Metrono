using DiodeCompany.Metrono.Core.Attributes;
using DiodeCompany.Metrono.Core.Extensions;
using MvvmCross.Platform.Converters;
using System;
using System.Globalization;

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

