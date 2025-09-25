using System;

class Program
{
    static void Main()
    {
        char[,] board = new char[3, 3];

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i, j] = '.';
            }
        }

        for (int i = 0; i < 3; i++)
        {
            for(int j = 0;j < 3; j++)
            {
                Console.Write(board[i, j] + " ");
            }
            Console.WriteLine();
        }


        Console.Write("Zadej řádek (0, 2): ");
        int row = int.Parse(Console.ReadLine());

        Console.Write("Zadej sloupec (0, 2): ");
        int col = int.Parse(Console.ReadLine());

        if (board[row, col] == '.')
        {
            board[row, col] = 'x';
        }

        Console.WriteLine();


    }
}

