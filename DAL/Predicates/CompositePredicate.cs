using CactusDAL.Predicates.Operators;
using System.Collections.Generic;

namespace CactusDAL.Predicates
{
    public class CompositePredicate : IPredicate
    {
        public List<IPredicate> Predicates { get; set; }
        public LogicalOperator LogicalOperator { get; set; }
    }
}
