
| Method              | Description                       |
| ------------------- | --------------------------------- |
| `Abs(x)`            | Absolute value                    |
| `Max(a, b)` / `Min` | Max or Min of two values          |
| `Pow(x, y)`         | x raised to power y               |
| `Sqrt(x)`           | Square root                       |
| `Ceiling`, `Floor`  | Round up/down to nearest integer  |
| `Round`, `Truncate` | Round to nearest, strip decimal   |
| `Sin`, `Cos`, `Tan` | Trigonometric functions           |
| `Log`, `Log10`      | Logarithms (base *e* and base 10) |
| `Exp(x)`            | Exponential of x (e^x)            |
| `PI`, `E`           | Mathematical constants            |





| Method                 | Description                          |
| ---------------------- | ------------------------------------ |
| `Next()`               | Random int (0 to MaxInt)             |
| `Next(max)`            | Random int (0 to max-1)              |
| `Next(min, max)`       | Random int (min to max-1)            |
| `NextDouble()`         | Random double (0.0 to <1.0)          |
| Simulated Toss/Boolean | Using `Next(2)` and ternary operator |




using System;

class MathAndRandomDemo
{
    static void Main()
    {
        Console.WriteLine("=== Math Class Demo ===");

        double a = 15.75;
        double b = 4.0;

        // Basic Math
        Console.WriteLine("Math.Abs(-20): " + Math.Abs(-20));
        Console.WriteLine("Math.Max(10, 25): " + Math.Max(10, 25));
        Console.WriteLine("Math.Min(10, 25): " + Math.Min(10, 25));

        // Powers and Roots
        Console.WriteLine("Math.Pow(3, 2): " + Math.Pow(3, 2));    // 3^2 = 9
        Console.WriteLine("Math.Sqrt(81): " + Math.Sqrt(81));     // √81 = 9

        // Rounding
        Console.WriteLine("Math.Ceiling(15.75): " + Math.Ceiling(a)); // Round up
        Console.WriteLine("Math.Floor(15.75): " + Math.Floor(a));     // Round down
        Console.WriteLine("Math.Round(15.75): " + Math.Round(a));     // Round to nearest
        Console.WriteLine("Math.Round(15.5): " + Math.Round(15.5));   // Tie-breaking
        Console.WriteLine("Math.Truncate(15.75): " + Math.Truncate(a)); // Remove decimal

        // Trigonometry (in radians)
        double angle = Math.PI / 4; // 45 degrees
        Console.WriteLine("Math.Sin(π/4): " + Math.Sin(angle));
        Console.WriteLine("Math.Cos(π/4): " + Math.Cos(angle));
        Console.WriteLine("Math.Tan(π/4): " + Math.Tan(angle));

        // Logarithmic and exponential
        Console.WriteLine("Math.Log(10): " + Math.Log(10));       // Natural log
        Console.WriteLine("Math.Log10(100): " + Math.Log10(100)); // Base-10 log
        Console.WriteLine("Math.Exp(2): " + Math.Exp(2));         // e^2

        // Constants
        Console.WriteLine("Math.PI: " + Math.PI);
        Console.WriteLine("Math.E: " + Math.E);

        Console.WriteLine("\n=== Random Class Demo ===");

        // Create a Random object
        Random rand = new Random();

        // Generate Integers
        Console.WriteLine("Random Integer (0-99): " + rand.Next(100));
        Console.WriteLine("Random Integer (50-100): " + rand.Next(50, 101)); // 101 is exclusive

        // Generate multiple random numbers
        Console.Write("5 Random Integers (1-6 like dice): ");
        for (int i = 0; i < 5; i++)
        {
            Console.Write(rand.Next(1, 7) + " ");
        }

        // Generate Random Double
        Console.WriteLine("\nRandom Double (0.0 to <1.0): " + rand.NextDouble());

        // Simulate a coin toss
        string coin = rand.Next(2) == 0 ? "Heads" : "Tails";
        Console.WriteLine("Coin Toss: " + coin);

        // Simulate random boolean
        bool randomBool = rand.Next(2) == 1;
        Console.WriteLine("Random Boolean: " + randomBool);
    }
}
