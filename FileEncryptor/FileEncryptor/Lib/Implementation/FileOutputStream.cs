using System.Text;

namespace FileEncryptor.Lib.Implementation;

public class FileOutputStream : IOutputStream
{
    private readonly FileStream _fileStream;
    private bool _closed = false;

    public FileOutputStream(string fileName)
    {
        _fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
    }

    public void WriteByte(byte value)
    {
        CheckIfClosed();
        try
        {
            _fileStream.WriteByte(value);
        }
        catch (Exception e)
        {
            throw new IOException("Failed to write byte to a stream", e);
        }
    }

    public void WriteBlock(IList<byte> sourceData, int dataSize)
    {
        CheckIfClosed();
        if (!_fileStream.CanWrite)
        {
            throw new InvalidOperationException("The stream does not support writing");
        }

        try
        {
            int actualDataSize = int.Min(dataSize, sourceData.Count);
            _fileStream.Write(sourceData.ToArray(), 0, actualDataSize);
        }
        catch (Exception e)
        {
            throw new IOException("Failed to write block to a stream: ", e);
        }
    }

    public void Close()
    {
        _closed = true;
    }

    public void Dispose()
    {
        _fileStream.Flush();
        _fileStream.Dispose();
        GC.SuppressFinalize(this);
    }

    private void CheckIfClosed()
    {
        if (_closed)
        {
            throw new InvalidOperationException("Output stream is closed");
        }
    }
}