using System;
using System.Data.OleDb;
using System.Data;

namespace DataAccessTier
{
    // Store the data

    public static class PermitData
    {
        // static fields
        // instead of a real database, we our going to store the data in a simple array
        // which would lose all the data every time the program is stopped!!
        // but you will imrove this with your homework
        private static string[,] fakeDB = new string[10, 3];  // hold ten entries 
        //private static string[,] fakeDB = new string[3, 3];  // test with just 3 entries 
        private static int index = 0;  // this index will be used to keep track of which new element of the db (array) we should add
                                       // because it is static, we can depend on it to hold a real value between calls, as there is only one array and one pointer


        // static method  called by middle tier, used to add another row of data
        public static void Save(string userName, string zip)
        {
            fakeDB = Diskstore.GetArrayData();  // overiding data --

            //---------------------adjusting index----------------------------------------------
            index = 0; //set the index back to 0, and now figure out what it should be
            for (int i = 0; i < 10; i++)  // find the first empty slot, and set index to point to it
            {
                if (fakeDB[i, 0] == null)
                {
                    break;  //if the file has never been written to, value may be null
                }
                else if (fakeDB[i, 0].Length < 2)
                {
                    break;  //or maybe its an empty string, like ""
                }
                else
                {
                    index = index + 1;  // bump up the index until we find an empty row

                }
            }
            //-----------------------------------------------------------------------------------


            if (index < 10)
            //if (index < 10)  // test with just 3 entries 
            {
                fakeDB[index, 0] = userName;
                fakeDB[index, 1] = zip;
                fakeDB[index, 2] = DateTime.Now.ToShortDateString();   // adding a time stamp from our DB
                index = index + 1;  // bump our pointer

                Diskstore.SaveTheArray(fakeDB);
                return;
            }
            else
            {
                throw new ApplicationException("Database is full.");
            }
        }

        // our 2nd  method in our 3rd tier
        // retrieves a list of applications (permits) from the TestPermit "database"
        // returns the array holding all data from the applications table
        // static method
        public static string[,] GetApplications()
        {
            // instead of a real SQL or file system operation, we just return our array
            return fakeDB;
        }

    }
}
