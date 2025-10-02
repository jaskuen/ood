namespace FileEncryptor.Lib.Implementation;

public class FileInputStream : IInputStream, IDisposable
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

    public long ReadBlock(ref byte[] destinationData, int dataSize)
    {
        byte[] buffer = new byte[dataSize];
        try
        {
            _fileStream.ReadExactly(buffer, 0, dataSize);
            destinationData = buffer;
        }
        catch (Exception e)
        {
            throw new IOException("Failed to read block from stream", e);
        }

        return _fileStream.Position;
    }

    public void Dispose()
    {
        _fileStream.Close();
        _fileStream.Dispose();
        GC.SuppressFinalize(this);
    }
}