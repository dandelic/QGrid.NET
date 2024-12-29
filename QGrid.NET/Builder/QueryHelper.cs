using QGrid.NET.Builder.Models;
using QGrid.NET.Exceptions;
using QGrid.NET.Serialization.Models;
using System.Linq.Expressions;

namespace QGrid.NET.Builder
{
    /// <summary>
    /// Provides helper methods for building queries, including sorting and pagination logic.
    /// </summary>
    internal static class QueryHelper
    {
        /// <summary>
        /// Sorts the given query based on the provided list of sorting filters.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the query.</typeparam>
        /// <param name="query">The query to apply sorting to.</param>
        /// <param name="sortFilters">The list of sorting filters.</param>
        /// <returns>A sorted <see cref="IQueryable{T}"/> based on the provided sorting filters.</returns>
        internal static IQueryable<T> Sort<T>(this IQueryable<T> query, List<QuerySort> sortFilters)
        {
            return sortFilters.Aggregate(query, (currentQuery, filter) => currentQuery.Sort(filter.PropertyName, filter.Ascending));
        }

        /// <summary>
        /// Sorts the given query based on the specified property name and sort direction (ascending or descending).
        /// </summary>
        /// <typeparam name="T">The type of the elements in the query.</typeparam>
        /// <param name="query">The query to apply sorting to.</param>
        /// <param name="propertyName">The name of the property to sort by.</param>
        /// <param name="ascending">Indicates whether to sort in ascending order.</param>
        /// <returns>A sorted <see cref="IQueryable{T}"/> based on the specified property and sort direction.</returns>
        /// <exception cref="QArgumentException">Thrown when the property name is null or empty or if the property type does not implement <see cref="IComparable"/>.</exception>
        internal static IQueryable<T> Sort<T>(this IQueryable<T> query, string propertyName, bool ascending)
        {
            QArgumentException.ThrowIfNullOrEmpty(propertyName);

            ParameterExpression? parameter = Expression.Parameter(typeof(T), "x");
            Expression? property = ExpressionHelper.PropertyExpression(parameter, propertyName);

            if (property.Type != typeof(string) && !typeof(IComparable).IsAssignableFrom(property.Type))
                throw new QArgumentException($"Property {propertyName} must implement {nameof(IComparable)}");

            LambdaExpression lambda = Expression.Lambda(property, parameter);

            string methodName = ascending ? "OrderBy" : "OrderByDescending";
            MethodCallExpression? result = Expression.Call(
                typeof(Queryable),
                methodName,
                [typeof(T), property.Type],
                query.Expression,
                Expression.Quote(lambda));

            return query.Provider.CreateQuery<T>(result);
        }

        /// <summary>
        /// Gets pagination information for the given query based on the provided pagination filter.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the query.</typeparam>
        /// <param name="query">The query to get pagination info from.</param>
        /// <param name="filter">The pagination filter containing information about page number and rows per page.</param>
        /// <returns>A <see cref="QPaginationInfo"/> object containing pagination information.</returns>
        internal static QPaginationInfo GetPaginationInfo<T>(this IQueryable<T> query, QueryPagination filter)
        {
            int totalCount = query.Count();
            int totalPages = totalCount == 0 ? 0 : (int)Math.Ceiling((double)totalCount / filter.Rows);
            int currentPage = filter.Page >= totalPages ? totalPages - 1 : filter.Page;

            return new QPaginationInfo(currentPage, filter.Rows, totalCount);
        }
    }
}
