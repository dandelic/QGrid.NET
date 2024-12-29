using QGrid.NET.Builder.Models;
using QGrid.NET.Enums;
using QGrid.NET.Serialization.Models;
using System.Linq.Expressions;

namespace QGrid.NET.Builder
{
    /// <summary>
    /// QueryBuilder facilitates building queries on an IQueryable collection using filters, sorting, and pagination.
    /// </summary>
    /// <typeparam name="TSource">The type of the entity being queried.</typeparam>
    public class QueryBuilder<TSource>
    {
        private readonly QueryModel _filter;
        private readonly IQueryable<TSource> _query;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryBuilder{TSource}"/> class.
        /// </summary>
        /// <param name="query">Query on which actions are performed.</param>
        /// <param name="filter">Model that encapsulates various filters, sorting, pagination, and search options </param>
        public QueryBuilder(IQueryable<TSource> query, QueryModel filter)
        {
            _query = query;
            _filter = filter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryBuilder{TSource}"/> class.
        /// </summary>
        /// <param name="query">Query on which actions are performed.</param>
        /// <param name="json">JSON representation of model that encapsulates various filters, sorting, pagination, and search options </param>
        public QueryBuilder(IQueryable<TSource> query, string json)
        {
            _query = query;
            _filter = new QueryModel(json);
        }

        /// <summary>
        /// Provides public access to the current query.
        /// </summary>
        public IQueryable<TSource> Query { get => _query; }

        /// <summary>
        /// Builds the query based on the current filter configuration.
        /// </summary>
        /// <returns>An IQueryable collection with applied filters and sorting.</returns>
        public IQueryable<TSource> BuildQuery()
        {
            Expression<Func<TSource, bool>> expression = BuildExpression(_filter);
            return _query.Where(expression).Sort([.._filter.SortFilters]);
        }

        /// <summary>
        /// Evaluates the query and returns a paginated response.
        /// </summary>
        /// <typeparam name="TTarget">The type of the resulting projection.</typeparam>
        /// <param name="expression">A lambda expression for projection (optional).</param>
        /// <returns>A paginated response with the projected result.</returns>
        public QPagedResponse<TTarget> Evaluate<TTarget>(Expression<Func<TSource, TTarget>> expression)
        {
            IEnumerable<TTarget> projection = Projection(expression);
            return new QPagedResponse<TTarget>(projection, PaginationInfo);
        }

        /// <summary>
        /// Adds an AND filter to the current filter configuration.
        /// </summary>
        /// <param name="propertyName">The name of the property to filter by.</param>
        /// <param name="operandType">The type of the comparison (e.g., equals, contains).</param>
        /// <param name="value">The value to compare against.</param>
        /// <returns>The current instance of QueryBuilder for method chaining.</returns>
        public QueryBuilder<TSource> And(string propertyName, OperandType operandType, string value)
        {
            _filter.AddFilter(new QueryFilter(propertyName, operandType, value, LogicalOperatorType.AND));
            return this;
        }

        /// <summary>
        /// Adds an OR filter to the current filter configuration.
        /// </summary>
        /// <param name="propertyName">The name of the property to filter by.</param>
        /// <param name="operandType">The type of the comparison (e.g., equals, contains).</param>
        /// <param name="value">The value to compare against.</param>
        /// <returns>The current instance of QueryBuilder for method chaining.</returns>
        public QueryBuilder<TSource> Or(string propertyName, OperandType operandType, string value)
        {
            _filter.AddFilter(new QueryFilter(propertyName, operandType, value, LogicalOperatorType.OR));
            return this;
        }

        /// <summary>
        /// Adds a search filter to the query using multiple properties and a search term.
        /// </summary>
        /// <param name="properties">The list of properties to search in.</param>
        /// <param name="term">The search term to match against.</param>
        /// <param name="logicalOperatorType">The logical operator used to combine search filters (default: OR).</param>
        /// <param name="operandType">The type of the comparison (default: contains).</param>
        /// <returns>The current instance of QueryBuilder for method chaining.</returns>
        public QueryBuilder<TSource> Search(List<string> properties, string term,
            LogicalOperatorType logicalOperatorType = LogicalOperatorType.OR, OperandType operandType = OperandType.Contains)
        {
            _filter.SearchFilter = new QuerySearch(term, properties, operandType, logicalOperatorType);
            return this;
        }

        /// <summary>
        /// Adds sorting to the query based on a specified property and order.
        /// </summary>
        /// <param name="propertyName">The name of the property to sort by.</param>
        /// <param name="ascending">Whether to sort in ascending order.</param>
        /// <param name="setPrimary">Whether to clear previous sorting and set this as primary sorting (default: true).</param>
        /// <returns>The current instance of QueryBuilder for method chaining.</returns>
        public QueryBuilder<TSource> SortBy(string propertyName, bool ascending, bool setPrimary = true)
        {
            if (setPrimary)
                _filter.ClearSort();
            _filter.AddSort(new QuerySort(propertyName, ascending));
            return this;
        }

        #region Private Methods

        /// <summary>
        /// Generates pagination information for the current query.
        /// </summary>
        private QPaginationInfo PaginationInfo => _query.GetPaginationInfo(_filter.PaginateFilter!);

        /// <summary>
        /// Projects the query result to a specified target type using a provided expression.
        /// </summary>
        /// <typeparam name="TTarget">The target type for the projection.</typeparam>
        /// <param name="expression">A lambda expression defining the projection.</param>
        /// <returns>A list of projected results.</returns>
        private List<TTarget> Projection<TTarget>(Expression<Func<TSource, TTarget>> expression)
        {
            IQueryable<TSource> query = BuildQuery();
            return [..query.Select(expression)];
        }

        /// <summary>
        /// Constructs a filtering expression based on the provided QueryModel.
        /// </summary>
        /// <param name="filter">The QueryModel containing filtering rules.</param>
        /// <returns>A lambda expression representing the filter logic.</returns>
        private static Expression<Func<TSource, bool>> BuildExpression(QueryModel filter)
        {
            List<QueryFilter> toFilter = filter.PropertyFilters?.ToList() ?? [];

            if (filter.SearchFilter != null)
            {
                IEnumerable<QueryFilter> searchFilters = filter.SearchFilter.Properties.Select(x => new QueryFilter(x,
                    filter.SearchFilter.OperandType,
                    filter.SearchFilter.SearchTerm,
                    filter.SearchFilter.LogicalOperator));

                toFilter.AddRange(searchFilters);
            }

            if (toFilter.Count == 0)
                return entity => true;

            ParameterExpression parameter = Expression.Parameter(typeof(TSource), "x");
            Expression? finalExpression = null;

            foreach (var propertyFilter in toFilter)
            {
                Type propertyExpression = ExpressionHelper.PropertyExpression(parameter, propertyFilter.PropertyName).Type;
                OperandsMap.ValidateOperand(propertyFilter.OperandType, propertyExpression);

                Expression filterExpression = ExpressionHelper.FilterExpression<TSource>(parameter, propertyFilter);

                if (finalExpression == null)
                    finalExpression = filterExpression;
                else
                    finalExpression = ExpressionHelper.OperatorExpression(propertyFilter.LogicalOperator, finalExpression, filterExpression);
            }

            return finalExpression == null
                ? entity => true
                : Expression.Lambda<Func<TSource, bool>>(finalExpression, parameter);
        }

        #endregion
    }
}
