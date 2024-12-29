using QGrid.NET.Builder.Models;
using QGrid.NET.Enums;
using QGrid.NET.Exceptions;
using System.Linq.Expressions;
using System.Reflection;

namespace QGrid.NET
{
    internal static class ExpressionHelper
    {
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

        internal static Expression OperatorExpression(LogicalOperatorType operatorType, Expression finalExpression, Expression filterExpression)
            => operatorType switch
            {
                LogicalOperatorType.AND => Expression.AndAlso(finalExpression, filterExpression),
                LogicalOperatorType.OR => Expression.OrElse(finalExpression, filterExpression),
                _ => throw new QArgumentException($"Unsupported logical operator '{operatorType}'."),
            };


        internal static ConstantExpression ConstantExpression(Type type, string value)
        {
            QArgumentException.ThrowIfNull(value);
            object parsedValue;

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
