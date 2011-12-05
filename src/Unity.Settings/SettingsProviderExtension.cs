using System;
using System.Collections.Generic;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;

namespace Unity.Settings
{
   public class SettingsProviderExtension : UnityContainerExtension
   {
      protected override void Initialize()
      {
         Container.RegisterType<ISettingsFactory, SettingsFactory>("DefaultSettingsFactory", new ContainerControlledLifetimeManager());
         Container.RegisterType<IEnumerable<ISettingsProvider>, ISettingsProvider[]>();

         Context.Strategies.Add(new BuildUpSettingsStrategy(), UnityBuildStage.Initialization);
      }
   }

   internal class BuildUpSettingsStrategy : BuilderStrategy
   {
      public override void PreBuildUp(IBuilderContext context)
      {
         if (context.Existing != null && context.Existing.GetType().IsClass && context.Existing.GetType().Name.EndsWith("Settings")) {
            var settingsFactory = GetSettingsFactory(context);
            settingsFactory.BuildUp(context.Existing);
         }
      }

      private static ISettingsFactory GetSettingsFactory(IBuilderContext context)
      {
         var factory = context.NewBuildUp<ISettingsFactory>("DefaultSettingsFactory");
         if (factory == null) {
            throw new InvalidOperationException("No instance of ISettingsFactory available.");
         }

         return factory;
      }
   }
}
