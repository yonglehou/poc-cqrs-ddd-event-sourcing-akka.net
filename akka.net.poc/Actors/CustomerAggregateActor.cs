using System;
using Akka.Persistence;
using Business.Commands;
using Business.Events;

namespace Business.Actors
{
    public class CustomerAggregateActor : PersistentActor
    {
        private CustomerState _state = new CustomerState();

        public Guid Id
        {
            get { return _state.Id; }
            private set { _state.Id = value; }
        }

        public string Name
        {
            get { return _state.Name; }
            private set { _state.Name = value; }
        }
        
        public override string PersistenceId
        {
            get { return string.Format("customer-{0}", Id.ToString("N").ToUpper()); }
        }

        protected override bool ReceiveRecover(object message)
        {
            if (message is CustomerCreated)
            {
                Create(message as CustomerCreated);
            }
            else if (message is CustomerNameChanged)
            {
                ChangeName(message as CustomerNameChanged);
            }
            else if (message is SnapshotOffer)
            {
                var state = ((SnapshotOffer)message).Snapshot as CustomerState;

                if (state != null)
                {
                    _state = state;
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        protected override bool ReceiveCommand(object message)
        {
            if (message is CreateCustomer)
            {
                var cmd = (CreateCustomer) message;
                
                Id = cmd.CustomerId;

                var evt = new CustomerCreated
                {
                    CustomerId = cmd.CustomerId,
                    CustomerName = cmd.CustomerName
                };

                Persist(evt, Create);

                Sender.Tell(evt.CustomerId, Self);

                return true;
            } 
            
            if (message is ChangeCustomerName)
            {
                var cmd = (ChangeCustomerName) message;
                var evt = new CustomerNameChanged
                {
                    CustomerId = Id,
                    NewCustomerName = cmd.NewName
                };

                Persist(evt, ChangeName);

                Sender.Tell(true, Self);

                return true;
            }
            
            if (message is string)
            {
                var cmd = (string) message;
                if (cmd == "snapshot")
                {
                    SaveSnapshot(_state);
                }

                return true;
            }

            return false;
        }

        private void ChangeName(CustomerNameChanged evt)
        {
            Name = evt.NewCustomerName;
        }

        private void Create(CustomerCreated evt)
        {
            Id = evt.CustomerId;
            Name = evt.CustomerName;
        }
        
    }

    public class CustomerState
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

}