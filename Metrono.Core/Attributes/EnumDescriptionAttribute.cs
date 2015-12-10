using System;

namespace DiodeCompany.Metrono.Core.Attributes
{
    [AttributeUsage (AttributeTargets.Field)]
    public class EnumDescriptionAttribute : Attribute
    {
        public string Description { get; set; }

        public EnumDescriptionAttribute (string description = "")
        {
            Description = description;
        }
    }
}

