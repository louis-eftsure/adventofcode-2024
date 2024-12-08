using System.Globalization;

namespace Day_7;

public class Concatenate: IOperator
{
    public double Apply(double a, double b)
    {
        return double.Parse(a + b.ToString(CultureInfo.InvariantCulture));
    }
}