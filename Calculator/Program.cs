using System;
using Calculator.Exceptions;
using Calculator.Exceptions;

namespace Calculator
{


    class Calculator
    {
        static void Main()
        {
            try
            {
                Console.WriteLine("Number #1: ");
                bool isValid1 = int.TryParse(Console.ReadLine(), out int num1);

                Console.WriteLine("Number #2: ");
                bool isValid2 = int.TryParse(Console.ReadLine(), out int num2);

                if (!isValid1 || !isValid2)
                {
                    throw new InvalidInputException();
                }

                Console.WriteLine("Choose operation: [+], [-], [*], [/]: ");
                string operation = Console.ReadLine();

                switch (operation)
                {
                    case "+":
                        Console.WriteLine(num1 + num2);
                        break;

                    case "-":
                        Console.WriteLine(num1 - num2);
                        break;

                    case "*":
                        Console.WriteLine(num1 * num2);
                        break;

                    case "/":
                        if (num2 != 0)
                        {
                            Console.WriteLine(num1 / num2);
                        }
                        else
                        {
                            throw new ArgumentException("You can't divide by zero");
                        }
                        break;

                    default:
                        throw new ArgumentException("Invalid operation");
                }
            }
            catch (InvalidInputException exception)
            {
                Console.WriteLine("Please enter valid numbers");
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine("Invalid argument");

            }

        }
    }
}
