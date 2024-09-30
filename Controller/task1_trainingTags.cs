using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIUC_FirstRound_TrainingsJSON.Helper;
using UIUC_FirstRound_TrainingsJSON.Models;

namespace UIUC_FirstRound_TrainingsJSON.Controller
{
    internal static class task1_trainingTags
    {
        public static void invokeTask1(List<PersonCompletions> Persons)
        {
            List<string> trainingTagList = HelperClass.getTrainingTagList(Persons);

            // Contains all completions training tags along with its count
            Dictionary<string, int> trainingTags = new Dictionary<string, int>();
            foreach (string trainingTag in trainingTagList)
            {
                trainingTags.Add(trainingTag, 0);
            }

            // Iterate through each person and update the trainingTags count.
            foreach (PersonCompletions person in Persons)
            {
                // If there is some training tag listed more than once for a given person, still the people count of that trainingTag should be just incrimented by 1.
                List<string> visited = new List<string>();
                if (person?.completions != null)
                {
                    foreach (Completion completion in person.completions)
                    {
                        if (completion?.name != null && visited.Contains(completion.name) == false)
                        {
                            visited.Add(completion.name);
                            trainingTags.TryGetValue(completion.name, out int val);
                            trainingTags[completion.name] = val + 1;
                        }
                    }
                }
            }


            Console.WriteLine("Training Tags with their person count \n\n");
            foreach ((String tag, int ct) in trainingTags)
            {
                Console.WriteLine("Training Tag - " + tag + ", Count - " + ct + "\n");
            }
        }
    }
}
