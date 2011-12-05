using System;
using System.ComponentModel;
using System.Reflection;

namespace Unity.Settings
{
   internal static class ReflectionExtensions
   {
      public static void Set(this PropertyInfo prop, object obj, object value)
      {
         var converter = TypeDescriptor.GetConverter(prop.PropertyType);
         if (converter != null && converter.CanConvertFrom(value.GetType())) {
            var convertedValue = converter.ConvertFrom(value);
            prop.SetValue(obj, convertedValue, null);
         }
      }
   }
}
