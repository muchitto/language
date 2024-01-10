namespace Syntax.Nodes;

public static class NodeTestHelper
{
    public static bool TestEquals<T>(this List<T> a, List<T> b) where T : BaseNode
    {
        if (a.Count != b.Count)
        {
            return false;
        }

        for (var i = 0; i < a.Count; i++)
        {
            if (!a[i].TestEquals(b[i]))
            {
                return false;
            }
        }

        return true;
    }

    public static bool TestEqualsForDictionary<TK, TV>(this Dictionary<TK, TV> a, Dictionary<TK, TV> b)
        where TK : BaseNode where TV : BaseNode
    {
        if (a.Count != b.Count)
        {
            return false;
        }

        foreach (var (key, value) in a)
        {
            if (!b.TryGetValue(key, out var otherValue))
            {
                return false;
            }

            if (!value.TestEquals(otherValue))
            {
                return false;
            }
        }

        return true;
    }

    public static bool TestEqualsOrBothNull<T, TK>(this T? a, TK? b) where T : BaseNode where TK : BaseNode
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.TestEquals(b);
    }
}