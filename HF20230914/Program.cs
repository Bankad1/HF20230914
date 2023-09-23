/*using System;

namespace HF20230914
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. Feladat
            /*Console.WriteLine("Kérem, adjon meg egy számot:");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int szam))
            {
                for (int i = 0; i < szam; i++)
                {
                    Console.Write("o");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Hibás bemenet. Kérlek adj meg egy érvényes számot.");*/

            //2. Feladat

            Console.WriteLine("Kérem, adja meg a vektor elemszámát:");
            int elemszam = int.Parse(Console.ReadLine());

            
            /*int[] vektor = new int[elemszam];

            // Dominók lefedése
            bool sikeresLefedes = Lefedes(vektor);

            if (sikeresLefedes)
            {
                Console.WriteLine("A dominók lefedése sikeres!");
                Console.WriteLine("Végleges vektor:");
                for (int i = 0; i < elemszam; i++)
                {
                    Console.Write(vektor[i] + " ");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Nem sikerült a dominók lefedése.");
            }
        }

        static bool Lefedes(int[] vektor)
        {
            int elemszam = vektor.Length;

            while (true)
            {
                
                int elsoSzabad = -1;
                for (int i = 0; i < elemszam - 1; i++)
                {
                    if (vektor[i] == 0 && vektor[i + 1] == 0)
                    {
                        elsoSzabad = i;
                        break;
                    }
                }

                
                if (elsoSzabad == -1)
                    break;

              
                vektor[elsoSzabad] = 1;
                vektor[elsoSzabad + 1] = 1;
            }

        
            for (int i = 0; i < elemszam; i++)
            {
                if (vektor[i] == 0)
                    return false;
            }

            return true; // Sikeres lefedés
        }*/
//GYAK2
using System;
using System.Collections.Generic;
using System.IO;

namespace DominoSolver
{
    public struct Separator
    {
        private int cell1, cell2;

        public Separator(int cell1, int cell2)
        {
            this.cell1 = cell1;
            this.cell2 = cell2;
        }
    }

    public enum Orientation
    {
        Horizontal = 0,
        Vertical = 1
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            int rows, columns;
            Separator[] separators;
            ReadInput("input.txt", out rows, out columns, out separators);

            int[] vector = new int[rows * columns];

            StreamWriter file = new StreamWriter("solution.txt");
            for (int i = 0; i < Math.Pow(2, vector.Length / 2); i++)
            {
                for (int k = 0; k < vector.Length; k++) vector[k] = 0;

                int[,] solution = new int[vector.Length / 2, 2];
                bool possible = true;

                int j = 0;
                while (j < vector.Length / 2 && possible)
                {
                    Orientation orientation = (Orientation)(i >> j & 0b1);
                    int emptyIndex = FirstValue(vector, 0);
                    bool placeable = false;

                    while (emptyIndex < vector.Length && !placeable)
                    {
                        if (orientation == Orientation.Horizontal)
                        {
                            if (emptyIndex % columns < columns - 1 &&
                                vector[emptyIndex] == 0 &&
                                vector[emptyIndex + 1] == 0)
                            {
                                vector[emptyIndex] = 1;
                                vector[emptyIndex + 1] = 1;
                                placeable = true;
                            }
                            else emptyIndex++;
                        }
                        else
                        {
                            if (emptyIndex / columns < rows - 1 &&
                                vector[emptyIndex] == 0 &&
                                vector[emptyIndex + columns] == 0)
                            {
                                vector[emptyIndex] = 1;
                                vector[emptyIndex + columns] = 1;
                                placeable = true;
                            }
                            else emptyIndex++;
                        }
                    }
                    if (placeable)
                    {
                        solution[j, 0] = emptyIndex;
                        solution[j, 1] = (int)orientation;
                    }
                    else possible = false;

                    j++;
                }

                foreach (Separator separator in separators)
                {
                    j = 0;
                    while (j < solution.GetLength(0) && possible)
                    {
                        if (solution[j, 0] == separator[0] &&
                            ((Orientation)solution[j, 1] == Orientation.Horizontal) &&
                            solution[j, 0] + 1 == separator[1] ||
                            (Orientation)solution[j, 1] == Orientation.Vertical &&
                            solution[j, 0] + columns == separator[1])
                        {
                            possible = false;
                        }
                        j++;
                    }
                }

                if (possible)
                {
                    Console.WriteLine("Solution:");
                    Console.WriteLine(new string('-', 7));
                    file.WriteLine(new string('-', 7));
                    for (int k = 0; k < solution.GetLength(0); k++)
                    {
                        Console.WriteLine("| {0} {1} |", solution[k, 0], (Orientation)solution[k, 1] == Orientation.Horizontal ? solution[k, 0] + 1 : solution[k, 0] + columns);
                        file.WriteLine("| {0} {1} |", solution[k, 0], (Orientation)solution[k, 1] == Orientation.Horizontal ? solution[k, 0] + 1 : solution[k, 0] + columns);
                    }
                    Console.WriteLine(new string('-', 7));
                    file.WriteLine(new string('-', 7));
                }
            }
            file.Close();
        }

        static void ReadInput(string input, out int rows, out int columns, out Separator[] separators)
        {
            StreamReader sr = new StreamReader(input);

            string[] firstLine = sr.ReadLine().Split(' ');
            rows = Convert.ToInt32(firstLine[0]);
            columns = Convert.ToInt32(firstLine[1]);

            int separatorCount = Convert.ToInt32(sr.ReadLine());
            separators = new Separator[separatorCount];
            for (int i = 0; i < separatorCount; i++)
            {
                string[] line = sr.ReadLine().Split(' ');
                separators[i] = new Separator(Convert.ToInt32(line[0]), Convert.ToInt32(line[1]));
            }

            sr.Close();
        }

        static int FirstValue(int[] vector, int value)
        {
            int i = 0, index = -1;
            while (i < vector.Length && vector[i] != value)
            {
                i++;
            }
            if (i < vector.Length) index = i;

            return index;
        }
    }
}

    }
    }


