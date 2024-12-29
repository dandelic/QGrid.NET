using QGrid.NET.Builder;
using QGrid.NET.Builder.Models;
using QGrid.NET.Serialization.Models;
using System.Linq.Expressions;

namespace QGrid.NET
{
    /// <summary>
    /// QGrid extension methods for IQueryable
    /// </summary>
    public static class QueryExtensions
    {
        /// <summary>
        /// Evaluates an IQueryable with a QueryModel filter and returning a paged response of the target type.
        /// This method applies filtering, sorting, and pagination to the source IQueryable, and allows for projecting the results into a target type.
        /// The optional <paramref name="expression"/> parameter can be provided for custom projection logic. If not provided, the default projection is used.
        /// </summary>
        /// <typeparam name="TTarget">The target type of the projection. This is the type that the results will be projected into.</typeparam>
        /// <typeparam name="TSource">The source type of the IQueryable that is being queried. This is the type from which data is fetched.</typeparam>
        /// <param name="query">The IQueryable source to be filtered and projected.</param>
        /// <param name="filter">The query model containing filter, sort, and pagination information to be applied to the source query.</param>
        /// <param name="expression">An optional expression used to map or project the source query to the target type. If no expression is provided, the default projection logic will be used.</param>
        /// <returns>A <see cref="QPagedResponse{TTarget}"/> containing the paged list of projected results of type <typeparamref name="TTarget"/>. 
        /// This includes both the projected data and pagination information like total pages, current page, and page size.</returns>
        public static QPagedResponse<TTarget> Evaluate<TTarget, TSource>(this IQueryable<TSource> query, QueryModel filter,
            Expression<Func<TSource, TTarget>> expression)
        {
            QueryBuilder<TSource> builder = new(query, filter);
            return builder.Evaluate(expression);
        }

        /// <summary>
        /// Evaluates an IQueryable with a QueryModel filter and returning a paged response of the target type.
        /// This method applies filtering, sorting, and pagination to the source IQueryable, and allows for projecting the results into a target type.
        /// The optional <paramref name="expression"/> parameter can be provided for custom projection logic. If not provided, the default projection is used.
        /// </summary>
        /// <typeparam name="TTarget">The target type of the projection. This is the type that the results will be projected into.</typeparam>
        /// <typeparam name="TSource">The source type of the IQueryable that is being queried. This is the type from which data is fetched.</typeparam>
        /// <param name="query">The IQueryable source to be filtered and projected.</param>
        /// <param name="json">JSON representation of the query model containing filter, sort, and pagination information to be applied to the source query.</param>
        /// <param name="expression">An optional expression used to map or project the source query to the target type. If no expression is provided, the default projection logic will be used.</param>
        /// <returns>A <see cref="QPagedResponse{TTarget}"/> containing the paged list of projected results of type <typeparamref name="TTarget"/>. 
        /// This includes both the projected data and pagination information like total pages, current page, and page size.</returns>
        public static QPagedResponse<TTarget> Evaluate<TTarget, TSource>(this IQueryable<TSource> query, string json,
            Expression<Func<TSource, TTarget>> expression)
        {
            QueryBuilder<TSource> builder = new(query, json);
            return builder.Evaluate(expression);
        }

        /// <summary>
        /// Extension method for building an IQueryable query by applying the filters, sorting, and pagination from a QueryModel.
        /// </summary>
        /// <typeparam name="TSource">The source type of the IQueryable that is being queried.</typeparam>
        /// <param name="query">The original IQueryable source to be modified based on the filter model.</param>
        /// <param name="filter">The query model containing filter, sort, and pagination information to be applied to the source query.</param>
        /// <returns>An <see cref="IQueryable{TSource}"/> that is modified with the filtering, sorting, and pagination based on the provided filter model.</returns>
        public static IQueryable<TSource> Evaluate<TSource>(this IQueryable<TSource> query, QueryModel filter)
        {
            QueryBuilder<TSource> builder = new(query, filter);
            return builder.BuildQuery();
        }
    }
}
