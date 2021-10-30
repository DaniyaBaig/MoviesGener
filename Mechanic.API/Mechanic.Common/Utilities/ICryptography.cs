using System;
using System.Collections.Generic;
using System.Text;

namespace Mechanic.Common.Utilities
{
    public interface ICryptography
    {
        byte[] EncryptStringToBytes(string plainText);

        string DecryptStringFromBytes(byte[] cipherText);
    }
}
