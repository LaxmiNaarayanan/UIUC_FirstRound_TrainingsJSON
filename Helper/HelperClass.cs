using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using UIUC_FirstRound_TrainingsJSON.Models;

namespace UIUC_FirstRound_TrainingsJSON.Helper
{
    enum expiration { expired = 0, expiresSoon = 1, notExpired = 2, noExpirationAvailable = 3 };
    internal static class HelperClass
    {
        
        // returns a list of all the training tags available
        public static List<string> getTrainingTagList(List<PersonCompletions> Persons)
        {
            // Contains all completions training tags along with its count
            List<string> trainingTags = new List<string>();

            foreach (PersonCompletions person in Persons)
            {
                if (person?.completions != null)
                {
                    foreach (Completion completion in person.completions)
                    {
                        if (completion?.name != null && !trainingTags.Contains(completion.name))
                        {
                            trainingTags.Add(completion.name);
                        }
                    }
                }

            }
            return trainingTags;
        }

        // Checks if the given date lies in the Fiscal Year
        public static bool isPresentInSelectedFiscalYear(string date)
        {
            DateTime startDate = new DateTime(2023, 7, 1);
            DateTime endDate = new DateTime(2024, 6, 30);
            DateTime selectedDate = DateTime.Parse(date);
            if (selectedDate >= startDate && selectedDate <= endDate)
            {
                return true;
            }
            return false;
        }

        // Get the Expiration Info in Enum for the given date
        public static expiration getExpirationInfo(string date)
        {
            XDocument inputConfig = XDocument.Load("C:\\Users\\laxmi\\Desktop\\Interviews\\UIUC\\UIUC_FirstRound_TrainingsJSON\\InputConfig.xml");
            //string val = inputConfig();
            DateTime expiryDate = new DateTime(2023, 10, 1);
            DateTime expireSoonDate = new DateTime(2023, 9, 1);
            if(date == null)
            {
                return expiration.noExpirationAvailable;
            }
            DateTime completionDate = DateTime.Parse(date);
            if (completionDate > expiryDate)
            {
                return expiration.expired;
            }
            else if (completionDate < expiryDate && completionDate > expireSoonDate)
            {
                return expiration.expiresSoon;
            }
            else
            {
                return expiration.notExpired;
            }
        }
    }
}