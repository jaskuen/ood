namespace FileEncryptor.Lib.Decorators;

public abstract class OutputStreamDecorator : IOutputStream
{
    protected readonly IOutputStream OutputStream;

    protected OutputStreamDecorator(IOutputStream outputStream)
    {
        OutputStream = outputStream;
    }

    public abstract void WriteByte(byte value);

    public abstract void WriteBlock(IList<byte> sourceData, int dataSize);

    public abstract void Close();

    public void Dispose()
    {
        OutputStream.Close();
        OutputStream.Dispose(); 
        
        GC.SuppressFinalize(this);
    }
}