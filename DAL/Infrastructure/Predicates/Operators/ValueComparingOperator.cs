namespace CactusDAL.Predicates.Operators
{
    public enum ValueComparingOperator
    {
        None,
        GreaterThan,
        GreaterThanOrEqual,
        Equal,
        NotEqual,
        LessThan,
        LessThanOrEqual,
        StringContains,
    }

    public static class ErrorLevelExtensions
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
