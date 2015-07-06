using System;
using Akka.Actor;
using Business.Commands;
using Business.Events;

namespace Business.Actors
{
    public class CustomerCommanderActor : ReceiveActor
    {
        public CustomerCommanderActor()
        {
            Ready();
        }

        

        private void Ready()
        {
            Receive<CreateCustomer>(cmd =>
            {
                var id = Guid.NewGuid();
                var actorName = string.Format("customer-{0}", id.ToString("N").ToUpper());
                var actor = Context.ActorOf<CustomerAggregateActor>(actorName);

                if (actor.Ask<bool>(cmd).Result)
                {
                    Self.Tell(new CommandExecuted<Guid>
                    {
                        CommandId = cmd.CommandId,
                        Payload = id
                    });
                }
                else
                {
                    Self.Tell(new CommandFailed
                    {
                        CommandId = cmd.CommandId,
                        ErrorMessage = "Falhou!"
                    });
                }
            });

            Receive<ChangeCustomerName>(cmd =>
            {
                var actorPath = string.Format("customer-{0}", cmd.CustomerId.ToString("N").ToUpper());

                if (Context.ActorSelection(actorPath).Ask<bool>(cmd).Result)
                {
                    Self.Tell(new CommandExecuted
                    {
                        CommandId = cmd.CommandId
                    });
                }
                else
                {
                    Self.Tell(new CommandFailed
                    {
                        CommandId = cmd.CommandId,
                        ErrorMessage = "Falhou!"
                    });
                }
            });
        }

    }
}