using QGrid.NET.Exceptions;
using System.Text.Json.Serialization;

namespace QGrid.NET.Serialization.Models
{
    /// <summary>
    /// Represents pagination information for a dataset, including the current page, page size,
    /// total item count, and total number of pages.
    /// </summary>
    public class QPaginationInfo
    {
        private int _currentPage;
        private int _pageSize;
        private int _totalCount;
        private int _totalPages;

        /// <summary>
        /// Initializes a new instance of the <see cref="QPaginationInfo"/> class with the specified
        /// current page, page size, and total item count.
        /// </summary>
        /// <param name="currentPage">The current page number (0-based).</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <param name="totalCount">The total number of items in the dataset.</param>
        public QPaginationInfo(int currentPage, int pageSize, int totalCount)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = totalCount == 0 ? 0 : (int)Math.Ceiling((double)totalCount / pageSize);
        }

        /// <summary>
        /// Gets or sets the current page number (0-based).
        /// If the value is negative, it defaults to 0.
        /// </summary>
        [JsonPropertyName("currentPage")]
        public int CurrentPage
        {
            get => _currentPage;
            private set
            {
                if (value < 0)
                {
                    _currentPage = 0;
                    return;
                }
                _currentPage = value;
            }
        }

        /// <summary>
        /// Gets or sets the number of items per page.
        /// Throws an exception if the value is negative.
        /// </summary>
        [JsonPropertyName("pageSize")]
        public int PageSize
        {
            get => _pageSize;
            private set
            {
                if (value < 0)
                    throw new QArgumentException("Page size cannot be negative.");
                _pageSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the total number of items in the dataset.
        /// Throws an exception if the value is negative.
        /// </summary>
        [JsonPropertyName("totalCount")]
        public int TotalCount
        {
            get => _totalCount;
            private set
            {
                if (value < 0)
                    throw new QArgumentException("Total count cannot be negative.");
                _totalCount = value;
            }
        }

        /// <summary>
        /// Gets or sets the total number of pages, based on the total item count and page size.
        /// Throws an exception if the value is negative.
        /// </summary>
        [JsonPropertyName("totalPages")]
        public int TotalPages
        {
            get => _totalPages;
            private set
            {
                if (value < 0)
                    throw new QArgumentException("Total pages cannot be negative.");
                _totalPages = value;
            }
        }
    }
}
