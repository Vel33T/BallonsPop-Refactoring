namespace BaloonsPopGame
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class BaloonsPopGame : Game
    {
        private static void CheckField(byte[,] matrix, int row, int column, int searchedItem)
        {
            //If index is out of the matrix stops recursion
            if (column >= Game.MATRIX_COLS || row >= Game.MATRIX_ROWS
                || column < 0 || row < 0)
            {
                return;
            }

            if (matrix[row, column] == searchedItem)
            {
                matrix[row, column] = 0;
                CheckNeighboringFields(matrix, row, column, searchedItem);
            }
            else
            {
                return;
            }
        }

        private static void CheckNeighboringFields(byte[,] matrix, int row, int column, int searchedItem)
        {
            CheckField(matrix, row, column + 1, searchedItem);
            CheckField(matrix, row, column - 1, searchedItem);
            CheckField(matrix, row + 1, column, searchedItem);
            CheckField(matrix, row - 1, column, searchedItem);
        }

        public static bool Change(byte[,] matrixToModify, int rowAtm, int columnAtm)
        {
            if (matrixToModify[rowAtm, columnAtm] == 0)
            {
                return true;
            }

            byte searchedTarget = matrixToModify[rowAtm, columnAtm];

            matrixToModify[rowAtm, columnAtm] = 0;
            CheckNeighboringFields(matrixToModify, rowAtm, columnAtm, searchedTarget);
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
            Console.WriteLine("-----------------------------------");


        }

        private static void SavePlayerPoints(string[,] chart, int points, int i)
        {
            Console.WriteLine("Please, insert your name:");
            string userName = Console.ReadLine();
            chart[i, 0] = points.ToString();
            chart[i, 1] = userName;
            StreamWriter sw = new StreamWriter("topFive.txt");
            for (int j = 0; j < chart.Length/2; j++)
            {
                if (chart[j, 1] == null)
                {
                    break;
                }
                sw.Write("%"+chart[j, 1] +"-"+ chart[j, 0]);
            }
            sw.Close();
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
                    SavePlayerPoints(chart, points, i);
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
                SavePlayerPoints(chart, points, worstMovesChartPosition);
                skilled = true;
            }
            return skilled;
        }

        private static void ProcessGame(string input, string[,] topFive, ref byte[,] matrix, ref int userMoves)
        {
            switch (input)
            {
                case "RESTART":
                    {
                        matrix = GenerateMatrix(Game.MATRIX_ROWS, Game.MATRIX_COLS);
                        PrintMatrix(matrix);
                        userMoves = 0;
                        break;
                    }
                case "TOP":
                    PrintScoreBoard(topFive);
                    break;
                default:
                    if ((input.Length == 3) && (input[0] >= '0' && input[0] <= '9') && (input[2] >= '0' && input[2] <= '9') && (input[1] == ' ' || input[1] == '.' || input[1] == ','))
                    {
                        int userRow, userColumn;
                        userRow = int.Parse(input[0].ToString());
                        if (userRow >= Game.MATRIX_ROWS)
                        {
                            throw new IndexOutOfRangeException("There is no such field!");
                        }
                        userColumn = int.Parse(input[2].ToString());

                        if (Change(matrix, userRow, userColumn))
                        {
                            throw new ArgumentException("This baloon is popped!");
                        }
                        userMoves++;
                        if (IsFinished(matrix))
                        {
                            Console.WriteLine("Great! You completed it in {0} moves.", userMoves);
                            if (SignIfSkilled(topFive, userMoves))
                            {
                                PrintScoreBoard(topFive);
                            }
                            else
                            {
                                Console.WriteLine("I'm sorry, you are not skillful enough for TopFive chart!");
                            }
                            matrix = GenerateMatrix(Game.MATRIX_ROWS, Game.MATRIX_COLS);
                            userMoves = 0;
                        }
                        PrintMatrix(matrix);
                        break;
                    }
                    else
                    {
                        throw new IndexOutOfRangeException("There is no such field! Try again!");
                    }
            }
        }

        public static void Main(string[] args)
        {
            //Load top five scores from file
            string topFiveUsers = File.ReadAllText("topFive.txt");
            string[] topfiveArray = topFiveUsers.Split('%');
            string[,] topFive = new string[5, 2];
            for (int i = 1; i < topfiveArray.Length; i++)
            {
                topFive[i-1,0] = topfiveArray[i].Split('-')[1];
                topFive[i-1, 1] = topfiveArray[i].Split('-')[0];
                if (i == 5)
                {
                    break;
                }
                
            }
            
            byte[,] matrix = GenerateMatrix(Game.MATRIX_ROWS, Game.MATRIX_COLS);
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

            Console.WriteLine("Good Bye!");
        }
    }
}
