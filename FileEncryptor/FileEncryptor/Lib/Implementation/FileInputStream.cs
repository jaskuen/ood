namespace FileEncryptor.Lib.Implementation;

public class FileInputStream : IInputStream
{
    private readonly FileStream _fileStream;

    public FileInputStream(string fileName)
    {
        _fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
    }

    public bool IsEOF()
    {
        return _fileStream.Position >= _fileStream.Length;
    }

    public byte ReadByte()
    {
        byte[] buffer = new byte[1];
        try
        {
            _fileStream.ReadExactly(buffer, 0, 1);
        }
        catch (Exception e)
        {
            throw new IOException("Failed to read byte from stream", e);
        }

        return buffer[0];
    }

    public long ReadBlock(IList<byte> destinationData, int dataSize)
    {
        long dataSizeToRead = long.Min(dataSize, _fileStream.Length - _fileStream.Position);
        byte[] buffer = new byte[dataSizeToRead];
        try
        {
            _fileStream.ReadExactly(buffer, 0, (int)dataSizeToRead);
            for (int i = 0; i < dataSizeToRead; i++)
            {
                if (i < destinationData.Count)
                {
                    destinationData[i] = buffer[i];
                }
                else
                {
                    destinationData.Add(buffer[i]);
                }
            }
        }
        catch (Exception e)
        {
            throw new IOException("Failed to read block from stream", e);
        }

        return dataSizeToRead;
    }

    public void Dispose()
    {
        _fileStream.Close();
        _fileStream.Dispose();
        GC.SuppressFinalize(this);
    }
}