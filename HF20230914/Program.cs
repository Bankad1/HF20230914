using System;
using System.Collections.Generic;
using System.IO;

namespace DominoBacktrack
{
    public struct Separator
    {
        private int cell1, cell2;

        public Separator(int cell1, int cell2)
        {
            this.cell1 = cell1;
            this.cell2 = cell2;
        }

        public Separator(int[] cells)
        {
            if (cells.Length != 2)
                throw new ArgumentException("Length of array is not 2.", "cells");

            this.cell1 = cells[0];
            this.cell2 = cells[1];
        }

        public int this[int index]
        {
            get
            {
                if (index == 0)
                    return this.cell1;
                else if (index == 1)
                    return this.cell2;
                else
                    throw new IndexOutOfRangeException("Index must be 0 or 1");
            }
            set
            {
                if (index == 0)
                    this.cell1 = value;
                else if (index == 1)
                    this.cell2 = value;
                else
                    throw new IndexOutOfRangeException("Index must be 0 or 1");
            }
        }
    }

    public enum Orientation
    {
        Horizontal = 0,
        Vertical = 1
    }

    internal class Program
    {
        static int attemptCounter = 0;

        static void ReadInput(string inputFilePath, out int rows, out int columns, out Separator[] separators)
        {
            using (StreamReader reader = new StreamReader(inputFilePath))
            {
                string[] firstLine = reader.ReadLine().Split(' ');
                rows = Convert.ToInt32(firstLine[0]);
                columns = Convert.ToInt32(firstLine[1]);

                int numSeparators = Convert.ToInt32(reader.ReadLine());
                separators = new Separator[numSeparators];

                for (int i = 0; i < numSeparators; i++)
                {
                    string[] line = reader.ReadLine().Split(' ');
                    separators[i] = new Separator(Convert.ToInt32(line[0]), Convert.ToInt32(line[1]));
                }
            }
        }

        static bool IsValidPlacement(int[] vector, int index, int orientation, int rows, int columns)
        {
            if (orientation == (int)Orientation.Horizontal)
            {
                return index % columns < columns - 1 &&
                       vector[index] == 0 &&
                       vector[index + 1] == 0;
            }
            else
            {
                return index / columns < rows - 1 &&
                       vector[index] == 0 &&
                       vector[index + columns] == 0;
            }
        }

        static bool IsSeparatorBlocked(int[,] solution, Separator separator)
        {
            for (int j = 0; j < solution.GetLength(0); j++)
            {
                if (solution[j, 0] == separator[0] &&
                    ((Orientation)solution[j, 1] == Orientation.Horizontal) &&
                    solution[j, 0] + 1 == separator[1] ||
                    (Orientation)solution[j, 1] == Orientation.Vertical &&
                    solution[j, 0] + solution.GetLength(1) == separator[1])
                {
                    return true;
                }
            }
            return false;
        }

        static bool SolveDominoProblem(int[] vector, int[,] solution, Separator[] separators, int index, int rows, int columns)
        {
            if (index >= vector.Length)
                return true;

            attemptCounter++;

            for (int orientation = 0; orientation <= 1; orientation++)
            {
                if (IsValidPlacement(vector, index, orientation, rows, columns))
                {
                    vector[index] = 1;
                    int emptyIndex = -1;

                    for (int i = index + 1; i < vector.Length; i++)
                    {
                        if (vector[i] == 0)
                        {
                            emptyIndex = i;
                            break;
                        }
                    }

                    if (emptyIndex != -1)
                    {
                        vector[emptyIndex] = 1;
                        solution[index / 2, 0] = emptyIndex;
                        solution[index / 2, 1] = orientation;

                        bool blocked = false;
                        foreach (var separator in separators)
                        {
                            if (IsSeparatorBlocked(solution, separator))
                            {
                                blocked = true;
                                break;
                            }
                        }

                        if (!blocked && SolveDominoProblem(vector, solution, separators, emptyIndex + 1, rows, columns))
                            return true;

                        vector[emptyIndex] = 0;
                    }

                    vector[index] = 0;
                }
            }

            return false;
        }

        static void Main(string[] args)
        {
            int rows, columns;
            Separator[] separators;
            ReadInput("input_3.txt", out rows, out columns, out separators);

            int[] vector = new int[rows * columns];
            int[,] solution = new int[rows * columns / 2, 2];

            if (SolveDominoProblem(vector, solution, separators, 0, rows, columns))
            {
                using (StreamWriter writer = new StreamWriter("solution.txt"))
                {
                    writer.WriteLine("A solution is:");
                    writer.WriteLine(new string('—', 7));
                    for (int k = 0; k < solution.GetLength(0); k++)
                    {
                        writer.WriteLine("| {0} {1} |", solution[k, 0], (Orientation)solution[k, 1] == Orientation.Horizontal ? solution[k, 0] + 1 : solution[k, 0] + columns);
                    }
                    writer.WriteLine(new string('—', 7));
                }

                Console.WriteLine($"Solution found in {attemptCounter} attempts.");
            }
            else
            {
                Console.WriteLine("No solution found.");
            }

            Console.ReadLine();
        }
    }
}
