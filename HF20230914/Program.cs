using System;

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

            
            int[] vektor = new int[elemszam];

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
        }

    }
    }


