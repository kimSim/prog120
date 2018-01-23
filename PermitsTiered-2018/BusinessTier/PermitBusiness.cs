using System;
using System.Collections;
using DataAccessTier;
using System.Data;

namespace BusinessTier
{
    // Summary description for Class1.

    public static class PermitBusiness
    {
        #region "_fields"
        //none, we won't store any state in this static class
        #endregion

        #region "constuctors"
        //none, let the system build a default
        #endregion

        // This method validates the string data 
        // <param "textToCheck">array contains the text strings passed in to be validated </param>
        // method is static since this is just a function, it makes no sense to create more than one copy, as
        // other objects can all share this this one.  Note that it holds no state

        public static void Validate(string[] textToCheck)
        {
            string userName = "";
            string zip = "";

            //Validate name, at least 4 but not longer than 20 characters
            if (textToCheck[0].Length >= 4 && textToCheck[0].Length <= 20)
            {
                userName = textToCheck[0];  // name is ok
            }
            else
            {
                throw new ApplicationException("The name must be at least 4 but not longer than 20 characters");
            }

            //Validate zip
            if (textToCheck[1].Length == 5) // making sure zip code is at least 5 characters, off course we could do better!
            {
                zip = textToCheck[1];  // zip is ok
            }
            else  // note we can report either or both errors
            {
                throw new ApplicationException("The zip code must be exactly 5 digits");
            }

            // use data tier to save data to database (array)
            try
            {
                PermitData.Save(userName, zip);  // calls data tier
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException("There was a problem recording data to the database: " + ex.Message);
            }
        }


        // Our 2nd Business tier static method, called by console (UI) to get all data, and it in turn calls the 3rd, data tier
        public static string[,] GetPermits(string userKey)
        {
            if (userKey == "11")  // our  simple validation
            {
                // if ok, make a call to our 3rd "data" tier
                // note the return from the data tier is the 10x3 matrix of data, so we call the tier, get it, and
                // then just pass it right back to the UI (console) tier
                return PermitData.GetApplications();
            }
            else
            {
                throw new ApplicationException("Invalid Password");
            }

        }
    }
}

