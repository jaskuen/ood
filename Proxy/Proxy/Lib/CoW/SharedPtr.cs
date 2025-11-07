namespace Proxy.Lib.CoW;

public class SharedPtr<T>
    where T : class
{
    private int _usageCount;
    private T _value;

    public SharedPtr(T value)
    {
        _value = value;
        
        Console.WriteLine("Created new pointer");
    }

    public int GetCount() => _usageCount;
    public void IncrementUsageCount() => ++_usageCount;

    public T Get() => _value;
    public void Set(T value) => _value = value;
}