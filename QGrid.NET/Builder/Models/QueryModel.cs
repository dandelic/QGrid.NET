using QGrid.NET.Configuration;
using QGrid.NET.Exceptions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace QGrid.NET.Builder.Models
{
    /// <summary>
    /// Represents a query model that encapsulates various filters, sorting, pagination, and search options.
    /// </summary>
    public class QueryModel
    {
        // Internal collections for property filters and sort filters
        private readonly List<QueryFilter> _propertyFilters = [];
        private readonly List<QuerySort> _sortFilters = [];

        /// <summary>
        /// Gets or sets the pagination filter, which defines the pagination options for the query.
        /// </summary>
        [JsonPropertyName("pagination")]
        public QueryPagination? PaginateFilter { get; set; }

        /// <summary>
        /// Gets or sets the search filter, which defines the search criteria for the query.
        /// </summary>
        [JsonPropertyName("search")]
        public QuerySearch? SearchFilter { get; set; }

        /// <summary>
        /// Gets or sets the property filters. These filters are applied to the properties of the data being queried.
        /// </summary>
        [JsonPropertyName("filters")]
        public List<QueryFilter> PropertyFilters
        {
            get => _propertyFilters;
            set
            {
                _propertyFilters.Clear();
                _propertyFilters.AddRange(value ?? []);
            }
        }

        /// <summary>
        /// Gets or sets the sort filters, which define how the data should be sorted.
        /// </summary>
        [JsonPropertyName("sort")]
        public List<QuerySort> SortFilters
        {
            get => _sortFilters;
            set
            {
                _sortFilters.Clear();
                _sortFilters.AddRange(value ?? []);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryModel"/> class.
        /// </summary>
        public QueryModel() {
            MapOptions(new QDefaultOptions());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryModel"/> class.
        /// Optionally accepts default options to pre-configure the query model.
        /// </summary>
        /// <param name="options">Optional default options to initialize the model with.</param>
        public QueryModel(QDefaultOptions? options = null)
        {
            MapOptions(options);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryModel"/> class by deserializing from JSON.
        /// </summary>
        /// <param name="json">The JSON string to deserialize from.</param>
        /// <param name="options">Optional default options to initialize the model with.</param>
        public QueryModel(string json, QDefaultOptions? options = null)
        {
            MapOptions(options);
            FromJSON(json);
        }

        /// <summary>
        /// Adds a single filter to the list of property filters.
        /// </summary>
        /// <param name="queryFilter">The query filter to add.</param>
        /// <exception cref="QArgumentException">Thrown when the filter is null.</exception>
        public void AddFilter(QueryFilter queryFilter)
        {
            QArgumentException.ThrowIfNull(queryFilter);
            _propertyFilters.Add(queryFilter);
        }

        /// <summary>
        /// Adds a collection of filters to the list of property filters.
        /// </summary>
        /// <param name="filters">The filters to add.</param>
        /// <exception cref="QArgumentException">Thrown when the filters collection is null.</exception>
        public void AddFilters(IEnumerable<QueryFilter> filters)
        {
            QArgumentException.ThrowIfNull(filters);
            _propertyFilters.AddRange(filters);
        }

        /// <summary>
        /// Adds a single sort filter to the list of sort filters.
        /// </summary>
        /// <param name="filter">The sort filter to add.</param>
        /// <exception cref="QArgumentException">Thrown when the filter is null.</exception>
        public void AddSort(QuerySort filter)
        {
            QArgumentException.ThrowIfNull(filter);
            _sortFilters.Add(filter);
        }

        /// <summary>
        /// Adds a collection of sort filters to the list of sort filters.
        /// </summary>
        /// <param name="filters">The sort filters to add.</param>
        /// <exception cref="QArgumentException">Thrown when the filters collection is null.</exception>
        public void AddSort(IEnumerable<QuerySort> filters)
        {
            QArgumentException.ThrowIfNull(filters);
            _sortFilters.AddRange(filters);
        }

        /// <summary>
        /// Clears all property filters from the query model.
        /// </summary>
        public void ClearFilters() => _propertyFilters.Clear();

        /// <summary>
        /// Clears all sort filters from the query model.
        /// </summary>
        public void ClearSort() => _sortFilters.Clear();

        /// <summary>
        /// Deserializes the query model from a JSON string and updates its properties.
        /// </summary>
        /// <param name="json">The JSON string to deserialize from.</param>
        /// <exception cref="QSerializationException">Thrown when the deserialization fails.</exception>
        public void FromJSON(string json)
        {
            try
            {
                var deserialized = JsonSerializer.Deserialize<QueryModel>(json) ?? throw new QSerializationException("Deserialization failure.");
                PaginateFilter = deserialized.PaginateFilter;
                SearchFilter = deserialized.SearchFilter;
                _propertyFilters.Clear();
                _propertyFilters.AddRange(deserialized.PropertyFilters);
                _sortFilters.Clear();
                _sortFilters.AddRange(deserialized.SortFilters);
            }
            catch (Exception ex)
            {
                throw new QSerializationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Maps the default options to the query model, adding any default sort filters.
        /// </summary>
        /// <param name="options">Optional default options to apply.</param>
        private void MapOptions(QDefaultOptions? options)
        {
            if (options != null && options.SortFilters.Count != 0)
                _sortFilters.AddRange(options.SortFilters);
        }
    }
}
