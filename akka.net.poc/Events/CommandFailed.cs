using System;

namespace Business.Events
{
    public class CommandFailed
    {
        public Guid CommandId { get; set; }
        public string ErrorMessage { get; set; }
    }
}