using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microkart.shared.Abstraction
{
    public interface IEventBus
    {
        Task PublishAsync(PubSubEvent integrationEvent);
    }
}
