using System;
using Microsoft.Practices.Unity;
using Unity.Settings;

namespace Demo
{
   class Program
   {
      static void Main(string[] args)
      {
         try {
            IUnityContainer container = new UnityContainer();
            container
               .AddNewExtension<TracingExtension>()
               .AddNewExtension<SettingsProviderExtension>()
               .RegisterType<ISettingsProvider, AppSettingsProvider>("AppSettingsProvider", new ContainerControlledLifetimeManager());

            container.Resolve<Worker>().DoWork();
         }
         catch (Exception ex) {
            Console.WriteLine(ex.ToString());
         }

         Console.WriteLine();
         Console.WriteLine("Finished!");
         Console.ReadLine();
      }
   }
}
