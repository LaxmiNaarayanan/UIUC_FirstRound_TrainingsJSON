using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIUC_FirstRound_TrainingsJSON.Models;

namespace UIUC_FirstRound_TrainingsJSON.Helper
{
    internal static class HelperClass
    {
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

        public static bool isPresentInSelectedFiscalYear(string date)
        {
            DateTime startDate = new DateTime(2023, 7, 1);
            DateTime endDate = new DateTime(2024, 6, 30);
            DateTime selectedDate = DateTime.Parse(date);
            if(selectedDate >= startDate && selectedDate <= endDate )
            {
                return true;
            }
            return false;
        }
    }
}
