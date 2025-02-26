using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Encryption
{
    public class EncryptedPayload
    {
        public string body { get; set; }
        public string key { get; set; }
        public string iv { get; set; }
        public string signature { get; set; }
    }
}
