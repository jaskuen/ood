namespace FileEncryptor.Lib.Decorators;

public class Decompressor : InputStreamDecorator
{
    private byte _currentByte;
    private int _remainingCount;
    private bool _isEof;

    public Decompressor(IInputStream inputStream) : base(inputStream)
    {
        _remainingCount = 0;
        _isEof = false;
    }

    public override bool IsEOF()
    {
        return (_isEof && _remainingCount == 0) || InputStream.IsEOF();
    }

    public override byte ReadByte()
    {
        // Remaining decompressed bytes
        if (_remainingCount > 0)
        {
            _remainingCount--;
            return _currentByte;
        }

        // next [count][value]
        if (InputStream.IsEOF())
        {
            _isEof = true;
            throw new EndOfStreamException("End of compressed stream reached");
        }

        byte count = InputStream.ReadByte();

        if (InputStream.IsEOF())
        {
            _isEof = true;
            throw new InvalidDataException("Invalid RLE stream: missing value after count");
        }

        _currentByte = InputStream.ReadByte();
        _remainingCount = count - 1;

        return _currentByte;
    }

    public override long ReadBlock(IList<byte> destinationData, int dataSize)
    {
        int bytesRead = 0;

        for (int i = 0; i < dataSize; i++)
        {
            if (IsEOF())
            {
                break;
            }

            try
            {
                if (i < destinationData.Count)
                {
                    destinationData[i] = ReadByte();
                }
                else
                {
                    destinationData.Add(ReadByte());
                }

                bytesRead++;
            }
            catch (EndOfStreamException)
            {
                break;
            }
        }

        return bytesRead;
    }
}