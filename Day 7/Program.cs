// See https://aka.ms/new-console-template for more information

using Day_7;

var partOneOperations = new List<IOperator> { new Multiply(), new Add() };
var inputStrings = await File.ReadAllLinesAsync("input.txt");

Console.WriteLine("Part 1: " + GetValidEquationSum(partOneOperations, inputStrings));

// Part 2
var partTwoOperations = new List<IOperator> { new Multiply(), new Add(), new Concatenate() };
Console.WriteLine("Part 2: " + GetValidEquationSum(partTwoOperations, inputStrings));

double GetValidEquationSum(List<IOperator> operations, string[] inputStrings)
{
    var validResults = new List<double>();
    
    Parallel.ForEach(inputStrings, inputString =>
    {
        if(CalibrationEquation.IsValid(inputString, operations, out var target)){
            lock(validResults)
            {
                validResults.Add(target);
            }
        }
    });

    return validResults.Sum();
}