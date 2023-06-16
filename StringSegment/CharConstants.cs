#if NET8_0_OR_GREATER
using System.Buffers;
#endif
namespace Binarycow.Text;

internal static class CharConstants
{
    public const string NewlineChars = "\r\n\f\u0085\u2028\u2029";
    
#if NET8_0_OR_GREATER
    public static SearchValues<char> NewlineCharSearchValues { get; } = SearchValues.Create(NewlineChars);
#endif
}