using System;

namespace Business.Events
{
    public class CustomerNameChanged 
    {
        public Guid CustomerId { get; set; }
        public string NewCustomerName { get; set; }
    }
}