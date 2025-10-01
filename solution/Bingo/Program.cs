using System;

class Program
{
    static void Main()
    {
        Random rand = new Random();
        int numberToGuess = rand.Next(1, 51); // číslo od 1 do 50
        int attempts = 0;
        int maxAttempts = 10; // můžeš změnit nebo nechat neomezeně

        Console.WriteLine("Vítej ve hře Bingo! Hádej číslo od 1 do 50.");

        while (true)
        {
            Console.Write("Zadej číslo: ");
            int playerGuess;
            bool isNumber = int.TryParse(Console.ReadLine(), out playerGuess);

            if (!isNumber)
            {
                Console.WriteLine("Zadej platné číslo!");
                continue;
            }

            attempts++;

            if (playerGuess == numberToGuess)
            {
                Console.WriteLine($"Gratulace! Uhodl jsi číslo {numberToGuess} na {attempts}. pokus.");
                break;
            }
            else if (playerGuess < numberToGuess)
            {
                Console.WriteLine("Hledané číslo je větší.");
            }
            else
            {
                Console.WriteLine("Hledané číslo je menší.");
            }

            if (attempts >= maxAttempts)
            {
                Console.WriteLine($"Prohrál jsi! Správné číslo bylo {numberToGuess}.");
                break;
            }
        }
    }
}

