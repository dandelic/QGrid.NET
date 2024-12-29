using QGrid.NET.Exceptions;
using System.Text.Json.Serialization;

namespace QGrid.NET.Serialization.Models
{
    /// <summary>
    /// Represents a paginated response containing a collection of data and pagination details.
    /// </summary>
    /// <typeparam name="T">The type of the items in the data collection.</typeparam>
    public class QPagedResponse<T>
    {
        private IEnumerable<T> _data = default!;
        private QPaginationInfo _paginationInfo = null!;

        /// <summary>
        /// Initializes a new instance of the <see cref="QPagedResponse{T}"/> class with the specified data and pagination information.
        /// </summary>
        /// <param name="data">The collection of data items.</param>
        /// <param name="paginationInfo">The pagination details.</param>
        public QPagedResponse(IEnumerable<T> data, QPaginationInfo paginationInfo)
        {
            Data = data;
            Pagination = paginationInfo;
        }

        /// <summary>
        /// Gets or sets the pagination information for the response.
        /// </summary>
        /// <exception cref="QArgumentException">Thrown when the assigned value is null.</exception>
        [JsonPropertyName("pagination")]
        public QPaginationInfo Pagination
        {
            get => _paginationInfo;
            set
            {
                QArgumentException.ThrowIfNull(value);
                _paginationInfo = value;
            }
        }

        /// <summary>
        /// Gets or sets the collection of data items in the response.
        /// </summary>
        /// <exception cref="QArgumentException">Thrown when the assigned value is null.</exception>
        [JsonPropertyName("data")]
        public IEnumerable<T> Data
        {
            get => _data;
            set
            {
                QArgumentException.ThrowIfNull(value);
                _data = value;
            }
        }
    }
}
