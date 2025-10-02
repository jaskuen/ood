using System.Text;

namespace FileEncryptor.Lib.Implementation;

public class FileOutputStream : IOutputStream, IDisposable
{
    private readonly FileStream _fileStream;

    public FileOutputStream(string fileName)
    {
        _fileStream = new FileStream(fileName, FileMode.Truncate, FileAccess.Write);
    }

    public void WriteByte(byte value)
    {
        try
        {
            _fileStream.WriteByte(value);
        }
        catch (Exception e)
        {
            throw new IOException("Failed to write byte to a stream", e);
        }
    }

    public void WriteBlock(byte[] sourceData, int dataSize)
    {
        if (!_fileStream.CanWrite)
        {
            throw new InvalidOperationException("Cannot write to a file stream");
        }

        try
        {
            _fileStream.Write(sourceData, 0, dataSize);
        }
        catch (Exception e)
        {
            throw new IOException("Failed to write block to a stream: ", e);
        }
    }

    public void Dispose()
    {
        _fileStream.Flush();
        _fileStream.Dispose();
        GC.SuppressFinalize(this);
    }
}