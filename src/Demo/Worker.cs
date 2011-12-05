using System;

namespace Demo
{
   public class Worker
   {
      private readonly WorkerSettings _settings;

      public Worker(WorkerSettings settings)
      {
         _settings = settings;
      }

      public void DoWork()
      {
         Console.WriteLine("Connecting to {0}:{1}", _settings.Server, _settings.Port);
      }
   }
}
