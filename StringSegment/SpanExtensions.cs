namespace Binarycow.Text;

public static class SpanExtensions
{
#if !NETSTANDARD2_1_OR_GREATER
    public static int IndexOfAny(this ReadOnlySpan<char> span, string values)
        => span.IndexOfAny(values.AsSpan());
    public static int IndexOfAny(this ReadOnlySpan<char> span, ReadOnlySpan<char> values)
    {
        for (var i = 0; i < span.Length; ++i)
        {
            if (span.StartsWith(values))
                return i;
        }
        return -1;
    }
#endif
}