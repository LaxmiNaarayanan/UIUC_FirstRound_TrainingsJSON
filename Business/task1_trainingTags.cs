using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using UIUC_FirstRound_TrainingsJSON.Helper;
using UIUC_FirstRound_TrainingsJSON.Models;

namespace UIUC_FirstRound_TrainingsJSON.Business
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

            // Serialize trainingTags object to output1.json
            string jsonString = JsonSerializer.Serialize(trainingTags, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("C:\\Users\\laxmi\\Desktop\\Interviews\\UIUC\\UIUC_FirstRound_TrainingsJSON\\Output\\output1.json", jsonString);

            // Print Output1:
            Console.WriteLine("\nTraining Tags with their person count \n\n");
            foreach ((String tag, int ct) in trainingTags)
            {
                Console.WriteLine("Training Tag - " + tag + ", Count - " + ct + "\n");
            }
        }
    }
}
