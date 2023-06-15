using System.Collections;
using System.Diagnostics;
using System.Globalization;

namespace Binarycow.Text;

[DebuggerTypeProxy(typeof(StringSegmentDebugView))]
[DebuggerDisplay("{DebuggerDisplay,raw}")]
public readonly struct StringSegment : IEquatable<StringSegment>, IComparable<StringSegment>, IComparable, IReadOnlyList<char>
{
    public StringSegment(string? text)
    {
        this.fullText = text;
        this.Offset = 0;
        this.Count = text?.Length ?? 0;
    }

    public StringSegment(string text, int offset)
    {
        text.ThrowIfNull(text);
        this.fullText = text;
        this.Offset = offset;
        this.Count = text.Length - offset;
    }
    public StringSegment(string text, int offset, int count)
    {
        text.ThrowIfNull(text);
        if ((uint)offset > (uint)text.Length || (uint)count > (uint)(text.Length - offset))
            throw new ArgumentException();
        this.fullText = text;
        this.Offset = offset;
        this.Count = count;
    }
    private string DebuggerDisplay => this.ToString();
    private readonly string? fullText;
    private string FullText => this.fullText ?? string.Empty;
    public int Count { get; }
    public int Length => this.Count;
    public int Offset { get; }
    public ReadOnlySpan<char> AsSpan() => this.fullText.AsSpan().Slice(this.Offset, this.Count);

    public static StringSegment Empty => default;
    public bool IsEmpty => this.Count is 0;
    
    public override string ToString() => this.FullText.Substring(this.Offset, this.Count);

    #region Enumeration

    public StringSegmentEnumerator GetEnumerator() => new (this);
    IEnumerator IEnumerable.GetEnumerator() => new StringSegmentEnumerator(this);
    IEnumerator<char> IEnumerable<char>.GetEnumerator() => new StringSegmentEnumerator(this);


    #endregion Enumeration
    
    #region Slicing and Indexing

    public StringSegment this[Range range] => this.Slice(range);

    private StringSegment Slice(Range range)
    {
        var (offset, length) = range.GetOffsetAndLength(this.Length);
        return Slice(offset, length);
    }
    
    public char this[int index] 
        => (uint)index >= (uint)this.Count
            ? throw new ArgumentOutOfRangeException(nameof(index), index, default)
            : this.FullText[this.Offset + index];
    
    public StringSegment Slice(int index) 
        => (uint)index > (uint)this.Count
            ? throw new ArgumentOutOfRangeException(nameof(index), index, default)
            : new StringSegment(this.FullText, this.Offset + index, this.Count - index);

    public StringSegment Slice(int index, int count) 
        => (uint)index > (uint)this.Count || (uint)count > (uint)(this.Count - index)
            ? throw new ArgumentOutOfRangeException()
            : new StringSegment(this.FullText, this.Offset + index, count);

    #endregion Slicing and Indexing

    #region Conversion
    
    public static implicit operator StringSegment(string? text) => new(text);
    public static implicit operator ReadOnlySpan<char>(StringSegment text) => text.AsSpan();

    #endregion Conversion
    
    #region Equality

    
    public bool Equals(ArraySegment<char> other, StringComparison comparison = StringComparison.Ordinal) 
        => this.AsSpan().Equals(other.AsSpan(), comparison);
    public bool Equals(char[] other, StringComparison comparison = StringComparison.Ordinal) 
        => this.AsSpan().Equals(other.AsSpan(), comparison);
    public bool Equals(ReadOnlySpan<char> other, StringComparison comparison = StringComparison.Ordinal)
        => this.AsSpan().Equals(other, comparison);
    
    public bool Equals(StringSegment other, StringComparison comparison) 
        => this.AsSpan().Equals(other.AsSpan(), comparison);
    public bool Equals(StringSegment other) 
        => this.Equals(other, StringComparison.Ordinal);

    public bool Equals(string? other, StringComparison comparison = StringComparison.Ordinal)
    {
        if (string.IsNullOrEmpty(other))
            return this.IsEmpty;
#if NETSTANDARD2_0
        other ??= string.Empty;
#endif
        if (this.IsEmpty)
            return false;
        if (
            comparison is StringComparison.Ordinal 
            && ReferenceEquals(this.fullText, other)
            && this.Offset == 0
            && this.Length == other.Length
        )
        {
            return true;
        }
        return this.AsSpan().Equals(other.AsSpan(), comparison);
    }


    public override bool Equals(object? obj) => obj switch
    {
        StringSegment other => this.Equals(other),
        char[] other => this.Equals(other),
        ArraySegment<char> other => this.Equals(other),
        string other => this.Equals(other),
        _ => false,
    };

#if NETCOREAPP3_0_OR_GREATER
    public int GetHashCode(StringComparison comparison) 
        => string.GetHashCode(this.AsSpan(), comparison);
    public override int GetHashCode() => this.GetHashCode(StringComparison.Ordinal);
#else
    public override int GetHashCode()
    {
        var hc = new HashCode();
        hc.Add(this.Length);
        foreach (var ch in this)
            hc.Add(ch);
        return hc.ToHashCode();
    }
#endif
    

    public static bool operator ==(StringSegment left, StringSegment right) => left.Equals(right);
    public static bool operator !=(StringSegment left, StringSegment right) => left.Equals(right) is false;
    
    public static bool operator ==(StringSegment left, string right) => left.Equals(right);
    public static bool operator !=(StringSegment left, string right) => left.Equals(right) is false;
    public static bool operator ==(string left, StringSegment right) => right.Equals(left);
    public static bool operator !=(string left, StringSegment right) => right.Equals(left) is false;
    
    public static bool operator ==(StringSegment left, ReadOnlySpan<char> right) => left.Equals(right);
    public static bool operator !=(StringSegment left, ReadOnlySpan<char> right) => left.Equals(right) is false;
    public static bool operator ==(ReadOnlySpan<char> left, StringSegment right) => right.Equals(left);
    public static bool operator !=(ReadOnlySpan<char> left, StringSegment right) => right.Equals(left) is false;
    
    public static bool operator ==(StringSegment left, ArraySegment<char> right) => left.Equals(right);
    public static bool operator !=(StringSegment left, ArraySegment<char> right) => left.Equals(right) is false;
    public static bool operator ==(ArraySegment<char> left, StringSegment right) => right.Equals(left);
    public static bool operator !=(ArraySegment<char> left, StringSegment right) => right.Equals(left) is false;


    #endregion Equality


    #region Comparison

    public static int Compare(StringSegment strA, StringSegment strB)
    {
        var length = Math.Min(strA.Length, strB.Length);
        return Compare(strA, strB, length) is not 0 and var result
            ? result
            : strA.Length.CompareTo(strB.Length);
    }
    
    public static int Compare(
        StringSegment strA,
        StringSegment strB,
        int length
    ) => string.Compare(
        strA.fullText, 
        strA.Offset,
        strB.fullText, 
        strB.Offset,
        length
    );
    
    public static int Compare(
        StringSegment strA, 
        StringSegment strB,
        bool ignoreCase
    )
    {
        var length = Math.Min(strA.Length, strB.Length);
        return Compare(strA, strB, length, ignoreCase) is not 0 and var result
            ? result
            : strA.Length.CompareTo(strB.Length);
    }
    
    public static int Compare(
        StringSegment strA,
        StringSegment strB, 
        int length, 
        bool ignoreCase
    ) => string.Compare(
        strA.fullText, 
        strA.Offset,
        strB.fullText, 
        strB.Offset,
        length, 
        ignoreCase
    );
    
    public static int Compare(
        StringSegment strA, 
        StringSegment strB, 
        StringComparison comparisonType
    )
    {
        var length = Math.Min(strA.Length, strB.Length);
        return Compare(strA, strB, length, comparisonType) is not 0 and var result
            ? result
            : strA.Length.CompareTo(strB.Length);
    }
    
    public static int Compare(
        StringSegment strA,
        StringSegment strB,
        int length, 
        StringComparison comparisonType
    ) => string.Compare(
        strA.fullText, 
        strA.Offset,
        strB.fullText, 
        strB.Offset,
        length, 
        comparisonType
    );
    
    
    public static int Compare (
        StringSegment strA, 
        StringSegment strB,
        bool ignoreCase, 
        CultureInfo? culture
    )
    {
        var length = Math.Min(strA.Length, strB.Length);
        return Compare(strA, strB, length, ignoreCase, culture) is not 0 and var result
            ? result
            : strA.Length.CompareTo(strB.Length);
    }
    
    public static int Compare(
        StringSegment strA, 
        StringSegment strB,
        int length, 
        bool ignoreCase,
        CultureInfo? culture
    ) => string.Compare(
        strA.fullText, 
        strA.Offset,
        strB.fullText, 
        strB.Offset,
        length, 
        ignoreCase, 
        culture
    );
    
    
    public static int Compare(
        StringSegment strA,
        StringSegment strB,
        CultureInfo? culture,
        CompareOptions options
    )
    {
        var length = Math.Min(strA.Length, strB.Length);
        return Compare(strA, strB, length, culture, options) is not 0 and var result
            ? result
            : strA.Length.CompareTo(strB.Length);
    }
    
    
    public static int Compare(
        StringSegment strA, 
        StringSegment strB,
        int length, 
        CultureInfo? cultureInfo,
        CompareOptions compareOptions
    ) => string.Compare(
        strA.fullText, 
        strA.Offset,
        strB.fullText, 
        strB.Offset,
        length, 
        cultureInfo, 
        compareOptions
    );

    public int CompareTo(StringSegment other, StringComparison comparison)
        => Compare(this, other, comparison);
    public int CompareTo(string? other, StringComparison comparison)
        => Compare(this, other, comparison);
    public int CompareTo(StringSegment other)
        => Compare(this, other);
    public int CompareTo(string? other)
        => Compare(this, other);

    public int CompareTo(object? obj)
    {
        return obj switch
        {
            null => 1,
            string other => this.CompareTo(other),
            StringSegment other => this.CompareTo(other),
            _ => throw new ArgumentException($"Object must be of type {nameof(StringSegment)} or {nameof(String)}", nameof(obj)),
        };
    }

    public static bool operator <(StringSegment left, StringSegment right) => left.CompareTo(right) < 0;
    public static bool operator >(StringSegment left, StringSegment right) => left.CompareTo(right) > 0;
    public static bool operator <=(StringSegment left, StringSegment right) => left.CompareTo(right) <= 0;
    public static bool operator >=(StringSegment left, StringSegment right) => left.CompareTo(right) >= 0;
    
    public static bool operator <(StringSegment left, string right) => Compare(left, right) < 0;
    public static bool operator >(StringSegment left, string right) => Compare(left, right) > 0;
    public static bool operator <=(StringSegment left, string right) => Compare(left, right) <= 0;
    public static bool operator >=(StringSegment left, string right) => Compare(left, right) >= 0;
    
    public static bool operator <(string left, StringSegment right) => left.CompareTo(right) < 0;
    public static bool operator >(string left, StringSegment right) => left.CompareTo(right) > 0;
    public static bool operator <=(string left, StringSegment right) => left.CompareTo(right) <= 0;
    public static bool operator >=(string left, StringSegment right) => left.CompareTo(right) >= 0;

    #endregion Comparison
    

}

internal sealed class StringSegmentDebugView
{
    public StringSegmentDebugView(StringSegment segment)
    {
        this.Items = segment.AsSpan().ToArray();
    }
    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public char[] Items { get; }
}