using Infrastructure.Predicates.Operators;

namespace Infrastructure.Predicates
{
    public class SimplePredicate : IPredicate
    {
        public string TargetPropertyName { get; set; }
        public object ComparedValue { get; set; }
        public ValueComparingOperator ValueComparingOperator { get; set; }

        public override string ToString()
        {
            return $"{TargetPropertyName} {ValueComparingOperator.GetString()} {ComparedValue}";
        }
    }
}
