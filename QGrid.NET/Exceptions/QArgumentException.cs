using System.Runtime.CompilerServices;

namespace QGrid.NET.Exceptions
{
    public class QArgumentException : ArgumentException
    {
        public QArgumentException() { }
        public QArgumentException(string message) : base(message) { }
        public QArgumentException(string message, Exception inner) : base(message, inner) { }

        new internal static void ThrowIfNullOrEmpty(string argument, [CallerArgumentExpression(nameof(argument))] string? argumentName = null)
        {
            if (string.IsNullOrEmpty(argument))
                throw new QArgumentException($"{argumentName ?? "Value"} cannot be null or empty.");
        }

        internal static void ThrowIfNull(object argument, [CallerArgumentExpression(nameof(argument))] string? argumentName = null)
        {
            if (argument == null)           
                throw new QArgumentException($"{argumentName ?? "Value"} cannot be null.");        
        }

        internal static void ThrowIfInvalidEnum<TEnum>(TEnum value, string? argumentName = null) where TEnum : struct, Enum
        {
            if (!Enum.IsDefined(typeof(TEnum), value))
                throw new QArgumentException(argumentName is null ? $"The value '{value}' is not a valid {typeof(TEnum).Name}."
                    : string.Format("{0}: The value '{1}' is invalid for enum {2}.", argumentName, value, typeof(TEnum).Name));
        }

        internal static QArgumentException ThrowIfInvalidType(Type type, string value) => new($"Value '{value}' cannot be parsed to type {type.Name}.");
    }
}
