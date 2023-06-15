using System.Collections;

namespace Binarycow.Text;

public struct StringSegmentLineEnumerator : IEnumerator<StringSegment>, IEnumerable<StringSegment>
{
    
    private StringSegment remaining;
    private bool isActive;

    public StringSegmentLineEnumerator(StringSegment segment)
    {
        this.remaining = segment;
        this.Current = default;
        this.isActive = true;
    }
    
    
    public bool MoveNext()
    {
        if (!this.isActive)
        {
            return false;
        }
        var (idx, stride) = IndexOfNewlineChar(this.remaining);
        if (idx >= 0)
        {
            this.Current = this.remaining.Slice(0, idx);
            this.remaining = this.remaining.Slice(idx + stride);
        }
        else
        {
            this.Current = this.remaining;
            this.remaining = default;
            this.isActive = false;
        }
        return true;
    }

    void IEnumerator.Reset() => throw new NotSupportedException();

    public StringSegment Current { get; private set; }

    object IEnumerator.Current => this.Current;

    public void Dispose()
    {
    }
    public StringSegmentLineEnumerator GetEnumerator() => this;
    IEnumerator IEnumerable.GetEnumerator() => this;
    IEnumerator<StringSegment> IEnumerable<StringSegment>.GetEnumerator() => this;

    private static (int Index, int Stride) IndexOfNewlineChar(ReadOnlySpan<char> text)
    {
#if NET8_0_OR_GREATER
        var idx = text.IndexOfAny(CharConstants.NewlineCharIndexOfAny);
#else
        var idx = text.IndexOfAny(CharConstants.NewlineChars);
#endif
        if ((uint)idx >= (uint)text.Length) 
            return (-1, default);
        text = text[idx..];
        return text is ['\r', '\n', ..] ? (idx, 2) : (idx, 1);
    }
}