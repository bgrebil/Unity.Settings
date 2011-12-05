using System;

namespace Unity.Settings
{
   public interface ISettingsProvider
   {
      object ProviderValueFor(Type type, string propertyName);
   }
}
