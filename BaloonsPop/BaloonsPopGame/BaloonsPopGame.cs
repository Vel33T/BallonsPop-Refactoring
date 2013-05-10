namespace BaloonsPopGame
{
    using System;
    using System.Collections.Generic;

    public class BaloonsPopGame : Game
    {
        private static void CheckLeft(byte[,] matrix, int row, int column, int searchedItem)
        {
            int newRow = row;
            int newColumn = column - 1;

            //If index is out of the matrix stops recursion
            if (newColumn >= Game.MATRIX_COLS || newRow >= Game.MATRIX_ROWS
                || newColumn < 0 || newRow < 0 )
            {
                return;
            }

            if (matrix[newRow, newColumn] == searchedItem)
            {
                matrix[newRow, newColumn] = 0;
                CheckLeft(matrix, newRow, newColumn, searchedItem);
            }
            else
            {
                return;
            }
        }

        public static void CheckRight(byte[,] matrix, int row, int column, int searchedItem)
        {
            int newRow = row;
            int newColumn = column + 1;

            //If index is out of the matrix stops recursion
            if (newColumn >= Game.MATRIX_COLS || newRow >= Game.MATRIX_ROWS
                || newColumn < 0 || newRow < 0)
            {
                return;
            }

            if (matrix[newRow, newColumn] == searchedItem)
            {
                matrix[newRow, newColumn] = 0;
                CheckRight(matrix, newRow, newColumn, searchedItem);
            }
            else
            {
                return;
            }
        }
        public static void CheckUp(byte[,] matrix, int row, int column, int searchedItem)
        {
            int newRow = row + 1;
            int newColumn = column;
            
            //If index is out of the matrix stops recursion
            if (newColumn >= Game.MATRIX_COLS || newRow >= Game.MATRIX_ROWS
                || newColumn < 0 || newRow < 0)
            {
                return;
            }

            if (matrix[newRow, newColumn] == searchedItem)
            {
                matrix[newRow, newColumn] = 0;
                CheckUp(matrix, newRow, newColumn, searchedItem);
            }
            else
            {
                return;
            }
        }

        public static void CheckDown(byte[,] matrix, int row, int column, int searchedItem)
        {
            int newRow = row - 1;
            int newColumn = column;

            //If index is out of the matrix stops recursion
            if (newColumn >= Game.MATRIX_COLS || newRow >= Game.MATRIX_ROWS
                || newColumn < 0 || newRow < 0)
            {
                return;
            }

            if (matrix[newRow, newColumn] == searchedItem)
            {
                matrix[newRow, newColumn] = 0;
                CheckDown(matrix, newRow, newColumn, searchedItem);
            }
            else
            {
                return;
            }
        }
        public static bool Change(byte[,] matrixToModify, int rowAtm, int columnAtm)
        {
            if (matrixToModify[rowAtm, columnAtm] == 0)
            {
                return true;
            }

            byte searchedTarget = matrixToModify[rowAtm, columnAtm];

            matrixToModify[rowAtm, columnAtm] = 0;
            CheckLeft(matrixToModify, rowAtm, columnAtm, searchedTarget);
            CheckRight(matrixToModify, rowAtm, columnAtm, searchedTarget);
            CheckUp(matrixToModify, rowAtm, columnAtm, searchedTarget);
            CheckDown(matrixToModify, rowAtm, columnAtm, searchedTarget);

            return false;
        }

        public static bool IsFinished(byte[,] matrix)
        {
            bool isWinner = true;
            Stack<byte> stack = new Stack<byte>();
            for (int j = 0; j < Game.MATRIX_COLS; j++)
            {
                for (int i = 0; i < Game.MATRIX_ROWS; i++)
                {
                    if (matrix[i, j] != 0)
                    {
                        isWinner = false;
                        stack.Push(matrix[i, j]);
                    }
                }
                for (int k = Game.MATRIX_ROWS - 1; k >= 0; k--)
                {
                    try
                    {
                        matrix[k, j] = stack.Pop();
                    }
                    catch (Exception)
                    {
                        matrix[k, j] = 0;
                    }
                }
            }
            return isWinner;
        }

        public static void PrintScoreBoard(string[,] tableToSort)
        {
            List<Score> scores = new List<Score>();

            for (int i = 0; i < 5; ++i)
            {
                if (tableToSort[i, 0] == null)
                {
                    break;
                }

                scores.Add(new Score(int.Parse(tableToSort[i, 0]), tableToSort[i, 1]));
            }

            scores.Sort();

            Console.WriteLine("---------TOP FIVE SCORES-----------");
            for (int i = 0; i < scores.Count; ++i)
            {
                Console.WriteLine("{0}.   {1}", i + 1, scores[i]);
            }
            Console.WriteLine("----------------------------------");


        }

        public static bool SignIfSkilled(string[,] chart, int points)
        {
            bool skilled = false;
            int worstMoves = 0;
            int worstMovesChartPosition = 0;
            for (int i = 0; i < 5; i++)
            {
                if (chart[i, 0] == null)
                {
                    Console.WriteLine("Type in your name.");
                    string tempUserName = Console.ReadLine();
                    chart[i, 0] = points.ToString();
                    chart[i, 1] = tempUserName;
                    skilled = true;
                    break;
                }
            }
            if (skilled == false)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (int.Parse(chart[i, 0]) > worstMoves)
                    {
                        worstMovesChartPosition = i;
                        worstMoves = int.Parse(chart[i, 0]);
                    }
                }
            }
            if (points < worstMoves && skilled == false)
            {
                Console.WriteLine("Type in your name.");
                string tempUserName = Console.ReadLine();
                chart[worstMovesChartPosition, 0] = points.ToString();
                chart[worstMovesChartPosition, 1] = tempUserName;
                skilled = true;
            }
            return skilled;
        }

        private static void ProcessGame(string temp, string[,] topFive, ref byte[,] matrix, ref int userMoves)
        {
            switch (temp)
            {
                case "RESTART":
                    matrix = GenerateMatrix(Game.MATRIX_ROWS, Game.MATRIX_COLS);
                    PrintMatrix(matrix);
                    userMoves = 0;
                    break;
                case "TOP":
                    PrintScoreBoard(topFive);
                    break;
                default:
                    if ((temp.Length == 3) && (temp[0] >= '0' && temp[0] <= '9') && (temp[2] >= '0' && temp[2] <= '9') && (temp[1] == ' ' || temp[1] == '.' || temp[1] == ','))
                    {
                        int userRow, userColumn;
                        userRow = int.Parse(temp[0].ToString());
                        if (userRow >= Game.MATRIX_ROWS)
                        {
                            throw new IndexOutOfRangeException("There are no such field!");
                        }
                        userColumn = int.Parse(temp[2].ToString());

                        if (Change(matrix, userRow, userColumn))
                        {
                            throw new ArgumentException("This baloon is popped!");
                        }
                        userMoves++;
                        if (IsFinished(matrix))
                        {
                            Console.WriteLine("Gratz ! You completed it in {0} moves.", userMoves);
                            if (SignIfSkilled(topFive, userMoves))
                            {
                                PrintScoreBoard(topFive);
                            }
                            else
                            {
                                Console.WriteLine("I am sorry you are not skillful enough for TopFive chart!");
                            }
                            matrix = GenerateMatrix(Game.MATRIX_ROWS, Game.MATRIX_COLS);
                            userMoves = 0;
                        }
                        PrintMatrix(matrix);
                        break;
                    }
                    else
                    {
                        throw new IndexOutOfRangeException("There are no such field! Try again!");
                    }
            }
        }

        public static void Main(string[] args)
        {
            string[,] topFive = new string[5, 2];
            byte[,] matrix = GenerateMatrix(5, 10);

            PrintMatrix(matrix);
            string temp = null;
            int userMoves = 0;

            Console.WriteLine("Enter a row and column: ");
            temp = Console.ReadLine();
            do
            {
                temp = temp.ToUpper().Trim();
                try
                {
                    ProcessGame(temp, topFive, ref matrix, ref userMoves);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine("Enter a row and column: ");
                temp = Console.ReadLine();
            }
            while (temp != "EXIT");

            Console.WriteLine("Good Bye! ");
        }
    }
}
