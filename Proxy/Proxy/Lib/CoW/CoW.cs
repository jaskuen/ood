namespace Proxy.Lib.CoW;

public sealed class CoW<T>
    where T : ICloneable
{
    private sealed class Box
    {
        internal T Value;
        internal int RefCount = 1;

        internal Box(T value) => Value = value!;
    }

    private Box _box;

    public CoW() => _box = new Box(default!);

    public CoW(T? value)
    {
        if (value is null && typeof(T).IsClass)
        {
            _box = new Box(default!);
        }
        else
        {
            _box = new Box(value);
        }
    }

    public CoW(CoW<T> other)
    {
        lock (other._box)
        {
            other._box.RefCount++;
            _box = other._box;
        }
    }

    public int RefCount => _box.RefCount;
    public T Value => _box.Value;

    public void Modify(Action<T> modifier)
    {
        EnsureUnique();
        modifier(_box.Value);
    }

    public TResult Modify<TResult>(Func<T, TResult> modifier)
    {
        EnsureUnique();
        return modifier(_box.Value);
    }

    public void Set(T newValue)
    {
        EnsureUnique();
        _box.Value = newValue;
    }

    private void EnsureUnique()
    {
        if (_box.RefCount > 1)
        {
            lock (_box)
            {
                if (_box.RefCount > 1)
                {
                    _box.RefCount--;
                    T copy = Clone(_box.Value);
                    _box = new Box(copy);
                }
            }
        }
    }

    private static T Clone(T original)
    {
        return original switch
        {
            null => default!,
            ICloneable cloneable => (T)cloneable.Clone()
        };
    }
}