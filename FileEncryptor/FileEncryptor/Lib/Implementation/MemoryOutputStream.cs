namespace FileEncryptor.Lib.Implementation;

public class MemoryOutputStream : IOutputStream
{
    private IList<byte> _data;
    private bool _closed;

    public MemoryOutputStream(IList<byte> data)
    {
        _data = data;
    }

    public void WriteByte(byte value)
    {
        CheckIfClosed();
        _data.Add(value);
    }

    public void WriteBlock(IList<byte> sourceData, int dataSize)
    {
        CheckIfClosed();
        for (var i = 0; i < dataSize; i++)
        {
            _data.Add(sourceData[i]);
        }
    }

    public void Close() => _closed = true;

    private void CheckIfClosed()
    {
        if (_closed)
        {
            throw new InvalidOperationException("Output stream is closed");
        }
    }

    public void Dispose()
    {
    }
}