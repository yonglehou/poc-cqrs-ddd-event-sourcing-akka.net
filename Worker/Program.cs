using System;
using Akka.Actor;
using Akka.Configuration;
using Business.Actors;
using Business.Commands;
using Serilog;

namespace Worker
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.ColoredConsole()
               .CreateLogger();
            try
            {
                Config config = ConfigurationFactory.FromResource<Program>("Worker.Akka.conf");
                ActorSystem actorSystem = ActorSystem.Create("ControlPanel", config);
                IActorRef commander = actorSystem.ActorOf(Props.Create(() => new CustomerCommanderActor()), ActorPaths.CustomerCommanderActor.Name);

                var cmdCreate = new CreateCustomer
                {
                    CustomerName = "Brasdril"
                };

                var customerId = commander.Ask<Guid>(cmdCreate).Result;

                Console.WriteLine("Guid Customer Brasdril: " + customerId);

                var cmdChangeName = new ChangeCustomerName
                {
                    CustomerId = customerId,
                    NewName = "Brasdril SA"
                };

                var nameChanged = commander.Ask<bool>(cmdChangeName).Result;


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}