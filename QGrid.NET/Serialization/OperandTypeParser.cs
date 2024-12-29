using QGrid.NET.Enums;
using QGrid.NET.Exceptions;

namespace QGrid.NET.Serialization
{
    public static class OperandTypeParser
    {
        private static readonly Dictionary<OperandType, string> OperandToString = new()
        {
            { OperandType.Equals, "eq" },
            { OperandType.NotEquals, "neq" },
            { OperandType.GreaterThan, "gt" },
            { OperandType.GreaterThanOrEqual, "gte" },
            { OperandType.LessThan, "lt" },
            { OperandType.LessThanOrEqual, "lte" },
            { OperandType.Contains, "cn" },
            { OperandType.StartsWith, "sw" },
            { OperandType.EndsWith, "ew" }
        };

        private static readonly Dictionary<string, OperandType> StringToOperand = [];
        static OperandTypeParser()
        {
            foreach (var kvp in OperandToString)
            {
                StringToOperand[kvp.Value] = kvp.Key;
            }
        }

        public static string ToShortString(OperandType operand) =>
            OperandToString.TryGetValue(operand, out var result) ? result : throw new QSerializationException("Invalid operand");

        public static OperandType FromShortString(string shortString) =>
            StringToOperand.TryGetValue(shortString.ToLower(), out var result) ? result : throw new QSerializationException("Invalid short string for operand");
    }

}
