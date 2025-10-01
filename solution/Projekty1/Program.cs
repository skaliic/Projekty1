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

        char currentPlayer = 'x';
        int moves = 0;

        while (true)
        {
            Console.Clear();
            PrintBoard(board);

            Console.WriteLine($"Hráč {currentPlayer}, na tahu:");
            Console.Write("Zadej řádek (0-2): ");
            int row = int.Parse(Console.ReadLine());

            Console.Write("Zadej sloupec (0-2): ");
            int col = int.Parse(Console.ReadLine());

            if (board[row, col] == '.')
            {
                board[row, col] = currentPlayer;
                moves++;


                if (CheckWin(board, currentPlayer))
                {
                    Console.Clear();
                    PrintBoard(board);
                    Console.WriteLine($"Hráč {currentPlayer} vyhrál!");
                    break;
                }


                if (moves == 9)
                {
                    Console.Clear();
                    PrintBoard(board);
                    Console.WriteLine("Remíza!");
                    break;
                }


                currentPlayer = (currentPlayer == 'x') ? 'o' : 'x';
            }
            else
            {
                Console.WriteLine("Toto pole už je obsazené! Stiskni klávesu...");
                Console.ReadKey();
            }
        }
    }


    static void PrintBoard(char[,] board)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(board[i, j] + " ");
            }
            Console.WriteLine();
        }
    }


    static bool CheckWin(char[,] board, char player)
    {

        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player)
                return true;
            if (board[0, i] == player && board[1, i] == player && board[2, i] == player)
                return true;
        }


        if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player)
            return true;
        if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player)
            return true;

        return false;
    }
}
