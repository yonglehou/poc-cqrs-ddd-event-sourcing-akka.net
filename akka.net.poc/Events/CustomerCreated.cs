using System;

namespace Business.Events
{
    public class CustomerCreated
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}