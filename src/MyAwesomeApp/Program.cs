using System;
using log4net;

namespace MyAwesomeApp
{
  public class Program
  {
    private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));
    static void Main(string[] args)
    {
      log4net.Config.XmlConfigurator.Configure();
      Console.WriteLine("Hello World");
      Logger.DebugFormat("App called with {0} argument(s).", args.Length);
      Console.WriteLine("Press enter to exit");
      Console.ReadLine(); 
    }
  }
}
