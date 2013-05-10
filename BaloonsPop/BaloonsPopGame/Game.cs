namespace BaloonsPopGame
{
    using System;
    using System.Text;

    public class Game
    {
        //The size of the field
        public const int MATRIX_ROWS=5;
        public const int MATRIX_COLS=10;

        public static void PrintMatrix(byte[,] matrix)
        {
            StringBuilder output = new StringBuilder();

            output.Append("    ");
            for (byte column = 0; column < matrix.GetLongLength(1); column++)
            {
                output.Append(column + " ");
            }
            output.Append("\n   ");
            for (byte column = 0; column < matrix.GetLongLength(1) * 2 + 1; column++)
            {
                output.Append("-");
            }
            output.Append(Environment.NewLine);

            for (byte i = 0; i < matrix.GetLongLength(0); i++)
            {
                output.Append(i + " | ");
                for (byte j = 0; j < matrix.GetLongLength(1); j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        output.Append("  ");
                    }
                    else
                    {
                        output.Append(matrix[i, j] + " "); 
                    }
                }
                output.Append("| ");
                output.Append(Environment.NewLine); 
            }

            output.Append("   ");
            for (byte column = 0; column < matrix.GetLongLength(1) * 2 + 1; column++)
            {
                output.Append("-");
            }
            output.AppendLine();

            Console.WriteLine(output);
        }


        public static byte[,] GenerateMatrix(byte rows, byte columns)
        {
            byte[,] matrix = new byte[rows, columns];
            Random randNumber = new Random();

            for (byte row = 0; row < rows; row++)
            {
                for (byte column = 0; column < columns; column++)
                {
                    byte tempByte = (byte)randNumber.Next(1, 5);
                    matrix[row, column] = tempByte;
                }
            }
            return matrix;
        }
    }
}
