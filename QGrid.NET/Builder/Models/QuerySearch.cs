using QGrid.NET.Enums;
using QGrid.NET.Exceptions;
using QGrid.NET.Serialization;
using System.Text.Json.Serialization;

namespace QGrid.NET.Builder.Models
{
    /// <summary>
    /// Represents the configuration and parameters for performing a global search.
    /// This filter allows specifying a search term, logical operator, and the properties to search within.
    /// </summary>
    public class QuerySearch
    {
        private readonly List<string> _properties = [];
        private string _searchTerm = null!;
        private LogicalOperatorType _logicalOperator;
        private OperandType _operandType;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuerySearch"/> class.
        /// </summary>
        /// <param name="searchTerm">The search term to apply.</param>
        /// <param name="properties">List of property names for search to be applied on.</param>
        /// <param name="operandType">The operand to use for search (e.g., Contains, StartsWith).</param>
        /// <param name="logicalOperator">The logical operator to combine conditions (AND/OR).</param>
        public QuerySearch(string searchTerm, List<string> properties, OperandType operandType = OperandType.Contains,
            LogicalOperatorType logicalOperator = LogicalOperatorType.OR)
        {
            SearchTerm = searchTerm;
            LogicalOperator = logicalOperator;
            OperandType = operandType;
            _properties = properties ?? [];
        }

        /// <summary>
        /// The search term to look for within the specified properties.
        /// </summary>
        /// <exception cref="QArgumentException">Thrown when the search term is null or whitespace.</exception>
        [JsonPropertyName("term")]
        public string SearchTerm
        {
            get => _searchTerm;
            set
            {       
                QArgumentException.ThrowIfNullOrEmpty(value);
                _searchTerm = value;
            }
        }

        /// <summary>
        /// The logical operator (AND/OR) used to combine conditions across properties.
        /// </summary>
        /// <exception cref="QArgumentException">Thrown when the value is not of type.</exception>
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
        /// Search operand type (e.g., Contains, StartsWith).
        /// </summary>
        /// <exception cref="QArgumentException">Thrown when the value is not of type.</exception>
        [JsonPropertyName("operand")]
        [JsonConverter(typeof(OperandJsonConverter))]
        public OperandType OperandType
        {
            get => _operandType;
            set
            {
                QArgumentException.ThrowIfInvalidEnum(value);
                _operandType = value;
            }
        }

        /// <summary>
        /// Gets the list of properties to search within. This collection is read-only.
        /// </summary>
        [JsonPropertyName("properties")]
        public IReadOnlyCollection<string> Properties => _properties.AsReadOnly();

        #region Methods

        /// <summary>
        /// Adds a property to the list of properties to search within.
        /// </summary>
        /// <param name="property">The name of the property to add.</param>
        /// <returns>The current instance of <see cref="QuerySearch"/> for chaining.</returns>
        /// <exception cref="QArgumentException">Thrown when the property name is null or whitespace.</exception>
        public QuerySearch AddProperty(string property)
        {
            QArgumentException.ThrowIfNullOrEmpty(property);
            _properties.Add(property);
            return this;
        }

        /// <summary>
        /// Adds multiple properties to the list of properties to search within.
        /// </summary>
        /// <param name="properties">The properties to add.</param>
        /// <returns>The current instance of <see cref="QuerySearch"/> for chaining.</returns>
        /// <exception cref="QEvaluatorException">Thrown when the properties collection is null.</exception>
        public QuerySearch AddProperties(IEnumerable<string> properties)
        {
            QArgumentException.ThrowIfNull(properties);

            foreach (var property in properties)
            {
                AddProperty(property);
            }

            return this;
        }

        /// <summary>
        /// Clears all properties from the list of properties to search within.
        /// </summary>
        public void ClearProperties()
        {
            _properties.Clear();
        }

        #endregion
    }
}
