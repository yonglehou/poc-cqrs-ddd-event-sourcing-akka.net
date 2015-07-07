using System;
using Akka.Actor;
using Business.Commands;

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
                var actor = Context.ActorOf(Props.Create<CustomerAggregateActor>(), actorName);

                cmd.CustomerId = id;

                //actor.Tell(cmd);

                var response = actor.Ask<Guid>(cmd).Result;

                Sender.Tell(response, Self);
            });

            Receive<ChangeCustomerName>(cmd =>
            {   
                try
                {
                    var actorPath = string.Format("customer-{0}", cmd.CustomerId.ToString("N").ToUpper());
                    var actor = Context.ActorSelection(actorPath);
                    var response = actor.Ask<bool>(cmd).Result;
                    Sender.Tell(response, Self);
                }
                catch (Exception e)
                {
                    throw;
                }
            });
        }

    }
}

