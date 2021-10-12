using CactusDAL.Predicates.Operators;

namespace CactusDAL.Predicates
{
    public class SimplePredicate : IPredicate
    {
        public string TargetPropertyName { get; set; }
        public object ComparedValue { get; set; }
        public ValueComparingOperator ValueComparingOperator { get; set; }
    }
}
