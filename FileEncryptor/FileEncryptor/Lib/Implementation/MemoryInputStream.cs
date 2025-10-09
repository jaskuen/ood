namespace FileEncryptor.Lib.Implementation;

public class MemoryInputStream : IInputStream
{
    private IList<byte> _data;
    private int _position = 0;

    public MemoryInputStream(byte[] data)
    {
        _data = data;
    }

    public bool IsEOF()
    {
        return _position >= _data.Count;
    }

    public byte ReadByte()
    {
        return _data[_position++];
    }

    public long ReadBlock(IList<byte> destinationData, int dataSize)
    {
        long dataSizeToRead = int.Min(dataSize, _data.Count - _position);
        for (int i = 0; i < dataSizeToRead; i++)
        {
            if (i < destinationData.Count)
            {
                destinationData[i] = ReadByte();
            }
            else
            {
                destinationData.Add(ReadByte());
            }
        }

        return dataSizeToRead;
    }

    public void Dispose()
    {
    }
}