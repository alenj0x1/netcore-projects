using System;

namespace InterestCalculator
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Interest calculator v1");

      decimal principal = GetDecimalInput("Enter the initial capital: ");
      decimal rate = GetDecimalInput("Enter the interest rate (in %): ") / 100;
      int years = GetIntInput("Enter the number of years: ");
      string interestType = GetInterestType();

      decimal amount = interestType == "simple"
          ? CalculateSimpleInterest(principal, rate, years)
          : CalculateCompoundInterest(principal, rate, years);

      Console.WriteLine($"The accumulated amount after {years} years is: {amount:C2}");
    }

    static decimal GetDecimalInput(string prompt)
    {
      decimal value;

      Console.Write(prompt);
      while (!decimal.TryParse(Console.ReadLine(), out value) || value < 0)
      {
        Console.Write("Invalid input. Please enter a decimal number: ");
      }

      return value;
    }

    static int GetIntInput(string prompt)
    {
      int value;
      Console.Write(prompt);

      while (!int.TryParse(Console.ReadLine(), out value) || value < 0)
      {
        Console.Write("Invalid input. Please enter a integer: ");
      }

      return value;
    }

    static string GetInterestType()
    {
      Console.Write("Enter the type of interest (simple/compound): ");

      string? interestType = Console.ReadLine();

      while (interestType != "simple" && interestType != "compound")
      {
        Console.Write("Invalid input. Please enter 'simple' or 'compound': ");
        interestType = Console.ReadLine();
      }

      return interestType;
    }

    static decimal CalculateSimpleInterest(decimal principal, decimal rate, int years)
    {
      return principal * (1 + rate * years);
    }

    static decimal CalculateCompoundInterest(decimal principal, decimal rate, int years)
    {
      return principal * (decimal)Math.Pow((double)(1 + rate), years);
    }
  }
}