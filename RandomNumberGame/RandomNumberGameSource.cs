// See https://aka.ms/new-console-template for more information
using System;
using System.Runtime.InteropServices;

internal class Program
{
    public static void Main(string[] args)
    {

        Random a = new Random();

        int b = a.Next(6);

        Console.WriteLine("Guess the number 1 - 5: {0}", b);

        for (int i = 1; i <= 5; i++)
        {
            int guess = Convert.ToInt32(Console.ReadLine());

            if (i >= 5)
            {
                Console.WriteLine("\nYou ran out of attempts. Better luck next time!");
            }

            else if (guess < b)
            {
                Console.WriteLine("\nYour guess was to low!!!\n");

            }
            else if (guess > b)
            {
                Console.WriteLine("\nYour guess was to high!!!\n");

            }

            else if (guess == b)
            {
                Console.WriteLine("\nCONGRATUALTIONS YOU WIN!!!!");

                break;
            }

            

        }

        


    }

 }
