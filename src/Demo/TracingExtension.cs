using System;
using System.Diagnostics;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;

namespace Demo
{
   public class TracingExtension : UnityContainerExtension
   {
      protected override void Initialize()
      {
         Context.Strategies.AddNew<TraceResolverStrategy>(UnityBuildStage.PostInitialization);

         Context.Registering += OnRegistering;
         Context.RegisteringInstance += OnRegisteringInstance;
      }

      public override void Remove()
      {
         Context.Registering -= OnRegistering;
         Context.RegisteringInstance -= OnRegisteringInstance;
      }

      void OnRegistering(object sender, RegisterEventArgs e)
      {
         Trace.WriteLine(String.Format("Registering: {0}", e.Name ?? "_Default_"));
         Trace.WriteLine(String.Format("   Type:     {0}", e.TypeTo));
         Trace.WriteLine(String.Format("   MappedTo: {0}", e.TypeFrom));
         if (e.LifetimeManager != null) {
            Trace.WriteLine(String.Format("   Lifetime: {0}", e.LifetimeManager.GetType()));
         }
      }

      void OnRegisteringInstance(object sender, RegisterInstanceEventArgs e)
      {
         Trace.WriteLine(String.Format("Registering Instance: {0}", e.Name ?? "_Default_"));
         Trace.WriteLine(String.Format("   Type:     {0}", e.RegisteredType));
         Trace.WriteLine(String.Format("   Lifetime: {0}", e.LifetimeManager.GetType()));
      }
   }

   internal class TraceResolverStrategy : BuilderStrategy
   {
      public override void PostBuildUp(IBuilderContext context)
      {
         var key = context.OriginalBuildKey;
         Trace.WriteLine(String.Format("Resolving: {0}", key.Name ?? "_Default_"));
         Trace.WriteLine(String.Format("   Type:     {0}", context.Existing.GetType()));
         Trace.WriteLine(String.Format("   MappedTo: {0}", key.Type));
      }
   }
}
