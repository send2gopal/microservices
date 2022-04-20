using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microkart.shared.Abstraction
{
    public record PubSubEvent
    {
        public Guid Id { get; }

        public DateTime CreationDate { get; }

        public PubSubEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }
    }
}
