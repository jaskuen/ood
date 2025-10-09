namespace FileEncryptor.Lib;

public interface IInputStream : IDisposable
{
    public bool IsEOF();
    public byte ReadByte();
    public long ReadBlock(IList<byte> destinationData, int dataSize);
}