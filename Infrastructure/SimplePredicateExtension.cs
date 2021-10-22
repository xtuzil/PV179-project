using CactusDAL.Predicates;
using CactusDAL.Predicates.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CactusDAL.Infrastructure
{
    public static class SimplePredicateExtension
    {
        private static IDictionary<ValueComparingOperator, Func<MemberExpression, ConstantExpression, Expression>> Expressions = new Dictionary<ValueComparingOperator, Func<MemberExpression, ConstantExpression, Expression>>
        {
            [ValueComparingOperator.GreaterThan] = (MemberExpression memberExpression, ConstantExpression constantExpression) => Expression.GreaterThan(memberExpression, constantExpression),
            [ValueComparingOperator.GreaterThanOrEqual] = (MemberExpression memberExpression, ConstantExpression constantExpression) => Expression.GreaterThanOrEqual(memberExpression, constantExpression),
            [ValueComparingOperator.LessThan] = (MemberExpression memberExpression, ConstantExpression constantExpression) => Expression.LessThan(memberExpression, constantExpression),
            [ValueComparingOperator.LessThanOrEqual] = (MemberExpression memberExpression, ConstantExpression constantExpression) => Expression.LessThanOrEqual(memberExpression, constantExpression),
            [ValueComparingOperator.NotEqual] = (MemberExpression memberExpression, ConstantExpression constantExpression) => Expression.NotEqual(memberExpression, constantExpression),
            [ValueComparingOperator.StringContains] = (MemberExpression memberExpression, ConstantExpression constantExpression) => Expression.Call(memberExpression, "Contains", new Type[] { }, constantExpression),
    };
    
        public static Expression GetExpression(this SimplePredicate simplePredicate, ParameterExpression parameterExpression)
        {
            MemberExpression memberExpression = Expression.Property(parameterExpression, simplePredicate.TargetPropertyName);
            ConstantExpression constantExpression = Expression.Constant(simplePredicate.ComparedValue, GetMemberType(simplePredicate, memberExpression));

            return TransformToExpression(simplePredicate.ValueComparingOperator, memberExpression, constantExpression);
        }

        private static Type GetMemberType(SimplePredicate simplePredicate, MemberExpression memberExpression)
        {
            return memberExpression.GetType();
        }

        public static Expression TransformToExpression(ValueComparingOperator comparingOperator, MemberExpression memberExpression, ConstantExpression constantExpression)
        {
            return Expressions[comparingOperator](memberExpression, constantExpression);
        }
    }
}
