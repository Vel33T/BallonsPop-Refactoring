using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BaloonsPopGame
{
    public class BaloonsPopGameEngine
    {
        private string[,] TopFive { get; set; }
        private byte[,] Matrix { get; set; }
        private int UserMoves { get; set; }
        private byte MatrixRows { get; set; }
        private byte MatrixCols { get; set; }
        private string Difficulty { get; set; }

        public BaloonsPopGameEngine(string difficulty)
        {
            this.Difficulty = difficulty;
            if (difficulty == "Easy")
            {
                this.MatrixRows = 4;
                this.MatrixCols = 5;
            }
            else if (difficulty == "Medium")
            {
                this.MatrixRows = 6;
                this.MatrixCols = 8;
            }
            else if (difficulty == "Hard")
            {
                this.MatrixRows = 9;
                this.MatrixCols = 9;
            }
            //Load top five scores from file
            string path = "../../results/topFive" + difficulty + ".txt";
            string topFiveUsers = File.ReadAllText(path);
            string[] topfiveArray = topFiveUsers.Split('%');
            this.TopFive = new string[5, 2];
            for (int i = 1; i < topfiveArray.Length; i++)
            {
                this.TopFive[i - 1, 0] = topfiveArray[i].Split('-')[1];
                this.TopFive[i - 1, 1] = topfiveArray[i].Split('-')[0];
                if (i == 5)
                {
                    break;
                }

            }
            this.Matrix = GenerateMatrix();
            Console.WriteLine(GetMatrixImage());
            this.UserMoves = 0;
        }

        private string GetMatrixImage()
        {
            StringBuilder output = new StringBuilder();

            output.Append("    ");
            for (byte column = 0; column < this.MatrixCols; column++)
            {
                output.Append(column + " ");
            }
            output.Append("\n   ");
            for (byte column = 0; column < this.MatrixCols * 2 + 1; column++)
            {
                output.Append("-");
            }
            output.Append(Environment.NewLine);

            for (byte i = 0; i < this.MatrixRows; i++)
            {
                output.Append(i + " | ");
                for (byte j = 0; j < this.MatrixCols; j++)
                {
                    if (this.Matrix[i, j] == 0)
                    {
                        output.Append("  ");
                    }
                    else
                    {
                        output.Append(this.Matrix[i, j] + " ");
                    }
                }
                output.Append("| ");
                output.Append(Environment.NewLine);
            }

            output.Append("   ");
            for (byte column = 0; column < this.MatrixCols * 2 + 1; column++)
            {
                output.Append("-");
            }
            output.AppendLine();

            return output.ToString();
        }

        private byte[,] GenerateMatrix()
        {
            byte[,] matrix = new byte[this.MatrixRows, this.MatrixCols];
            Random randNumber = new Random();

            for (byte row = 0; row < this.MatrixRows; row++)
            {
                for (byte column = 0; column < this.MatrixCols; column++)
                {
                    byte tempByte = (byte)randNumber.Next(1, 5);
                    matrix[row, column] = tempByte;
                }
            }
            return matrix;
        }

        private void CheckField(int row, int column, int searchedItem)
        {
            //If index is out of the matrix stops recursion
            if (column >= this.MatrixCols || row >= this.MatrixRows
                || column < 0 || row < 0)
            {
                return;
            }

            if (this.Matrix[row, column] == searchedItem)
            {
                this.Matrix[row, column] = 0;
                CheckNeighboringFields(row, column, searchedItem);
            }
            else
            {
                return;
            }
        }

        private void CheckNeighboringFields(int row, int column, int searchedItem)
        {
            CheckField(row, column + 1, searchedItem);
            CheckField(row, column - 1, searchedItem);
            CheckField(row + 1, column, searchedItem);
            CheckField(row - 1, column, searchedItem);
        }

        private bool Change(int rowAtm, int columnAtm)
        {
            if (this.Matrix[rowAtm, columnAtm] == 0)
            {
                return true;
            }

            byte searchedTarget = this.Matrix[rowAtm, columnAtm];

            this.Matrix[rowAtm, columnAtm] = 0;
            CheckNeighboringFields(rowAtm, columnAtm, searchedTarget);
            return false;
        }

        private bool IsFinished()
        {
            bool isWinner = true;
            Stack<byte> stack = new Stack<byte>();
            for (int j = 0; j < this.MatrixCols; j++)
            {
                for (int i = 0; i < this.MatrixRows; i++)
                {
                    if (this.Matrix[i, j] != 0)
                    {
                        isWinner = false;
                        stack.Push(this.Matrix[i, j]);
                    }
                }
                for (int k = this.MatrixRows - 1; k >= 0; k--)
                {
                    try
                    {
                        this.Matrix[k, j] = stack.Pop();
                    }
                    catch (Exception)
                    {
                        this.Matrix[k, j] = 0;
                    }
                }
            }
            return isWinner;
        }

        public void PrintScoreBoard()
        {
            List<Score> scores = new List<Score>();

            for (int i = 0; i < 5; ++i)
            {
                if (this.TopFive[i, 0] == null)
                {
                    break;
                }

                scores.Add(new Score(int.Parse(this.TopFive[i, 0]), this.TopFive[i, 1]));
            }

            scores.Sort();

            Console.WriteLine("---------TOP FIVE SCORES-----------");
            for (int i = 0; i < scores.Count; ++i)
            {
                Console.WriteLine("{0}.   {1}", i + 1, scores[i]);
            }
            Console.WriteLine("-----------------------------------");


        }

        public void ProcessGame(string input)
        {
            switch (input)
            {
                case "RESTART":
                    {
                        this.Matrix = GenerateMatrix();
                        Console.WriteLine(GetMatrixImage());
                        this.UserMoves = 0;
                        break;
                    }
                case "TOP":
                    PrintScoreBoard();
                    break;
                default:
                    if ((input.Length == 3) && (input[0] >= '0' && input[0] <= '9') && (input[2] >= '0' && input[2] <= '9') && (input[1] == ' ' || input[1] == '.' || input[1] == ','))
                    {
                        int userRow, userColumn;
                        userRow = int.Parse(input[0].ToString());
                        if (userRow >= MatrixRows)
                        {
                            throw new IndexOutOfRangeException("There is no such field!");
                        }
                        userColumn = int.Parse(input[2].ToString());

                        if (Change(userRow, userColumn))
                        {
                            throw new ArgumentException("This baloon is popped!");
                        }
                        this.UserMoves++;
                        if (IsFinished())
                        {
                            Console.WriteLine("Great! You completed it in {0} moves.", this.UserMoves);
                            if (Score.SignIfSkilled(this.TopFive, this.UserMoves, this.Difficulty))
                            {
                                PrintScoreBoard();
                            }
                            else
                            {
                                Console.WriteLine("I'm sorry, you are not skillful enough for Top Five chart!");
                            }
                            this.Matrix = GenerateMatrix();
                            this.UserMoves = 0;
                        }
                        Console.WriteLine(GetMatrixImage());
                        break;
                    }
                    else
                    {
                        throw new IndexOutOfRangeException("There is no such field! Try again!");
                    }
            }
        }
    }
}
