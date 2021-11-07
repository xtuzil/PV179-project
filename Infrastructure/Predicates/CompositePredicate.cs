using Infrastructure.Predicates.Operators;
using Infrastructure.Predicates.Operators.Extensions;
using System.Collections.Generic;

namespace Infrastructure.Predicates
{
    public class CompositePredicate : IPredicate
    {
        public List<IPredicate> Predicates { get; set; }
        public LogicalOperator LogicalOperator { get; set; }

        public CompositePredicate(List<IPredicate> Predicates, LogicalOperator LogicalOperator)
        {
            this.Predicates = Predicates;
            this.LogicalOperator = LogicalOperator;
        }

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < Predicates.Count; i++)
            {
                result += Predicates[i];

                if (i < Predicates.Count - 1)
                {
                    result += " ";
                    result += LogicalOperator.GetString();
                }
            }

            return result;
        }
    }
}
