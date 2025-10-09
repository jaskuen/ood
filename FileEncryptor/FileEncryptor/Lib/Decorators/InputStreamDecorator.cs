namespace FileEncryptor.Lib.Decorators;

public abstract class InputStreamDecorator : IInputStream
{
    protected readonly IInputStream InputStream;

    protected InputStreamDecorator(IInputStream inputStream)
    {
        InputStream = inputStream;
    }

    public abstract bool IsEOF();

    public abstract byte ReadByte();

    public abstract long ReadBlock(IList<byte> destinationData, int dataSize);
    public void Dispose() => InputStream.Dispose();
}