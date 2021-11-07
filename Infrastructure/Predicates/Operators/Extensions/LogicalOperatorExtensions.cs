namespace Infrastructure.Predicates.Operators.Extensions
{
    public static class LogicalOperatorExtensions
    {
        public static string GetString(this LogicalOperator logicalOperator)
        {
            switch (logicalOperator)
            {
                case LogicalOperator.AND:
                    return "&&";
                case LogicalOperator.OR:
                    return "||";
                default:
                    return "";
            }
        }


    }
}
