namespace BaloonsPopGame
{
    using System;
    using System.Collections.Generic;
    using System.Text;


    public class GameEngine
    {
        private readonly Score scoreBoard;
        private readonly string difficulty;
        private int userMoves;
        private byte matrixRows;
        private byte matrixCols;

        public byte[,] Matrix { get; set; }

        /// <summary>
        /// Constructor of the class game engine, initialize key components 
        /// corresponding to the inputed difficulty
        /// </summary>
        public GameEngine(string difficulty)
        {
            this.scoreBoard = new Score();
            this.difficulty = difficulty;
            this.Matrix = GenerateMatrix();
            this.userMoves = 0;
        }

        /// <summary>
        /// Makes string image of the current state of the matrix which
        /// is used for printing it on the Console
        /// </summary>
        /// <returns>String representation of the matrix</returns>
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
            output.Append("\n");

            for (byte i = 0; i < this.matrixRows; i++)
            {
                output.Append(i + " | ");
                for (byte j = 0; j < this.matrixCols; j++)
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
                output.Append("\n");
            }

            output.Append("   ");
            for (byte column = 0; column < this.matrixCols * 2 + 1; column++)
            {
                output.Append("-");
            }
            output.Append("\n");

            return output.ToString();
        }

        /// <summary>
        /// Generates matrix with random values
        /// </summary>
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

        /// <summary>
        /// Checks if the the <paramref name="searchedItem"/> is equal to the
        /// item in the matrix at coordinates <paramref name="row"/> <paramref name="column"/>
        /// If the item in the matrix at those coordinates is the same
        /// this method calls recursively the method CheckNeighboringFields
        /// </summary>
        private void CheckField(int row, int column, int searchedItem)
        {
            //If index is out of the matrix stops recursion
            if (column >= this.matrixCols || row >= this.matrixRows
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

        /// <summary>
        /// Calls CheckFiled to all possible to pop directions
        /// </summary>
        private void CheckNeighboringFields(int row, int column, int searchedItem)
        {
            CheckField(row, column + 1, searchedItem);
            CheckField(row, column - 1, searchedItem);
            CheckField(row + 1, column, searchedItem);
            CheckField(row - 1, column, searchedItem);
        }

        /// <summary>
        /// Checks if the item at the position <paramref name="row"/> 
        /// <paramref name="col"/> is already popped
        /// </summary>
        /// <returns>Boolean value</returns>
        private bool IsPopped(int row, int col)
        {
            if (this.Matrix[row, col] == 0)
            {
                throw new ArgumentException("This baloon is popped!");
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if all the balloons in the matrix are popped
        /// </summary>
        /// <returns>Boolean value representing if you are winning</returns>
        private bool IsFinished()
        {
            bool isWinner = true;
            for (int j = 0; j < this.matrixCols; j++)
            {
                for (int i = 0; i < this.matrixRows; i++)
                {
                    if (this.Matrix[i, j] != 0)
                    {
                        isWinner = false;
                    }
                }
            }
            return isWinner;
        }

        /// <summary>
        /// Checks if the input is correct
        /// </summary>
        /// <param name="input">Current input</param>
        /// <returns>Boolean value</returns>
        private bool IsInputValid(string input)
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

        /// <summary>
        /// Drops down all balloons if ballons below them are popped
        /// </summary>
        private void DropDownMatrix()
        {
            Stack<byte> stack = new Stack<byte>();
            for (int j = 0; j < this.matrixCols; j++)
            {
                for (int i = 0; i < this.matrixRows; i++)
                {
                    if (this.Matrix[i, j] != 0)
                    {
                        stack.Push(this.Matrix[i, j]);
                    }
                }
                for (int k = this.matrixRows - 1; k >= 0; k--)
                {
                    if (stack.Count != 0)
                    {
                        this.Matrix[k, j] = stack.Pop();
                    }
                    else 
                    {
                        this.Matrix[k, j] = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Method for handling all game behaviour
        /// </summary>
        /// <param name="input">Current input</param>
        public void ProcessGame(string input)
        {
            if (input == "RESTART")
            {
                this.Matrix = GenerateMatrix();
                Console.WriteLine(GetMatrixImage());
                this.userMoves = 0;
            }
            else if (input == "TOP")
            {
                Console.WriteLine(scoreBoard.GetScoreBoard());
            }
            else
            {
                if (IsInputValid(input))
                {
                    int userRow = int.Parse(input[0].ToString());
                    if (userRow >= matrixRows)
                    {
                        throw new IndexOutOfRangeException("There is no such field!");
                    }

                    int userColumn = int.Parse(input[2].ToString());
                    if (!(IsPopped(userRow, userColumn)))
                    {
                        byte searchedTarget = this.Matrix[userRow, userColumn];
                        this.Matrix[userRow, userColumn] = 0;
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
                            Console.WriteLine(scoreBoard.GetScoreBoard());
                        }
                        else
                        {
                            Console.WriteLine("I'm sorry, you are not skillful enough for Top Five chart!");
                        }

                        this.Matrix = GenerateMatrix();
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
    }
}
