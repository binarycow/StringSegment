#if NETCOREAPP3_0_OR_GREATER

using System.Collections;
using System.Diagnostics;
using System.Text;

namespace Binarycow.Text;

public class StringSegmentRuneEnumerator : IEnumerator<Rune>, IEnumerable<Rune>
{
    private StringSegment remaining;
    public StringSegmentRuneEnumerator(StringSegment segment)
    {
        this.remaining = segment;
        this.Current = default;
    }
    
    public bool MoveNext()
    {
        if (this.remaining.IsEmpty)
        {
            // reached the end of the buffer
            this.Current = default;
            return false;
        }

        var runeEnumerator = this.remaining.AsSpan().EnumerateRunes();
        var success = runeEnumerator.MoveNext();
        Debug.Assert(success, $"{nameof(SpanRuneEnumerator)}.{nameof(this.MoveNext)} should have returned true for a non-empty input");
        this.Current = runeEnumerator.Current;
        remaining = remaining[this.Current.Utf16SequenceLength..];
        return true;
    }
    public Rune Current { get; private set; }
    object IEnumerator.Current => this.Current;

    public void Dispose()
    {
    }
    public StringSegmentRuneEnumerator GetEnumerator() => this;
    IEnumerator IEnumerable.GetEnumerator() => this;
    IEnumerator<Rune> IEnumerable<Rune>.GetEnumerator() => this;
    void IEnumerator.Reset() => throw new NotSupportedException();
}

#endif