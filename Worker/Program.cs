﻿using System;
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


                var guid = commander.Ask<Guid>(new CreateCustomer
                {
                    CustomerName = "Brasdril"
                }).Result;

                Console.WriteLine("Guid Customer Brasdril: "+ guid);

                commander.Ask<bool>(new ChangeCustomerName
                {
                    CustomerId = guid,
                    NewName = "Brasdril SA"
                });


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}