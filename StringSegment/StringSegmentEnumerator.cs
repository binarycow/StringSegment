using System.Collections;

namespace Binarycow.Text;

public struct StringSegmentEnumerator : IEnumerable<char>, IEnumerator<char>
{
    private readonly StringSegment segment;
    private int index;

    public StringSegmentEnumerator(StringSegment segment)
    {
        this.segment = segment;
        this.index = -1;
    }

    public bool MoveNext()
    {
        var nextIndex = this.index + 1;
        if (nextIndex >= this.segment.Count) 
            return false;
        this.index = nextIndex;
        return true;
    }

    public void Reset()
    {
        this.index = -1;
    }

    public char Current => this.segment[this.index];

    object IEnumerator.Current => this.Current;

    public void Dispose()
    {
    }
    public StringSegmentEnumerator GetEnumerator() => this;
    IEnumerator IEnumerable.GetEnumerator() => this;
    IEnumerator<char> IEnumerable<char>.GetEnumerator() => this;
}