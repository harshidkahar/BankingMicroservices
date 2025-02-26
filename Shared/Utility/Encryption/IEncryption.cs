using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Encryption
{
    public interface IEncryptionService
    {
        EncryptedPayload EncryptAesManaged(string body);
        string DecryptResponse(string encJson, string encIV, string EncKey, string SignatureData);
    }
}
