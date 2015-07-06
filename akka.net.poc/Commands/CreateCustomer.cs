using System;

namespace Business.Commands
{
    public class CreateCustomer
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}