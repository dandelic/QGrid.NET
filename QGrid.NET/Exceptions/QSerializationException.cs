using System.Runtime.Serialization;

namespace QGrid.NET.Exceptions
{
    internal class QSerializationException : SerializationException
    {
        public QSerializationException() { }
        public QSerializationException(string message) : base(message) { }
        public QSerializationException(string message, Exception inner) : base(message, inner) { }
    }
}
