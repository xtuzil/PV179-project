using Infrastructure.Predicates.Operators;
using System.Collections.Generic;

namespace Infrastructure.Predicates
{
    public class CompositePredicate : IPredicate
    {
        public List<IPredicate> Predicates { get; set; }
        public LogicalOperator LogicalOperator { get; set; }

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
