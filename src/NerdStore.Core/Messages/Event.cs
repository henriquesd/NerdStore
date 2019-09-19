using MediatR;
using System;

namespace NerdStore.Core.Messages
{
    public abstract class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; } // o horário que o evento foi disparado;

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}