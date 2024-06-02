using System;

namespace ConsoleCalculator
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.Clear();
      Console.WriteLine("----------------------");
      Console.WriteLine("Console calculator v1");
      Console.WriteLine("----------------------");
      Console.WriteLine("Select a option:");
      Console.WriteLine("----------------------");
      Console.WriteLine("1. Add");
      Console.WriteLine("2. Substract");
      Console.WriteLine("3. Multiply");
      Console.WriteLine("4. Division");
      Console.WriteLine("5. Exit");
      Console.WriteLine("----------------------");
      Console.Write("Your option: ");

      string? InputUser = Console.ReadLine();

      switch (InputUser)
      {
        case "1":
          StartOperation("sum");
          break;
        case "2":
          StartOperation("sub");
          break;
        case "3":
          StartOperation("mul");
          break;
        case "4":
          StartOperation("div");
          break;
        case "5":
          ExitCalculator();
          break;
        default:
          Console.WriteLine("Option invalid. Try again");

          Thread.Sleep(1000);

          Main([]);

          break;
      }
    }

    static void StartOperation(string? operation)
    {
      try
      {
        Console.WriteLine("----------------------");

        Console.Write("Enter your first number: ");
        double NumberOne = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter your second number: ");
        double NumberTwo = Convert.ToDouble(Console.ReadLine());

        double result = operation switch
        {
          "sum" => NumberOne + NumberTwo,
          "sub" => NumberOne - NumberTwo,
          "mul" => NumberOne * NumberTwo,
          "div" => NumberTwo != 0 ? NumberOne / NumberTwo : throw new DivideByZeroException(),
          _ => throw new InvalidOperationException("Error: Invalid operation")
        };

        Console.WriteLine("----------------------");
        Console.WriteLine($"Operation result: {result}");
      }
      catch (FormatException)
      {
        Console.WriteLine("Error: Input invalid");
      }
      catch (DivideByZeroException)
      {
        Console.WriteLine("Error: Not allowed divide by zero");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}");
      }
      finally
      {
        Console.WriteLine("----------------------");
        Console.WriteLine("Press any key for other operation.");
        Console.WriteLine("Press 'o' for exit.");

        // True for not show key
        var key = Console.ReadKey(true).Key;

        if (key == ConsoleKey.O)
        {
          ExitCalculator();
        }

        Main([]);
      }
    }

    static void ExitCalculator()
    {
      Environment.Exit(0);
    }
  }
}
