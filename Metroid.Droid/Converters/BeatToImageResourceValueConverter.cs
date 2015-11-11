using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;
using DiodeTeam.Metroid.Core.Models;

namespace DiodeTeam.Metroid.Droid.Converters
{
    public class BeatToImageResourceValueConverter : MvxValueConverter<Beat, int>
    {
        protected override int Convert (Beat beat, Type targetType, object parameter, CultureInfo culture)
        {
            switch (beat.Value)
            {
                case 1:
                    return Resource.Drawable.whole;
                case 2:
                    return Resource.Drawable.half;
                case 4:
                    return Resource.Drawable.quarter;
                case 8:
                    return Resource.Drawable.eighth;
                case 16:
                    return Resource.Drawable.sixteenth;
                case 32:
                    return Resource.Drawable.thirty_second;
            }

            return Resource.Drawable.quarter;
        }
    }
}

