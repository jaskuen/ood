using FileEncryptor.Lib.Crypt;

namespace FileEncryptor.Lib.Decorators;

public class Encryptor : OutputStreamDecorator
{
    private byte[] _encryptTable;

    public Encryptor(IOutputStream outputStream, int key) : base(outputStream)
    {
        _encryptTable = CryptExtensions.CreateCryptTable(key);
    }

    public override void Close() => OutputStream.Close();

    public override void WriteByte(byte value)
    {
        byte b = _encryptTable[value];
        OutputStream.WriteByte(b);
    }

    public override void WriteBlock(IList<byte> sourceData, int dataSize)
    {
        IList<byte> temp = new List<byte>(sourceData);
        temp.CryptBytes(_encryptTable, dataSize);
        
        OutputStream.WriteBlock(temp, dataSize);
    }

}