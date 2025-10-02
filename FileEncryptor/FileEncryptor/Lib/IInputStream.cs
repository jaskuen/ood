namespace FileEncryptor.Lib;

public interface IInputStream
{
    public bool IsEOF();
    public byte ReadByte();
    public long ReadBlock(ref byte[] destinationData, int dataSize);
}