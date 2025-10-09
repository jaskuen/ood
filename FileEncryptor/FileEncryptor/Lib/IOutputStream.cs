namespace FileEncryptor.Lib;

public interface IOutputStream : IDisposable
{
    public void WriteByte(byte value);
    public void WriteBlock(IList<byte> sourceData, int dataSize);
    public void Close();
}