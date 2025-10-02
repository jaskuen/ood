namespace FileEncryptor.Lib;

public interface IOutputStream
{
    public void WriteByte(byte value);
    public void WriteBlock(byte[] sourceData, int dataSize);
}