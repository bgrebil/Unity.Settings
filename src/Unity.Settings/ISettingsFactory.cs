using System;

namespace Unity.Settings
{
   public interface ISettingsFactory
   {
      object Create(Type type);
      void BuildUp(object obj);
   }
}
