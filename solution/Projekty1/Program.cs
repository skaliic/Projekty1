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
                Console.WriteLine(board[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}

