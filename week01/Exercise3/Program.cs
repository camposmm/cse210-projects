using System;

class Program
{
    static void Main(string[] args)
    {
        // Test constructors
        Fraction fraction1 = new Fraction();       // 1/1
        Fraction fraction2 = new Fraction(5);       // 5/1
        Fraction fraction3 = new Fraction(3, 4);    // 3/4
        Fraction fraction4 = new Fraction(1, 3);   // 1/3

        // Test getters and setters
        fraction1.SetNumerator(6);
        fraction1.SetDenominator(7);
        Console.WriteLine($"Fraction 1: {fraction1.GetFractionString()}"); // Should print 6/7

        // Test representation methods
        Console.WriteLine($"{fraction1.GetFractionString()} = {fraction1.GetDecimalValue()}");
        Console.WriteLine($"{fraction2.GetFractionString()} = {fraction2.GetDecimalValue()}");
        Console.WriteLine($"{fraction3.GetFractionString()} = {fraction3.GetDecimalValue()}");
        Console.WriteLine($"{fraction4.GetFractionString()} = {fraction4.GetDecimalValue()}");
    }
}