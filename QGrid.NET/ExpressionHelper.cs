using QGrid.NET.Builder.Models;
using QGrid.NET.Enums;
using QGrid.NET.Exceptions;
using System.Linq.Expressions;
using System.Reflection;

namespace QGrid.NET
{
    internal static class ExpressionHelper
    {
        /// <summary>
        /// Generates a MemberExpression for accessing a property or field from a given parameter expression.
        /// </summary>
        /// <param name="parameter">The parameter expression representing the object.</param>
        /// <param name="propertyName">The name of the property or field to access, supports nested properties using dot notation.</param>
        /// <returns>A MemberExpression representing the property access.</returns>
        /// <exception cref="QArgumentException">Thrown if the property name is null, empty, or invalid for the parameter type.</exception>
        internal static MemberExpression PropertyExpression(ParameterExpression parameter, string propertyName)
        {
            QArgumentException.ThrowIfNullOrEmpty(propertyName);
            Expression property = parameter;

            foreach (var propName in propertyName.Split('.'))
            {
                PropertyInfo propertyInfo = property.Type.GetProperty(propName) ??
                    throw new QArgumentException($"{propName}' is not a valid property or field of type '{property.Type.Name}'.");

                property = Expression.PropertyOrField(property, propName);
            }

            return (MemberExpression)property;
        }

        /// <summary>
        /// Creates a filter expression based on a QueryFilter object.
        /// </summary>
        /// <typeparam name="T">The type of the entity being filtered.</typeparam>
        /// <param name="parameter">The parameter expression representing the object.</param>
        /// <param name="filter">The filter definition containing property name, value, and operand type.</param>
        /// <returns>An Expression representing the filter logic.</returns>
        /// <exception cref="QArgumentException">Thrown if the operand type is unsupported.</exception>
        internal static Expression FilterExpression<T>(ParameterExpression parameter, QueryFilter filter)
        {
            MemberExpression property = PropertyExpression(parameter, filter.PropertyName);
            Expression constant = ConstantExpression(property.Type, filter.Value);

            return filter.OperandType switch
            {
                OperandType.Equals => Expression.Equal(property, constant),
                OperandType.NotEquals => Expression.NotEqual(property, constant),
                OperandType.GreaterThan => Expression.GreaterThan(property, constant),
                OperandType.GreaterThanOrEqual => Expression.GreaterThanOrEqual(property, constant),
                OperandType.LessThan => Expression.LessThan(property, constant),
                OperandType.LessThanOrEqual => Expression.LessThanOrEqual(property, constant),
                OperandType.Contains => Expression.Call(property, "Contains", null, constant),
                OperandType.StartsWith => Expression.Call(property, "StartsWith", null, constant),
                OperandType.EndsWith => Expression.Call(property, "EndsWith", null, constant),
                _ => throw new QArgumentException($"Unsupported filter type '{filter.OperandType}' for property '{filter.PropertyName}'."),
            };
        }

        /// <summary>
        /// Combines two expressions using a logical operator (AND/OR).
        /// </summary>
        /// <param name="operatorType">The logical operator type (AND/OR).</param>
        /// <param name="finalExpression">The existing expression.</param>
        /// <param name="filterExpression">The new filter expression to combine.</param>
        /// <returns>A combined Expression using the specified logical operator.</returns>
        /// <exception cref="QArgumentException">Thrown if the logical operator type is unsupported.</exception>
        internal static Expression OperatorExpression(LogicalOperatorType operatorType, Expression finalExpression, Expression filterExpression)
            => operatorType switch
            {
                LogicalOperatorType.AND => Expression.AndAlso(finalExpression, filterExpression),
                LogicalOperatorType.OR => Expression.OrElse(finalExpression, filterExpression),
                _ => throw new QArgumentException($"Unsupported logical operator '{operatorType}'."),
            };

        /// <summary>
        /// Creates a constant expression from a string value and a target type.
        /// </summary>
        /// <param name="type">The target type of the constant.</param>
        /// <param name="value">The string representation of the value to parse.</param>
        /// <returns>A ConstantExpression representing the parsed value.</returns>
        /// <exception cref="QArgumentException">Thrown if the type is unsupported or the value cannot be parsed into the target type.</exception>
        internal static ConstantExpression ConstantExpression(Type type, string value)
        {
            QArgumentException.ThrowIfNull(value);
            object parsedValue;

            if (type.IsEnum)
            {
                if (Enum.TryParse(type, value?.ToString(), true, out var result))              
                    return Expression.Constant(result, type);            
                else           
                    throw QArgumentException.ThrowIfInvalidType(type, value!);              
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Int32:
                    if (int.TryParse(value, out var intResult))
                        parsedValue = intResult;
                    else
                        throw QArgumentException.ThrowIfInvalidType(typeof(int), value);
                    break;

                case TypeCode.Double:
                    if (double.TryParse(value, out var doubleResult))
                        parsedValue = doubleResult;
                    else
                        throw QArgumentException.ThrowIfInvalidType(typeof(double), value);
                    break;

                case TypeCode.Int64:
                    if (long.TryParse(value, out var longResult))
                        parsedValue = longResult;
                    else
                        throw QArgumentException.ThrowIfInvalidType(typeof(long), value);
                    break;

                case TypeCode.Single:
                    if (float.TryParse(value, out var floatResult))
                        parsedValue = floatResult;
                    else
                        throw QArgumentException.ThrowIfInvalidType(typeof(float), value);
                    break;

                case TypeCode.Decimal:
                    if (decimal.TryParse(value, out var decimalResult))
                        parsedValue = decimalResult;
                    else
                        throw QArgumentException.ThrowIfInvalidType(typeof(decimal), value);
                    break;

                case TypeCode.Boolean:
                    if (bool.TryParse(value, out var boolResult))
                        parsedValue = boolResult;
                    else
                        throw QArgumentException.ThrowIfInvalidType(typeof(bool), value);
                    break;

                case TypeCode.String:
                    parsedValue = value;
                    break;

                case TypeCode.DateTime:
                    if (DateTime.TryParse(value, out var dateTimeResult))
                        parsedValue = dateTimeResult;
                    else
                        throw QArgumentException.ThrowIfInvalidType(typeof(DateTime), value);
                    break;

                default:
                    throw new QArgumentException($"Type '{type.Name}' is not supported for filtering.");
            }

            return Expression.Constant(parsedValue, type);
        }
    }
}
