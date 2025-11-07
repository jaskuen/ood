namespace Proxy.Lib.CoW;

public class CoW<T>
    where T : class
{
    private SharedPtr<T> _obj;

    public CoW(SharedPtr<T> obj)
    {
        _obj = obj;
        _obj.IncrementUsageCount();
    }

    private CoW(CoW<T> other)
    {
        if (other == null) throw new ArgumentNullException(nameof(other));
        _obj = other._obj;
        _obj.IncrementUsageCount();
    }

    public CoW<T> Clone()
    {
        return new CoW<T>(this);
    }

    public virtual T Value
    {
        get => _obj.Get();
        set
        {
            if (_obj.GetCount() > 1 && !EqualityExtensions.Equals(value, _obj.Get()))
            {
                _obj = new SharedPtr<T>(value);
            }
            else
            {
                _obj.Set(value);
            }
        }
    }
}