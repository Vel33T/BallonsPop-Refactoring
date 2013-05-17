namespace BaloonsPopGame
{
    using System;
    using System.Collections.Generic;
    using System.Text;


    public class GameEngine
    {
        private Score scoreBoard;
        private byte[,] matrix;
        private int userMoves;
        private byte matrixRows;
        private byte matrixCols;
        private string difficulty;

        public GameEngine(string difficulty)
        {
            this.scoreBoard = new Score();
            this.difficulty = difficulty;
            this.matrix = GenerateMatrix();
            this.userMoves = 0;
        }

        public string GetMatrixImage()
        {
            StringBuilder output = new StringBuilder();

            output.Append("    ");
            for (byte column = 0; column < this.matrixCols; column++)
            {
                output.Append(column + " ");
            }
            output.Append("\n   ");
            for (byte column = 0; column < this.matrixCols * 2 + 1; column++)
            {
                output.Append("-");
            }
            output.Append(Environment.NewLine);

            for (byte i = 0; i < this.matrixRows; i++)
            {
                output.Append(i + " | ");
                for (byte j = 0; j < this.matrixCols; j++)
                {
                    if (this.matrix[i, j] == 0)
                    {
                        output.Append("  ");
                    }
                    else
                    {
                        output.Append(this.matrix[i, j] + " ");
                    }
                }
                output.Append("| ");
                output.Append(Environment.NewLine);
            }

            output.Append("   ");
            for (byte column = 0; column < this.matrixCols * 2 + 1; column++)
            {
                output.Append("-");
            }
            output.AppendLine();

            return output.ToString();
        }

        private byte[,] GenerateMatrix()
        {
            if (this.difficulty == "easy")
            {
                this.matrixRows = 4;
                this.matrixCols = 5;
            }
            else if (this.difficulty == "medium")
            {
                this.matrixRows = 6;
                this.matrixCols = 8;
            }
            else if (this.difficulty == "hard")
            {
                this.matrixRows = 9;
                this.matrixCols = 9;
            }

            byte[,] matrix = new byte[this.matrixRows, this.matrixCols];
            Random randNumber = new Random();

            for (byte row = 0; row < this.matrixRows; row++)
            {
                for (byte column = 0; column < this.matrixCols; column++)
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
            if (column >= this.matrixCols || row >= this.matrixRows
                || column < 0 || row < 0)
            {
                return;
            }

            if (this.matrix[row, column] == searchedItem)
            {
                this.matrix[row, column] = 0;
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

        private bool IsPopped(int rowAtm, int columnAtm)
        {
            if (this.matrix[rowAtm, columnAtm] == 0)
            {
                throw new ArgumentException("This baloon is popped!");
            }
            else
            {
                return false;
            }
        }

        private bool IsFinished()
        {
            bool isWinner = true;
            for (int j = 0; j < this.matrixCols; j++)
            {
                for (int i = 0; i < this.matrixRows; i++)
                {
                    if (this.matrix[i, j] != 0)
                    {
                        isWinner = false;
                    }
                }
            }
            return isWinner;
        }

        private void DropDownMatrix()
        {
            Stack<byte> stack = new Stack<byte>();
            for (int j = 0; j < this.matrixCols; j++)
            {
                for (int i = 0; i < this.matrixRows; i++)
                {
                    if (this.matrix[i, j] != 0)
                    {
                        stack.Push(this.matrix[i, j]);
                    }
                }
                for (int k = this.matrixRows - 1; k >= 0; k--)
                {
                    if (stack.Count != 0)
                    {
                        this.matrix[k, j] = stack.Pop();
                    }
                    else 
                    {
                        this.matrix[k, j] = 0;
                    }
                }
            }
        }

        public void ProcessGame(string input)
        {
            if (input == "RESTART")
            {
                this.matrix = GenerateMatrix();
                Console.WriteLine(GetMatrixImage());
                this.userMoves = 0;
            }
            else if (input == "TOP")
            {
                scoreBoard.PrintScoreBoard();
            }
            else
            {
                if (isInputValid(input))
                {
                    int userRow = int.Parse(input[0].ToString());
                    if (userRow >= matrixRows)
                    {
                        throw new IndexOutOfRangeException("There is no such field!");
                    }

                    int userColumn = int.Parse(input[2].ToString());
                    if (!(IsPopped(userRow, userColumn)))
                    {
                        byte searchedTarget = this.matrix[userRow, userColumn];
                        this.matrix[userRow, userColumn] = 0;
                        CheckNeighboringFields(userRow, userColumn, searchedTarget);
                        DropDownMatrix();
                    }

                    this.userMoves++;

                    if (IsFinished())
                    {
                        Console.WriteLine("Great! You completed it in {0} moves.", this.userMoves);
                        if (scoreBoard.IsGoodEnough(this.userMoves))
                        {
                            Console.WriteLine("Enter your name: ");
                            string playerName = Console.ReadLine();
                            scoreBoard.AddPlayer(playerName, this.userMoves);
                            scoreBoard.Sort();
                            scoreBoard.PrintScoreBoard();
                        }
                        else
                        {
                            Console.WriteLine("I'm sorry, you are not skillful enough for Top Five chart!");
                        }

                        this.matrix = GenerateMatrix();
                        this.userMoves = 0;
                    }
                    Console.WriteLine(GetMatrixImage());
                }
                else
                {
                    throw new IndexOutOfRangeException("There is no such field! Try again!");
                }
            }
        }

        private bool isInputValid(string input)
        {
            if ((input.Length == 3) && (input[0] >= '0' && input[0] <= '9') && (input[2] >= '0' && input[2] <= '9') &&
                (input[1] == ' ' || input[1] == '.' || input[1] == ','))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
