using System.Buffers;
using System.Globalization;


namespace Binarycow.Text;

public static class StringSegmentExtensions
{
    public static StringSegment AsStringSegment(this string? text) => text;

    public static StringSegment AsStringSegment(this string? text, int start)
        => text is null ? default : new StringSegment(text, start, text.Length);

    public static StringSegment AsStringSegment(this string? text, int start, int length)
        => text is null ? default : new StringSegment(text, start, length);

    public static int BinarySearch<TComparable>(StringSegment segment, TComparable comparable)
        where TComparable : IComparable<char>
        => segment.AsSpan().BinarySearch(comparable);

    public static int BinarySearch<TComparer>(StringSegment segment, char value, TComparer comparer)
        where TComparer : IComparer<char>
        => segment.AsSpan().BinarySearch(value, comparer);

    public static int BinarySearch(StringSegment segment, IComparable<char> comparable)
        => segment.AsSpan().BinarySearch(comparable);
    

    public static bool Contains(StringSegment segment, ReadOnlySpan<char> value, StringComparison comparison) 
        => segment.AsSpan().Contains(value, comparison);
    public static bool Contains(StringSegment segment, StringSegment value, StringComparison comparison) 
        => segment.AsSpan().Contains(value, comparison);


    public static bool EndsWith(StringSegment segment, ReadOnlySpan<char> value, StringComparison comparison) 
        => segment.AsSpan().EndsWith(value, comparison);

    public static bool EndsWith(StringSegment segment, ReadOnlySpan<char> value)
        => segment.AsSpan().EndsWith(value);
    public static bool EndsWith(StringSegment segment, StringSegment value, StringComparison comparison)
        => segment.AsSpan().EndsWith(value, comparison);

    public static bool EndsWith(StringSegment segment, StringSegment value)
        => segment.AsSpan().EndsWith(value);



    public static int IndexOf(this StringSegment segment, ReadOnlySpan<char> value, StringComparison comparison) 
        => segment.AsSpan().IndexOf(value, comparison);
    public static int IndexOf(this StringSegment segment, StringSegment value, StringComparison comparison) 
        => segment.AsSpan().IndexOf(value, comparison);

    public static int IndexOf(this StringSegment segment, ReadOnlySpan<char> value)
        => segment.AsSpan().IndexOf(value);
    public static int IndexOf(this StringSegment segment, StringSegment value)
        => segment.AsSpan().IndexOf(value);

    public static int IndexOf(this StringSegment segment, char value)
        => segment.AsSpan().IndexOf(value);

    public static int IndexOfAny(this StringSegment segment, ReadOnlySpan<char> values)
        => segment.AsSpan().IndexOfAny(values);
    public static int IndexOfAny(this StringSegment segment, StringSegment values)
        => segment.AsSpan().IndexOfAny(values);

    public static int IndexOfAny(this StringSegment segment, char value0, char value1)
        => segment.AsSpan().IndexOfAny(value0, value1);

    public static int IndexOfAny(this StringSegment segment, char value0, char value1, char value2)
        => segment.AsSpan().IndexOfAny(value0, value1, value2);

    public static bool IsWhiteSpace(this StringSegment segment)
        => segment.AsSpan().IsWhiteSpace();


    public static int LastIndexOf(this StringSegment segment, StringSegment value)
        => segment.AsSpan().LastIndexOf(value);
    public static int LastIndexOf(this StringSegment segment, ReadOnlySpan<char> value)
        => segment.AsSpan().LastIndexOf(value);

    public static int LastIndexOf(this StringSegment segment, char value)
        => segment.AsSpan().LastIndexOf(value);

    public static int LastIndexOfAny(this StringSegment segment, StringSegment values)
        => segment.AsSpan().LastIndexOfAny(values);
    public static int LastIndexOfAny(this StringSegment segment, ReadOnlySpan<char> values)
        => segment.AsSpan().LastIndexOfAny(values);

    public static int LastIndexOfAny(this StringSegment segment, char value0, char value1)
        => segment.AsSpan().LastIndexOfAny(value0, value1);

    public static int LastIndexOfAny(this StringSegment segment, char value0, char value1, char value2)
        => segment.AsSpan().LastIndexOfAny(value0, value1, value2);


    public static bool Overlaps(this StringSegment segment, StringSegment other)
        => segment.AsSpan().Overlaps(other);
    public static bool Overlaps(this StringSegment segment, ReadOnlySpan<char> other)
        => segment.AsSpan().Overlaps(other);

    public static bool Overlaps(this StringSegment segment, StringSegment other, out int elementOffset)
        => segment.AsSpan().Overlaps(other, out elementOffset);
    public static bool Overlaps(this StringSegment segment, ReadOnlySpan<char> other, out int elementOffset)
        => segment.AsSpan().Overlaps(other, out elementOffset);


