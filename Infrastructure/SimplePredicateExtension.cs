using Infrastructure.Predicates;
using Infrastructure.Predicates.Operators;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Infrastructure
{
    public static class SimplePredicateExtension
    {
        private static IDictionary<ValueComparingOperator, Func<MemberExpression, ConstantExpression, Expression>> Expressions = new Dictionary<ValueComparingOperator, Func<MemberExpression, ConstantExpression, Expression>>
        {
            [ValueComparingOperator.Equal] = (memberExpression, constantExpression) => Expression.Equal(memberExpression, constantExpression),
            [ValueComparingOperator.GreaterThan] = (memberExpression, constantExpression) => Expression.GreaterThan(memberExpression, constantExpression),
            [ValueComparingOperator.GreaterThanOrEqual] = (memberExpression, constantExpression) => Expression.GreaterThanOrEqual(memberExpression, constantExpression),
            [ValueComparingOperator.LessThan] = (memberExpression, constantExpression) => Expression.LessThan(memberExpression, constantExpression),
            [ValueComparingOperator.LessThanOrEqual] = (memberExpression, constantExpression) => Expression.LessThanOrEqual(memberExpression, constantExpression),
            [ValueComparingOperator.NotEqual] = (memberExpression, constantExpression) => Expression.NotEqual(memberExpression, constantExpression),
            [ValueComparingOperator.StringContains] = (memberExpression, constantExpression) => Expression.Call(memberExpression, "Contains", new Type[] { }, constantExpression),
        };

        public static Expression GetExpression(this SimplePredicate simplePredicate, ParameterExpression parameterExpression)
        {
            MemberExpression memberExpression = Expression.Property(parameterExpression, simplePredicate.TargetPropertyName);
            Type type = GetMemberType(simplePredicate, memberExpression);
            ConstantExpression constantExpression = Expression.Constant(simplePredicate.ComparedValue, type);

            return TransformToExpression(simplePredicate.ValueComparingOperator, memberExpression, constantExpression);
        }

        private static Type GetMemberType(SimplePredicate simplePredicate, MemberExpression memberExpression)
        {
            return memberExpression.Type;
        }

        public static Expression TransformToExpression(ValueComparingOperator comparingOperator, MemberExpression memberExpression, ConstantExpression constantExpression)
        {
            return Expressions[comparingOperator](memberExpression, constantExpression);
        }
    }
}
