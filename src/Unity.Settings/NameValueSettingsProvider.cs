using System;
using System.Collections.Specialized;

namespace Unity.Settings
{
   public class NameValueSettingsProvider : ISettingsProvider
   {
      private readonly NameValueCollection _source;

      public NameValueSettingsProvider(NameValueCollection source)
      {
         _source = source;
      }

      public object ProviderValueFor(Type type, string propertyName)
      {
         var key = type.Name + "." + propertyName;
         return _source[key];
      }
   }
}
