using QGrid.NET.Enums;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace QGrid.NET.Serialization
{
    public class LogicalOperatorJsonConverter : JsonConverter<LogicalOperatorType>
    {
        public override LogicalOperatorType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string shortString = reader.GetString()!;
            return LogicalOperatorTypeParser.FromShortString(shortString);
        }

        public override void Write(Utf8JsonWriter writer, LogicalOperatorType value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(LogicalOperatorTypeParser.ToShortString(value));
        }
    }
}
