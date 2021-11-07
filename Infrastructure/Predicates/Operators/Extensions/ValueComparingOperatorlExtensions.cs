namespace Infrastructure.Predicates.Operators.Extensions
{
    public static class ValueComparingOperatorlExtensions
    {
        public static string GetString(this ValueComparingOperator valueComparingOperator)
        {
            switch (valueComparingOperator)
            {
                case ValueComparingOperator.None:
                    return "";
                case ValueComparingOperator.GreaterThan:
                    return ">";
                case ValueComparingOperator.GreaterThanOrEqual:
                    return ">=";
                case ValueComparingOperator.Equal:
                    return "=";
                case ValueComparingOperator.NotEqual:
                    return "<>";
                case ValueComparingOperator.LessThan:
                    return "<";
                case ValueComparingOperator.LessThanOrEqual:
                    return "<=";
                case ValueComparingOperator.StringContains:
                    return "LIKE";
                default:
                    return "";
            }
        }
    }
}
