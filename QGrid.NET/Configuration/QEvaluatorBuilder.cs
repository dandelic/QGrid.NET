using Microsoft.Extensions.DependencyInjection;

namespace QGrid.NET.Configuration
{
    /// <summary>
    /// Provides extension methods for configuring QGrid services and options.
    /// </summary>
    public static class QEvaluatorBuilder
    {
        /// <summary>
        /// Adds the QGrid services to the <see cref="IServiceCollection"/> and configures the options.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        /// <param name="configureOptions">An optional action to configure the default options for QGrid.</param>
        /// <returns>The updated <see cref="IServiceCollection"/> with the QGrid services added.</returns>
        public static IServiceCollection AddQGrid(this IServiceCollection services, Action<QDefaultOptions>? configureOptions = null)
        {
            var options = new QDefaultOptions();
            configureOptions?.Invoke(options);
            services.AddSingleton(options);

            return services;
        }
    }
}
