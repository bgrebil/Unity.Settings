using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace Unity.Settings
{
   public class SettingsFactory : ISettingsFactory
   {
      private readonly IEnumerable<ISettingsProvider> _providers;

      public SettingsFactory(IEnumerable<ISettingsProvider> providers)
      {
         _providers = providers;
      }

      public object Create(Type type)
      {
         var obj = Activator.CreateInstance(type);
         BuildUp(obj);

         return obj;
      }

      public void BuildUp(object obj)
      {
         var propertiesMissed = PopulateProperties(obj.GetType(), obj);
         if (propertiesMissed.Any()) {
            throw new ConfigurationErrorsException(
               String.Format("{0} is missing the following properties in configuration: {1}", obj.GetType().Name, String.Join(", ", propertiesMissed))
            );
         }
      }

      private IEnumerable<string> PopulateProperties(Type type, object obj)
      {
         var propertiesMissed = new List<string>();

         var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanWrite);

         foreach (var prop in props) {
            var value = _providers.Select(p => p.ProviderValueFor(type, prop.Name)).FirstOrDefault(p => p != null);
            if (value != null) {
               prop.Set(obj, value);
            }
            else {
               propertiesMissed.Add(prop.Name);
            }
         }

         return propertiesMissed;
      }
   }
}
