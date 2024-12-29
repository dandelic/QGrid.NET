using QGrid.NET.Enums;
using QGrid.NET.Exceptions;
using QGrid.NET.Serialization;
using System.Text.Json.Serialization;

namespace QGrid.NET.Builder.Models
{
    /// <summary>
    /// Represents a filter that is applied to a specific property in a query.
    /// </summary>
    public class QueryFilter
    {
        private string _propertyName = null!;
        private string _value = null!;
        private OperandType _filterOperand;
        private LogicalOperatorType _logicalOperator;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryFilter"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property for the filter to be applied on.</param>
        /// <param name="operandType">Filter type (e.g., Equals, GreaterThan).</param>
        /// <param name="value">Value to filter by.</param>
        /// <param name="logicalOperator">Logical operator (e.g., AND, OR).</param>
        public QueryFilter(string propertyName, OperandType operandType, string value,
            LogicalOperatorType logicalOperator = LogicalOperatorType.AND)
        {
            PropertyName = propertyName;
            OperandType = operandType;
            Value = value;
            LogicalOperator = logicalOperator;
        }

        /// <summary>
        /// Gets or sets the name of the property that the filter is applied to.
        /// </summary>
        /// <exception cref="QArgumentException">Thrown when the value is null or empty.</exception>
        [JsonPropertyName("property")]
        public string PropertyName
        {
            get => _propertyName;
            set
            {
                QArgumentException.ThrowIfNullOrEmpty(value);
                _propertyName = value;
            }
        }

        /// <summary>
        /// Gets or sets the operand type (e.g., Equals, GreaterThan).
        /// Defines the type of comparison to perform in the filter.
        /// </summary>
        /// <exception cref="QArgumentException">Thrown when the value is not of type.</exception>
        [JsonPropertyName("operand")]
        [JsonConverter(typeof(OperandJsonConverter))]
        public OperandType OperandType
        {
            get => _filterOperand;
            set
            {
                QArgumentException.ThrowIfInvalidEnum(value);
                _filterOperand = value;
            }
        }

        /// <summary>
        /// Gets or sets the logical operator (e.g., AND, OR) used to combine this filter with other filters.
        /// </summary>
        /// <exception cref="QArgumentException">Thrown when the value is null or empty.</exception>
        [JsonPropertyName("operator")]
        [JsonConverter(typeof(LogicalOperatorJsonConverter))]
        public LogicalOperatorType LogicalOperator
        {
            get => _logicalOperator;
            set
            {
                QArgumentException.ThrowIfInvalidEnum(value);
                _logicalOperator = value;
            }
        }

        /// <summary>
        /// Gets or sets the value to filter by.
        /// </summary>
        /// <exception cref="QArgumentException">Thrown when the value is null or empty.</exception>
        [JsonPropertyName("value")]
        public string Value
        {
            get => _value;
            set
            {
                QArgumentException.ThrowIfNullOrEmpty(value);
                _value = value;
            }
        }
    }
}
