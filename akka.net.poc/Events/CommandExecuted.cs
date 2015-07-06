using System;

namespace Business.Events
{
    public class CommandExecuted<TPayload>
    {
        public Guid CommandId { get; set; }
        public TPayload Payload { get; set; }
    }

    public class CommandExecuted
    {
        public Guid CommandId { get; set; }
    }
}