    public static bool StartsWith(this StringSegment segment, StringSegment value, StringComparison comparison)
        => segment.AsSpan().StartsWith(value, comparison);
    public static bool StartsWith(this StringSegment segment, ReadOnlySpan<char> value, StringComparison comparison)
        => segment.AsSpan().StartsWith(value, comparison);

    public static bool StartsWith(this StringSegment segment, StringSegment value)
        => segment.AsSpan().StartsWith(value);
    public static bool StartsWith(this StringSegment segment, ReadOnlySpan<char> value)
        => segment.AsSpan().StartsWith(value);

    public static int ToLower(this StringSegment segment, Span<char> destination, CultureInfo? culture)
        => segment.AsSpan().ToLower(destination, culture);

    public static int ToLowerInvariant(this StringSegment segment, Span<char> destination)
        => segment.AsSpan().ToLowerInvariant(destination);

    public static int ToUpper(this StringSegment segment, Span<char> destination, CultureInfo? culture)
        => segment.AsSpan().ToUpper(destination, culture);

    public static int ToUpperInvariant(this StringSegment segment, Span<char> destination)
        => segment.AsSpan().ToUpperInvariant(destination);

    


    public static StringSegment TrimEnd(this StringSegment segment) 
        => segment[..^(segment.Length - segment.AsSpan().TrimEnd().Length)];

    public static StringSegment TrimEnd(this StringSegment segment, char trimElement)
        => segment[..^(segment.Length - segment.AsSpan().TrimEnd(trimElement).Length)];

    public static StringSegment TrimEnd(this StringSegment segment, StringSegment trimElements)
        => segment[..^(segment.Length - segment.AsSpan().TrimEnd(trimElements).Length)];

    public static StringSegment TrimEnd(this StringSegment segment, ReadOnlySpan<char> trimElements)
        => segment[..^(segment.Length - segment.AsSpan().TrimEnd(trimElements).Length)];


    public static StringSegment TrimStart(this StringSegment segment)
        => segment[..^(segment.Length - segment.AsSpan().TrimStart().Length)];

    public static StringSegment TrimStart(this StringSegment segment, char trimElement)
        => segment[..^(segment.Length - segment.AsSpan().TrimStart(trimElement).Length)];

    public static StringSegment TrimStart(this StringSegment segment, ReadOnlySpan<char> trimElements)
        => segment[..^(segment.Length - segment.AsSpan().TrimStart(trimElements).Length)];
    public static StringSegment TrimStart(this StringSegment segment, StringSegment trimElements)
        => segment[..^(segment.Length - segment.AsSpan().TrimStart(trimElements).Length)];


    
    
    public static StringSegment Trim(this StringSegment segment)
        => segment.TrimStart().TrimEnd();

    public static StringSegment Trim(this StringSegment segment, char trimElement)
        => segment.TrimStart(trimElement).TrimEnd(trimElement);

    public static StringSegment Trim(this StringSegment segment, ReadOnlySpan<char> trimElements)
        => segment.TrimStart(trimElements).TrimEnd(trimElements);

    public static StringSegment Trim(this StringSegment segment, StringSegment trimElements)
        => segment.TrimStart(trimElements).TrimEnd(trimElements);
    
    public static StringSegmentLineEnumerator EnumerateLines(this StringSegment segment) => new (segment);

    
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_0_OR_GREATER
    public static string ToLower(this StringSegment segment, CultureInfo? culture) => string.Create(
        segment.Length,
        (Segment: segment, Culture: culture),
        static (span, args) => args.Segment.AsSpan().ToLower(span, args.Culture)
    );

    public static string ToLowerInvariant(this StringSegment segment) => string.Create(
        segment.Length,
        segment,
        static (span, segment) => segment.AsSpan().ToLowerInvariant(span)
    );

    public static string ToUpper(this StringSegment segment, CultureInfo? culture) => string.Create(
        segment.Length,
        (Segment: segment, Culture: culture),
        static (span, args) => args.Segment.AsSpan().ToUpper(span, args.Culture)
    );

    public static string ToUpperInvariant(this StringSegment segment) => string.Create(
        segment.Length,
        segment,
        static (span, segment) => segment.AsSpan().ToUpperInvariant(span)
    );
#else
    public static string ToLower(this StringSegment segment, CultureInfo? culture)
        => segment.ToString().ToLower(culture!);

    public static string ToLowerInvariant(this StringSegment segment)
        => segment.ToString().ToLowerInvariant();

    public static string ToUpper(this StringSegment segment, CultureInfo? culture)
        => segment.ToString().ToUpper(culture!);

