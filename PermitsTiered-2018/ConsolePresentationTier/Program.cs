﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessTier;

namespace ConsolePresentationTier
{
    class Program
    {
        static void Main(string[] args)
        {
            // using arrays to pass mutiple items since method arrays can only return 1 item
            string[] elementsToCheck = new string[2];  // array of data collected from the interface
            bool done = false;  // flag to keep us in the while loop until user is done
            while (!done)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Welcome to the Parking Permit Site");
                Console.WriteLine("If you would like to issue a parking permit, enter a p");
                Console.WriteLine("If you would like to see a report of all the issued permits, enter an r");
                Console.WriteLine("If you would like to exit, enter an e");
                Console.ResetColor();
                string userInput = Console.ReadLine();
                userInput.ToLower();  // handle both capital and lower letters
                Console.Clear();
                switch (userInput)
                {
                    case "p":  // add a new entry to the permits "database"
                        //Put values entered into array 
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("please enter your name:  ");
                        elementsToCheck[0] = Console.ReadLine(); // name;
                        Console.Write("please ener your zip code:  ");
                        elementsToCheck[1] = Console.ReadLine(); // zip;
                        // off course we could have used a wider array and collected more information
                        Console.ResetColor();
                        // call our static Class  (you do not instantiate a static Class, as there can only be 1 instance
                        // You just call its methods directly >>>>   ClassName.Method(params);
                        // this is a call to our middle tier
                        //  by using a param of "out" we are saying (1) varaible by ref, and 
                        // (2) the method should not expect it to have an initial value, it must set if before it uses it.

                        try
                        {
                            PermitBusiness.Validate(elementsToCheck);
                            Console.WriteLine("Your permit is recorded.");
                        }
                        catch (ApplicationException ex)
                        {

                            Console.WriteLine("Sorry, there was a problem processing your applicaiton");
                            Console.WriteLine(ex.Message);
                            Console.WriteLine();
                        }
                        break;

                    // User wants a report
                    case "r":
                        Console.WriteLine();
                        Console.Write("Enter the Admin Key: ");  // very serious security here  :-)
                        string key = Console.ReadLine();
                        // call another static method in the middle tier, which also returns an array, with all the data
                        string[,] data; // must be defined before we get in the Try, as I want to access out of the Try
                        try
                        {
                            data = BusinessTier.PermitBusiness.GetPermits(key);
                        }
                        catch (ApplicationException ex)
                        {
                            Console.WriteLine(ex.Message);
                            data = null;  // without this, compiler says I am using an unassigned variable below
                        }
                        
                        
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        // write out a header for our table of data
                        if (data != null)
                        {
                            Console.WriteLine("{0, -20}, {1,-7}, {2}", "USERNAME", "ZIP", "DATE ISSUED");
                            for (int i = 0; i < data.GetLength(0); i++)  // loop to write out all the data (including the empty cells)
                            {
                                Console.WriteLine("{0, -20}, {1,-7}, {2}", data[i, 0], data[i, 1], data[i, 2]);  // remember how to force column widths?
                            }

                        }
                        Console.ResetColor();
                        break;
                    case "q":  // accept either q or e for quit  or exit
                    case "e":
                        Console.WriteLine("Goodbye");
                        done = true;
                        break;
                    default:
                        Console.WriteLine("Not a valid input.");
                        break;
                }  // end of switch
            }  // end of while
            Console.ReadLine();
           
        } // end of Main()
    } // end of class
    
}
