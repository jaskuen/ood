using FileEncryptor.Lib.Crypt;

namespace FileEncryptor.Lib.Decorators;

public class Decryptor : InputStreamDecorator
{
    private byte[] _decryptTable;
    public Decryptor(IInputStream inputStream, int key) : base(inputStream)
    {
        _decryptTable = CryptExtensions.CreateDecryptTable(key);
    }

    public override bool IsEOF() => InputStream.IsEOF();

    public override byte ReadByte()
    {
        byte b = InputStream.ReadByte();
        return _decryptTable[b];
    }

    public override long ReadBlock(IList<byte> destinationData, int dataSize)
    {
        IList<byte> temp = new List<byte>();
        long realDataSize = InputStream.ReadBlock(temp, dataSize);

        temp.CryptBytes(_decryptTable, dataSize);
        for (int i = 0; i < realDataSize; i++)
        {
            if (i < destinationData.Count)
            {
                destinationData[i] = temp[i];
            }
            else
            {
                destinationData.Add(temp[i]);
            }
        }
        
        return realDataSize;
    }
}