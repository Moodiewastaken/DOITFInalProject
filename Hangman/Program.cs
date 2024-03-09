using Hangman.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

class program
{
    static void Main()
    {
        List<string> words = new List<string>
        {
            "apple", "banana", "orange", "grape", "kiwi",
            "strawberry", "pineapple", "blueberry", "peach", "watermelon"
        };

        Random random = new Random();
        string word = words[random.Next(words.Count)];
        Console.WriteLine(word);

        try
        {

            Console.WriteLine("Welcome to the game! ");
            int attempts = 0;
            int maxattempts = 6;
            int correctguess = 0;

            while (attempts < maxattempts)
            {
                Console.WriteLine("Guess the letter: ");
                string guess = Console.ReadLine();

                if (char.TryParse(guess, out char letter) && char.IsLetter(letter))
                {
                    List<int> indexes = new List<int>();

                    for (int i = 0; i < word.Length; i++)
                    {
                        if (word[i] == letter)
                        {
                            indexes.Add(i);
                        }
                    }

                    if (indexes.Count > 0)
                    {
                        Console.WriteLine($"Correct! Indexes of the letter:");

                        foreach (int index in indexes)
                        {
                            Console.Write($"{index} ");
                        }

                        Console.WriteLine();
                        correctguess++;
                    }
                    else
                    {
                        Console.WriteLine("Wrong! Try again");
                    }
                }
                else
                {
                    throw new InvalidGuess();
                }

                attempts++;

                if (attempts >= maxattempts)
                {
                    if (correctguess > 0)
                    {
                        Console.WriteLine("Time to guess the word!: ");
                        string finalword = Console.ReadLine();

                        if (finalword == word)
                        {
                            Console.WriteLine("Congratulations! You won! ");
                        }
                        else
                        {
                            Console.WriteLine($"Incorrect! The word was: {word} ");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Game over!");
                    }
                }
            }

        }
        catch (InvalidGuess exception)
        {
            Console.WriteLine(exception.Message);
        }
    }
}

