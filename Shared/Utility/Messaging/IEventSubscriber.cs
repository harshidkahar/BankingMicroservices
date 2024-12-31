using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Messaging
{
    public interface IEventSubscriber
    {
        Task SubscribeAsync(string topicName, string subscriptionName, Func<string, Task> handleMessage);
    }

}
