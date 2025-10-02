namespace FileEncryptor.Lib.Implementation;

public class MemoryOutputStream : IOutputStream
{
    public void WriteByte(byte value)
    {
        throw new NotImplementedException();
    }

    public void WriteBlock(byte[] sourceData, int dataSize)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
    }
}