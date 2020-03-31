using System;
using System.Collections.Generic;
using System.Text;

namespace BlugraryDetectionSystemBAL.Contracts
{
    public interface ICryptographyBAL
    {
        string EncryptData(string message, out string salt);
        string DecryptData(string ecnryptedMessage, string salt);
    }
}
