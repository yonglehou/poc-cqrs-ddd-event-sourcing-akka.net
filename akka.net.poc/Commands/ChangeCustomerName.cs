using System;

namespace Business.Commands
{
    public class ChangeCustomerName
    {
        public Guid CommandId { get; set; }
        public Guid CustomerId { get; set; }
        public string NewName { get; set; }
    }
}