    public static string ToUpperInvariant(this StringSegment segment)
        => segment.ToString().ToUpperInvariant();
#endif
    
#if NETCOREAPP3_0_OR_GREATER
    public static bool Contains(StringSegment segment, char value)
        => segment.AsSpan().Contains(value);
    public static int LastIndexOf(this StringSegment segment, StringSegment value, StringComparison comparison)
        => segment.AsSpan().LastIndexOf(value, comparison);
    public static int LastIndexOf(this StringSegment segment, ReadOnlySpan<char> value, StringComparison comparison)
        => segment.AsSpan().LastIndexOf(value, comparison);
    public static StringSegmentRuneEnumerator EnumerateRunes(this StringSegment segment) => new(segment);
#endif
    
#if NET7_0_OR_GREATER
    
    public static int CommonPrefixLength(StringSegment segment, StringSegment other) 
        => segment.AsSpan().CommonPrefixLength(other);
    public static int CommonPrefixLength(StringSegment segment, StringSegment other, IEqualityComparer<char>? comparer) 
        => segment.AsSpan().CommonPrefixLength(other, comparer);
    
    public static int CommonPrefixLength(StringSegment segment, ReadOnlySpan<char> other) 
        => segment.AsSpan().CommonPrefixLength(other);
    public static int CommonPrefixLength(StringSegment segment, ReadOnlySpan<char> other, IEqualityComparer<char>? comparer) 
        => segment.AsSpan().CommonPrefixLength(other, comparer);

    public static int IndexOfAnyExcept(this StringSegment segment, StringSegment values)
        => segment.AsSpan().IndexOfAnyExcept(values);
    public static int IndexOfAnyExcept(this StringSegment segment, ReadOnlySpan<char> values)
        => segment.AsSpan().IndexOfAnyExcept(values);

    public static int IndexOfAnyExcept(this StringSegment segment, char value)
        => segment.AsSpan().IndexOfAnyExcept(value);

    public static int IndexOfAnyExcept(this StringSegment segment, char value0, char value1)
        => segment.AsSpan().IndexOfAnyExcept(value0, value1);

    public static int IndexOfAnyExcept(this StringSegment segment, char value0, char value1, char value2)
        => segment.AsSpan().IndexOfAnyExcept(value0, value1, value2);
    
    
    public static int LastIndexOfAnyExcept(this StringSegment segment, StringSegment values)
        => segment.AsSpan().LastIndexOfAnyExcept(values);
    public static int LastIndexOfAnyExcept(this StringSegment segment, ReadOnlySpan<char> values)
        => segment.AsSpan().LastIndexOfAnyExcept(values);

    public static int LastIndexOfAnyExcept(this StringSegment segment, char value)
        => segment.AsSpan().LastIndexOfAnyExcept(value);

    public static int LastIndexOfAnyExcept(this StringSegment segment, char value0, char value1)
        => segment.AsSpan().LastIndexOfAnyExcept(value0, value1);

    public static int LastIndexOfAnyExcept(this StringSegment segment, char value0, char value1, char value2)
        => segment.AsSpan().LastIndexOfAnyExcept(value0, value1, value2);

#endif
    
#if NET8_0_OR_GREATER
    public static int IndexOfAnyExcept(this StringSegment segment, IndexOfAnyValues<char> values)
        => segment.AsSpan().IndexOfAnyExcept(values);
    public static int LastIndexOfAnyExcept(this StringSegment segment, IndexOfAnyValues<char> values)
        => segment.AsSpan().LastIndexOfAnyExcept(values);
    public static int IndexOfAny(this StringSegment segment, IndexOfAnyValues<char> values)
        => segment.AsSpan().IndexOfAny(values);
    public static int LastIndexOfAny(this StringSegment segment, IndexOfAnyValues<char> values)
        => segment.AsSpan().LastIndexOfAny(values);
    public static int IndexOfAnyInRange(this StringSegment segment, char lowInclusive, char highInclusive)
        => segment.AsSpan().IndexOfAnyInRange(lowInclusive, highInclusive);
    public static int LastIndexOfAnyInRange(this StringSegment segment, char lowInclusive, char highInclusive)
        => segment.AsSpan().LastIndexOfAnyInRange(lowInclusive, highInclusive);
    public static int IndexOfAnyExceptInRange(this StringSegment segment, char lowInclusive, char highInclusive)
        => segment.AsSpan().IndexOfAnyExceptInRange(lowInclusive, highInclusive);
    public static int LastIndexOfAnyExceptInRange(this StringSegment segment, char lowInclusive, char highInclusive)
        => segment.AsSpan().LastIndexOfAnyExceptInRange(lowInclusive, highInclusive);
#endif
}