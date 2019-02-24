using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Program
    {
        // Creating players
        static string[] player = { "Player 1", "Player 2" };
        static string[] mark = { "O", "X" };
        static int position = 0;

        static int winner;

        static void Main(string[] args)
        {
            //Creating a field
            string[,] ticTacArray = new string[3, 3];
            int num = 0;
            // Check whether it is no win (no win if nobody won until 9 moves)
            int playerMoves;

            // Infinite loop, quit only if broken
            while (true)
            {
                // Crating an array of {0, ..., 9} and drawing it, also initial conditions
                ticTacArray = CreateField();
                DrawField(ticTacArray);
                playerMoves = 0;

                while (!IsWinningConditions(ticTacArray))
                {
                    // For loop for players turns
                    for (num = 0; num <= 1; num++) 
                    {
                        // Let the player type a position until it is correct - do-while loop
                        do
                        {
                            position = PlayerTurn(player[num], mark[num]);
                            if (IsPositionAvailable(position, ticTacArray))
                            {
                                ticTacArray[Row, Column] = mark[num];
                                playerMoves++;
                                DrawField(ticTacArray);
                            }
                        }
                        while (IsPositionAvailable(position, ticTacArray) || playerMoves == 9);
                        if (IsWinningConditions(ticTacArray) || playerMoves == 9) break;
                    }
                }

                // Checks if somebody won the match
                if (playerMoves == 9)
                {
                    Console.WriteLine("Nobody won! Type 'exit' to exit or any other sequence to repeat!");
                }
                else
                {
                    winner = num;
                    Console.WriteLine("Congratulations! {0} won ({1} ones)! Type 'exit' to exit or any other key to repeat!",
                        player[winner], mark[winner]);
                }

                // Decision: to play again or to quit?
                string key = Console.ReadLine().ToString();

                if (key.Equals("exit") || key.Equals("'exit'")) break;
            }
        }
        
        // Row property - transforming position to a row of matrix
        public static int Row
        {
            get
            {
                return (position <= 3 ? 0 : position <= 6 ? 1 : 2);
            }
        }

        // Column property - transforming position to a column of matrix
        public static int Column
        {
            get
            {
                return (position % 3 == 1 ? 0 : position % 3 == 2 ? 1 : 2);
            }
        }

        // Creating a 3x3 matrix for Tic Tac Toe game, numerations from 1 to 9
        static string[,] CreateField()
        {
            string[,] field = new string[3, 3];

            for(int i = 0; i <= 2; i++)
            {
                for(int j = 0; j <= 2; j++)
                {
                    field[i, j] = (i * 3 + j + 1).ToString();
                }
            }

            return field;
        }

        // Drawing a field having a matrix
        static void DrawField(string[,] field)
        {
            Console.Clear();
            Console.WriteLine("   |   |   ");
            Console.WriteLine(" {0} | {1} | {2} ", field[0, 0], field[0, 1], field[0, 2]);
            Console.WriteLine("___|___|___");
            Console.WriteLine("   |   |   ");
            Console.WriteLine(" {0} | {1} | {2} ", field[1, 0], field[1, 1], field[1, 2]);
            Console.WriteLine("___|___|___");
            Console.WriteLine("   |   |   ");
            Console.WriteLine(" {0} | {1} | {2} ", field[2, 0], field[2, 1], field[2, 2]);
            Console.WriteLine("   |   |   ");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        // Player must input a correct position which will be marked with "O" or "X"
        static int PlayerTurn(string playerName, string character)
        {
            int position = 0;

            Console.Write("{0}, choose your field! ", playerName);
            int.TryParse(Console.ReadLine(), out position);

            while (position < 1 && position > 9)
            {
                Console.Write("Invalid input! Please select your field, {0}! ", playerName);
                int.TryParse(Console.ReadLine(), out position);
            }

            return position;
        }

        // Converting position to row and column in matrix
        static string PositionToMatrix(int position, string[,] matrix)
        {
            int row = 0, column = 0;
            switch (position)
            {
                case 1: row = 0; column = 0; break;
                case 2: row = 0; column = 1; break;
                case 3: row = 0; column = 2; break;
                case 4: row = 1; column = 0; break;
                case 5: row = 1; column = 1; break;
                case 6: row = 1; column = 2; break;
                case 7: row = 2; column = 0; break;
                case 8: row = 2; column = 1; break;
                case 9: row = 2; column = 2; break;
            }

            return matrix[row, column];
        }


        // Checks whether in this position is a number or "O" / "X"
        static bool IsPositionAvailable(int position, string[,] matrix)
        {
            int.TryParse(PositionToMatrix(position, matrix), out int checkMatrixPosition);
            if (checkMatrixPosition == position)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static string[,] ChangeMatrix(string[,] matrix)
        {
            return matrix;
        }

        // Check whether there are conditions for one player to win
        static bool IsWinningConditions(string[,] matrix)
        {
            bool isWin = false;
            for (int i = 0; i <= 2; i++)
            {
                // Winning condition 1: the same sign in one of the columns
                if (matrix[i, 0].Equals(matrix[i, 1]) && matrix[i, 1].Equals(matrix[i, 2]))
                {
                    isWin = true;
                }
                // Winning condition 2: the same sign in one of the rows
                else if (matrix[0, i].Equals(matrix[1, i]) && matrix[1, i].Equals(matrix[2, i]))
                {
                    isWin = true;
                }
            }
            //Winning condition 3: check diagonals
            if ( (matrix[0, 0].Equals(matrix[1, 1]) && matrix[1, 1].Equals(matrix[2, 2]) )
                || (matrix[2, 0].Equals(matrix[1, 1]) && matrix[1, 1].Equals(matrix[0, 2])) )
            {
                isWin = true;
            }

            return isWin;

        }
    }
}
