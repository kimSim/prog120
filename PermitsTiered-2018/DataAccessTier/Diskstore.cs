using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DataAccessTier
{
    public class Diskstore
    {
        static string currentDocPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\hwarrayData.txt";


        internal static string[,] GetArrayData()
        {
            string[,] returnArray = new string[10, 3];

            StreamReader fileReader;
            fileReader = new StreamReader(currentDocPath);


            VerifyFileExist(10, 3);  // if there is no such file, create it
            // now read 4 lines and store each into array
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    returnArray[i, j] = fileReader.ReadLine();
                }
            }

            fileReader.Close();  // make sure to always close


            return returnArray;
        }

        internal static void SaveTheArray(string[,] fakeDB)
        {
            StreamWriter fileWriter;

            // call the constuctor
            fileWriter = new StreamWriter(currentDocPath);
            // write for lines to the file stream
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    fileWriter.WriteLine(fakeDB[i, j]);
                }
            }
            fileWriter.Close();  // make sure to always close!

        }

        public static bool VerifyFileExist(int rows, int columns)
        {
            try
            {
                if (File.Exists(currentDocPath))
                {
                    return true;
                }
                else
                {
                    CreateFile(rows, columns);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("We have a problem: " + ex.Message);
                return false;
            }

        }
        //------------------------------------------------------
        private static void CreateFile(int rows, int columns)
        {
            StreamWriter arrayCreator = new StreamWriter(currentDocPath);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    arrayCreator.WriteLine("");
                }
            }
            arrayCreator.Close();
        }
    }
}
