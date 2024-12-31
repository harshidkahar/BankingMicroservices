using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Messaging
{
    public interface IEventPublisher
    {
        Task PublishAsync<T>(string topicName, T message);
    }

}
