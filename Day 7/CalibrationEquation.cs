namespace Day_7;

public class CalibrationEquation
{
    // Test if the answer can be calculated from the equation parts and any combination of the operations
    public static bool IsValid(string equation, List<IOperator> operations, out double target)
    {
        var equationStringParts = equation.Split(":").ToList();
        target  = double.Parse(equationStringParts[0].Trim());
        var equationParts = equationStringParts[1].Trim().Split(" ").Select(double.Parse).ToList();
        return Evaluate(equationParts, operations, target, equationParts[0], 1);
    }
    
    static bool Evaluate(List<double> numbers, List<IOperator> operations, double target, double currentValue, int index)
    {
        // Base case: if we've used all numbers, check if currentValue matches the target
        if (index == numbers.Count)
        {
            return currentValue == target;
        }

        // Recursive cases: try all operations with the next number
        double nextNumber = numbers[index];

        foreach (var operation in operations)
        {
            if(Evaluate(numbers, operations, target, operation.Apply(currentValue, nextNumber), index + 1))
            {
                return true;
            }
        }
        
        return false;
    }
}