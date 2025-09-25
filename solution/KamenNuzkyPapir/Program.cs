using System;

class Program
{
    enum Choice { Kamen = 1, Nuzky = 2, Papir = 3 }

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("=== Kámen, nůžky, papír ===");

        while (true)
        {
            int bestOf = AskBestOf();
            int neededToWin = bestOf / 2 + 1;

            int playerScore = 0;
            int compScore = 0;
            int round = 0;
            Random rng = new Random();

            while (playerScore < neededToWin && compScore < neededToWin)
            {
                round++;
                Console.WriteLine($"\nKolo {round} — skóre: Ty {playerScore} : {compScore} PC");
                Choice? playerChoice = AskPlayerChoice();
                if (playerChoice == null)
                {
                    // hráč zadal "q" => konec hry úplně
                    Console.WriteLine("Ukončuji hru. Čau!");
                    return;
                }

                Choice compChoice = (Choice)rng.Next(1, 4);
                Console.WriteLine($"Počítač zvolil: {compChoice}");

                int result = DecideRound(playerChoice.Value, compChoice);
                if (result == 0)
                {
                    Console.WriteLine("Remíza.");
                }
                else if (result == 1)
                {
                    Console.WriteLine("Vyhrál jsi toto kolo!");
                    playerScore++;
                }
                else
                {
                    Console.WriteLine("Prohrál jsi toto kolo.");
                    compScore++;
                }
            }

            Console.WriteLine($"\nKONEC SERIE — konečné skóre: Ty {playerScore} : {compScore}");
            if (playerScore > compScore)
                Console.WriteLine("Gratuluju, vyhrál jsi sérii!");
            else
                Console.WriteLine("Počítač vyhrál sérii. Zkus to znova!");

            Console.Write("\nChceš hrát znovu? (a = ano, n = ne): ");
            string again = Console.ReadLine()?.Trim().ToLower() ?? "n";
            if (again != "a" && again != "y")
            {
                Console.WriteLine("Dík za hraní — měj se!");
                break;
            }
        }
    }

    // Zeptá se hráče, jakou sérii chce hrát (best of 1/3/5/7...).
    static int AskBestOf()
    {
        while (true)
        {
            Console.Write("Zadej 'best of' (1, 3, 5, 7) nebo enter pro 3: ");
            string input = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(input)) return 3;
            if (int.TryParse(input, out int n) && n > 0 && n % 2 == 1 && n <= 15)
                return n;
            Console.WriteLine("Neplatná volba — zadej liché číslo (např. 1,3,5,7). Maximálně 15.");
        }
    }

    // Zeptá se hráče na volbu, vrátí null pokud hráč chce ukončit ("q")
    static Choice? AskPlayerChoice()
    {
        Console.WriteLine("Zadej volbu: (1) Kámen, (2) Nůžky, (3) Papír. Nebo 'q' pro ukončení.");
        while (true)
        {
            Console.Write("Tvoje volba: ");
            string input = Console.ReadLine()?.Trim().ToLower();
            if (string.IsNullOrEmpty(input)) continue;
            if (input == "q" || input == "quit") return null;

            // akceptuj čísla i slova
            if (int.TryParse(input, out int num) && num >= 1 && num <= 3)
                return (Choice)num;

            if (input.StartsWith("k")) return Choice.Kamen;
            if (input.StartsWith("n") || input.StartsWith("ň")) return Choice.Nuzky;
            if (input.StartsWith("p")) return Choice.Papir;

            Console.WriteLine("Neplatný vstup — napiš 1/2/3 nebo kámen/nůžky/papír (či 'q' pro konec).");
        }
    }

    // Vrací 0 = remíza, 1 = hráč vyhrál, -1 = počítač vyhrál
    static int DecideRound(Choice player, Choice comp)
    {
        if (player == comp) return 0;

        // Kámen(1) porazí Nůžky(2)
        // Nůžky(2) porazí Papír(3)
        // Papír(3) porazí Kámen(1)
        if ((player == Choice.Kamen && comp == Choice.Nuzky) ||
            (player == Choice.Nuzky && comp == Choice.Papir) ||
            (player == Choice.Papir && comp == Choice.Kamen))
            return 1;
        else
            return -1;
    }
}

