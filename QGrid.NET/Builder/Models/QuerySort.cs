using QGrid.NET.Exceptions;
using System.Text.Json.Serialization;

namespace QGrid.NET.Builder.Models
{
    /// <summary>
    /// Represents a sorting model for a specific property.
    /// </summary>
    public class QuerySort
    {
        private string _propertyName = null!;
        private bool _ascending;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuerySort"/> class.
        /// </summary>
        /// <param name="propertyName">Property name to sort by.</param>
        /// <param name="ascending">Sort direction (ascending or descending).</param>
        public QuerySort(string propertyName, bool ascending = true)
        {
            PropertyName = propertyName;
            _ascending = ascending;
        }

        /// <summary>
        /// Property to sort by.
        /// </summary>
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
        /// Sort direction (Ascending or Descending). Default is Ascending.
        /// </summary>
        [JsonPropertyName("ascending")]
        public bool Ascending
        {
            get => _ascending;
            set
            {
                _ascending = value;
            }
        }
    }
}
