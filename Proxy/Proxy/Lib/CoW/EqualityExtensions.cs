using System.Collections;

namespace Proxy.Lib.CoW;

public static class EqualityExtensions
{
    public new static bool Equals(object obj1, object obj2)
    {
        switch (obj1)
        {
            case IList<int> list1:
                IList<int> list2 = (IList<int>)obj2;
                var firstNotSecond = list1.Except(list2).ToList();
                var secondNotFirst = list2.Except(list1).ToList();
                return !firstNotSecond.Any() && !secondNotFirst.Any();
        }

        return false;
    }
}