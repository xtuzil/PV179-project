using Infrastructure.Predicates.Operators;

namespace Infrastructure.Predicates
{
    public class SimplePredicate : IPredicate
    {
        public string TargetPropertyName { get; set; }
        public object ComparedValue { get; set; }
        public ValueComparingOperator ValueComparingOperator { get; set; }

        public SimplePredicate(string targetPropertyName, object comparedValue, ValueComparingOperator comparingOperator)
        {
            TargetPropertyName = targetPropertyName;
            ComparedValue = comparedValue;
            ValueComparingOperator = comparingOperator;
        }

        public override string ToString()
        {
            return $"{TargetPropertyName} {ValueComparingOperator.GetString()} {ComparedValue}";
        }
    }
}
