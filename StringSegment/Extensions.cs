using System.Runtime.CompilerServices;

namespace Binarycow.Text;

internal static class Extensions
{
    public static void ThrowIfNull<T>(
        this T value
        , [CallerArgumentExpression(nameof(value))] string argumentName = ""
    )
    {
        #if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(value, argumentName);
        #else
        if (value is null)
            throw new ArgumentNullException(argumentName);
        #endif
    }
}