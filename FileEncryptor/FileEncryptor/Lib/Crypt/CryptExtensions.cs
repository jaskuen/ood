namespace FileEncryptor.Lib.Crypt;

public static class CryptExtensions
{
    public static byte[] CreateCryptTable(int key)
    {
        Random random = new Random(key);
        int n = 256;
        byte[] table = new byte[n];
        for (int i = 0; i < table.Length; i++)
        {
            table[i] = (byte)i;
        }
        
        while (n > 1)
        {
            int k = random.Next(n--);
            (table[n], table[k]) = (table[k], table[n]);
        }
        
        return table;
    }

    public static byte[] CreateDecryptTable(int key)
    {
        byte[] cryptTable = CreateCryptTable(key);
        byte[] decryptTable = new byte[256];

        for (int i = 0; i < decryptTable.Length; i++)
        {
            decryptTable[cryptTable[i]] = (byte)i;
        }
        
        return decryptTable;
    }

    /// <summary>
    /// Crypts first N bytes of list by a crypt table
    /// </summary>
    /// <param name="bytes">List to be crypted</param>
    /// <param name="cryptTable">Crypt table</param>
    /// <param name="count">Count of bytes to be crypted</param>
    public static void CryptBytes(this IList<byte> bytes, byte[] cryptTable, int count)
    {
        int actualCount = int.Min(count, bytes.Count);
        for (int i = 0; i < actualCount; i++)
        {
            bytes[i] = cryptTable[bytes[i]];
        }
    }
}
