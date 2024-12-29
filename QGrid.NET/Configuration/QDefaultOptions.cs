using QGrid.NET.Builder.Models;
using QGrid.NET.Exceptions;

namespace QGrid.NET.Configuration
{
    public class QDefaultOptions
    {
        private const int DEFAULT_PAGE = 1;
        private const int DEFAULT_ROWS = 10;
        private const int DEFAULT_ROWS_LIMIT = 50;

        private int _defaultPage = DEFAULT_PAGE;
        private int _defaultRows = DEFAULT_ROWS;
        private int _maxRows = DEFAULT_ROWS_LIMIT;

        /// <summary>
        /// List of default sort filters to be applied if none are specified by the user. 
        /// If empty, no sort is applied.
        /// </summary>
        public List<QuerySort> SortFilters { get; private set; } = [];

        /// <summary>
        /// Default page number for pagination.
        /// </summary>
        public int DefaultPage => _defaultPage;

        /// <summary>
        /// Default number of rows per page.
        /// </summary>
        public int DefaultRows => _defaultRows;

        /// <summary>
        /// Maximum allowed rows per page to prevent performance issues.
        /// </summary>
        public int MaxRows => _maxRows;

        #region Fluent Setter Methods

        /// <summary>
        /// Set the default page number.
        /// </summary>
        public QDefaultOptions SetDefaultPage(int page)
        {
            if (page <= 0) throw new QArgumentException("Page number must be greater than zero.");
            _defaultPage = page;
            return this;
        }

        /// <summary>
        /// Set the default number of rows per page.
        /// </summary>
        public QDefaultOptions SetDefaultRows(int rows)
        {
            if (rows <= 0) throw new QArgumentException("Rows per page must be greater than zero.");
            if (rows > _maxRows) throw new QArgumentException($"Rows cannot exceed maximum rows ({_maxRows}).");
            _defaultRows = rows;
            return this;
        }

        /// <summary>
        /// Set the maximum allowed rows per page.
        /// </summary>
        public QDefaultOptions SetMaxRows(int maxRows)
        {
            if (maxRows <= 0) throw new QArgumentException("Maximum rows must be greater than zero.");
            _maxRows = maxRows;
            return this;
        }

        /// <summary>
        /// Add a default sort filter.
        /// </summary>
        /// <param name="propertyName">Name of the property for the filter to be applied on.</param>
        /// <param name="ascending">If true sort is ascending; else descending</param>
        /// <returns></returns>
        public QDefaultOptions SetDefaultOrder(string propertyName, bool ascending)
        {
            SortFilters.Add(new QuerySort(propertyName, ascending));
            return this;
        }

        /// <summary>
        /// Set the list of default sort filters.
        /// </summary>
        public QDefaultOptions SetDefaultOrder(List<QuerySort> sortFilters)
        {
            if (sortFilters == null || sortFilters.Count == 0)
                throw new QArgumentException("Order filters list cannot be null or empty.");

            SortFilters = sortFilters;
            return this;
        }

        #endregion
    }

}
