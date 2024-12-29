using QGrid.NET.Enums;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace QGrid.NET.Serialization
{
    public class OperandJsonConverter : JsonConverter<OperandType>
    {
        public override OperandType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string shortString = reader.GetString()!;
            return OperandTypeParser.FromShortString(shortString.ToLower());
        }

        public override void Write(Utf8JsonWriter writer, OperandType value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(OperandTypeParser.ToShortString(value));
        }
    }
}
