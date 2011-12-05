using System;
using System.Configuration;

namespace Unity.Settings
{
   public class AppSettingsProvider : NameValueSettingsProvider
   {
      public AppSettingsProvider() : base(ConfigurationManager.AppSettings) { }
   }
}
