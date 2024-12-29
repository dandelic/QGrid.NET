using QGrid.NET.Configuration;
using System.Text.Json.Serialization;

namespace QGrid.NET.Builder.Models
{
    /// <summary>
    /// Represents pagination settings for queries, including the number of rows and the current page.
    /// </summary>
    public class QueryPagination
    {
        private readonly QDefaultOptions _options;
        private int _rows;
        private int _page;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryPagination"/> class.
        /// </summary>
        public QueryPagination()
        {
            _options = new QDefaultOptions();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryPagination"/> class.
        /// </summary>
        /// <param name="options">The options to configure default pagination settings.</param>
        public QueryPagination(QDefaultOptions? options = null)
        {
            _options = options ?? new QDefaultOptions();
            Page = _options.DefaultPage;
            Rows = _options.DefaultRows;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryPagination"/> class with specified rows and page.
        /// </summary>
        /// <param name="rows">The number of rows per page.</param>
        /// <param name="page">The current page number.</param>
        /// <param name="options">The options to configure pagination settings.</param>
        public QueryPagination(int rows, int page, QDefaultOptions? options = null)
        {
            _options = options ?? new QDefaultOptions();
            Page = page;
            Rows = rows;
        }

        /// <summary>
        /// Gets or sets the number of rows per page. The value is limited by the configuration options.
        /// </summary>
        [JsonPropertyName("rows")]
        public int Rows
        {
            get => _rows;
            set
            {
                if (value <= 0)
                    _rows = _options.DefaultRows;
                else if (value > _options.MaxRows)
                    _rows = _options.MaxRows;
                else
                    _rows = value;
            }
        }

        /// <summary>
        /// Gets or sets the current page number. The value must be at least 1.
        /// </summary>
        [JsonPropertyName("page")]
        public int Page
        {
            get => _page;
            set => _page = value < 1 ? _options.DefaultPage : value;
        }
    }
}
