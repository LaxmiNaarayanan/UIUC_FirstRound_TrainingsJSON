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

        // expiryDateLimit is read from the InputConfig.xml
        public static string expiryDateLimit = null;

        // set the trainings.txt file path in trainingTextPath
        public static string trainingTextPath = null;

        // set the InputConfig.xml file path in inputConfigFilePath
        public static string inputConfigFilePath = null;

        // set the baseDirectory path for the project
        public static string baseDirectory = null;

        // set the List of training tags from InputConfig.xml - for task2
        public static List<string> trainingList = new List<string>();

        // set file paths for trainings.txt and InputConfig.xml
        public static void setFilePaths()
        {
            // trainingTextPath = "C:\\Users\\laxmi\\source\\repos\\UIUC_FirstRound_TrainingsJSON\\InputFiles\\trainings.txt";
            baseDirectory = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\";
            trainingTextPath = baseDirectory + "trainings.txt";
            inputConfigFilePath = baseDirectory + "InputConfig.xml";
        }

        /* Set the Projects Configuration Values: Default Values - { FY = "2024", Expiration Date = "Oct 1st, 2023" }
         * Change the Default Values at InputConfig.xml
         */
        public static void setConfigValue()
        {
            XDocument inputConfig = XDocument.Load(inputConfigFilePath);
            XElement trainingConfigRoot = inputConfig.Element("trainingConfigRoot");
            XElement FYElement = trainingConfigRoot.Element("FY");
            XElement expiryDateLimitElement = trainingConfigRoot.Element("expiryDateLimit");

            XElement TrainingTags = trainingConfigRoot.Element("TrainingTags");
            foreach(XElement trainingTag in TrainingTags.Elements())
            {
                trainingList.Add(trainingTag.Value);
            }

            fiscalYear = FYElement.Value;
            expiryDateLimit = expiryDateLimitElement.Value;
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

        // Checks if the given expiryDate lies in the Fiscal Year
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

        // Get the Expiration Info in Enum for the given Completion_Expiry_Date
        public static expiration getExpirationInfo(string expiryDate)
        {
            if(expiryDate == null)
                return expiration.notExpiredOrNoExpirationAvailable;
            DateTime expiryDateLimitDT = DateTime.Parse(expiryDateLimit);
            DateTime expireSoonLowerLimitDT = new DateTime(2023, expiryDateLimitDT.Month - 1, 1);
            
            DateTime completionExpiryDate = DateTime.Parse(expiryDate);

            if (completionExpiryDate > expiryDateLimitDT)
            {
                return expiration.expired;
            }
            else if (completionExpiryDate < expiryDateLimitDT && completionExpiryDate > expireSoonLowerLimitDT)
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