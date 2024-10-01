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
    enum expiration { expired = 0, expiresSoon = 1, notExpiredOrNoExpirationAvailable = 2 };
    internal static class HelperClass
    {
        // Fiscal Year is read from the InputConfig.xml
        public static string fiscalYear = null;

        // expirationDate is read from the InputConfig.xml
        public static string expirationDate = null;

        /* Set the Projects Configuration Values: Default Values - { FY = "2024", Expiration Date = "Oct 1st, 2023" }
         * Change the Default Values at InputConfig.xml
         */
        public static void setConfigValue()
        {
            XDocument inputConfig = XDocument.Load("C:\\Users\\laxmi\\Desktop\\Interviews\\UIUC\\UIUC_FirstRound_TrainingsJSON\\InputConfig.xml");
            XElement trainingConfigRoot = inputConfig.Element("trainingConfigRoot");
            XElement FYElement = trainingConfigRoot.Element("FY");
            XElement ExpirationDateElement = trainingConfigRoot.Element("ExpirationDate");

            fiscalYear = FYElement.Value;
            expirationDate = ExpirationDateElement.Value;
        }
        
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
            int FY = int.Parse(fiscalYear);
            DateTime startDate = new DateTime(FY - 1, 7, 1);
            DateTime endDate = new DateTime(FY, 6, 30);
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
            if(date == null)
                return expiration.notExpiredOrNoExpirationAvailable;
            DateTime expiryDate = DateTime.Parse(expirationDate);
            DateTime expireSoonDate = new DateTime(2023, expiryDate.Month - 1, 1);
            
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
                return expiration.notExpiredOrNoExpirationAvailable;
            }
        }

    }
}