namespace FileEncryptor.Lib.Decorators;

public class Compressor : OutputStreamDecorator
{
    private byte? _currentByte;
    private int _count;

    public Compressor(IOutputStream outputStream) : base(outputStream)
    {
        _currentByte = null;
        _count = 0;
    }

    public override void WriteByte(byte value)
    {
        if (_currentByte == null)
        {
            _currentByte = value;
            _count = 1;
        }
        else if (_currentByte == value)
        {
            _count++;
            if (_count == 255)
            {
                FlushRun();
            }
        }
        else
        {
            FlushRun();
            _currentByte = value;
            _count = 1;
        }
    }

    // Поправить использование последнего байта
    public override void WriteBlock(IList<byte> sourceData, int dataSize)
    {
        int actualDataSize = int.Min(dataSize, sourceData.Count);
        for (int i = 0; i < actualDataSize; i++)
        {
            WriteByte(sourceData[i]);
        }

        FlushRun();
    }

    public override void Close()
    {
        FlushRun();
        OutputStream.Close();
    }

    private void FlushRun()
    {
        if (_currentByte == null || _count == 0)
        {
            return;
        }

        // [count][value]
        OutputStream.WriteByte((byte)_count);
        OutputStream.WriteByte(_currentByte.Value);

        _count = 0;
        _currentByte = null;
    }
}