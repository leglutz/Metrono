using System;
using System.Linq;
using System.Reflection;

namespace DiodeCompany.Metroid.Core.Extensions
{
    public static class EnumExtensions
    {
        public static T ToEnum<T> (this string enumString)
        {
            if (!string.IsNullOrWhiteSpace (enumString))
            {
                try
                {
                    return (T)Enum.Parse (typeof(T), enumString);
                }
                catch (Exception)
                {
                    return default(T);
                }
            }

            return default(T);
        }

        public static T GetAttribute<T> (this Enum value) where T : Attribute
        {
            try
            {
                var type = value.GetType ();
                var name = Enum.GetName (type, value);
                var attributes = type.GetRuntimeField (name).GetCustomAttributes (false).OfType<T> ().ToArray ();
                if (attributes.Length > 0)
                {
                    return attributes [0];
                }

                return default(T);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}

