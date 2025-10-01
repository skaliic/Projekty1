using System;

class Program
{
    static void Main()
    {
        string[] words = { "kolo", "počítač", "škola", "hudba", "python", "pes", "fotbal", "auto" };
        Random rand = new Random();
        string wordToGuess = words[rand.Next(words.Length)].ToLower();

        char[] guessedWord = new char[wordToGuess.Length];
        for (int i = 0; i < guessedWord.Length; i++)
            guessedWord[i] = '_';

        int wrongGuesses = 0;
        int maxWrongGuesses = 6;

        while (wrongGuesses < maxWrongGuesses && new string(guessedWord) != wordToGuess)
        {
            Console.Clear();
            Console.WriteLine("Šibenice");
            Console.WriteLine($"Slovo: {string.Join(" ", guessedWord)}");
            Console.WriteLine($"Chybné pokusy: {wrongGuesses}/{maxWrongGuesses}");
            Console.Write("Hádej písmeno: ");
            char guess = Char.ToLower(Console.ReadKey().KeyChar);
            Console.WriteLine();

            bool correct = false;
            for (int i = 0; i < wordToGuess.Length; i++)
            {
                if (wordToGuess[i] == guess)
                {
                    guessedWord[i] = guess;
                    correct = true;
                }
            }

            if (!correct)
                wrongGuesses++;
        }

        Console.Clear();
        if (new string(guessedWord) == wordToGuess)
            Console.WriteLine($"Super! Uhodl jsi slovo: {wordToGuess}");
        else
            Console.WriteLine($"Prohrál jsi! Slovo bylo: {wordToGuess}");
    }
}

