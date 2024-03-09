using GuessingGame.Exceptions;
using System.ComponentModel.Design;

Console.WriteLine("Enter difficulity, [easy] [medium] [hard] : ");

try
{
    string difficulty = Console.ReadLine();

    if (difficulty != "easy" && difficulty != "medium" && difficulty != "hard")
    {
        throw new InvalidDifficultyException();
    }


    Random random = new Random();

    int num = 0;

    if (difficulty == "easy")
    {
        num = random.Next(0, 15);
    }
    else if (difficulty == "medium")
    {
        num = random.Next(0, 25);
    }
    else if (difficulty == "hard")
    {
        num = random.Next(0, 50);
    }

    Console.WriteLine(num.ToString());

    int attempt = 0;
    int maxattempt = 10;

    while (attempt < maxattempt)
    {
        Console.WriteLine("guess: ");
        string guessedNumber = Console.ReadLine();
        if (int.TryParse(guessedNumber, out int guess))
        {

            try
            {
                if (guess != num)
                {
                    if (guess > num)
                    {
                        Console.WriteLine("Too high! Try again: ");
                    }
                    else if (guess < num)
                    {
                        Console.WriteLine("Too low! Try again: ");
                    }
                }
                else if (guess == num)
                {
                    Console.WriteLine("Correct");
                    break;
                }
                attempt++;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid Input!");
            }

            if (attempt >= maxattempt)
            {
                Console.WriteLine("Game over!");
            }
        }
        else
        {
            throw new InvalidInputException();
        }
    }


}
catch (InvalidDifficultyException exception)
{
    Console.WriteLine(exception.Message);
}

catch (InvalidInputException exception)
{
    Console.WriteLine(exception.Message);
}