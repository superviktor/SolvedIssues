namespace CSharpObserveVersions.Property_matching
{
    public class DiscountCalculator
    {
        public decimal GetStateDependentDiscount(decimal baseDiscount, Address address) =>
            address switch
            {
                { State : "CA"} => baseDiscount * 2,
                { State: "MI" } => baseDiscount / 3,
                _ => baseDiscount
            };
    }
}
