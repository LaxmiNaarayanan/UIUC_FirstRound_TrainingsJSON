using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UIUC_FirstRound_TrainingsJSON.Helper;
using UIUC_FirstRound_TrainingsJSON.Models;

namespace UIUC_FirstRound_TrainingsJSON.Controller
{
    internal static class task2_peopleListForFiscalYear
    {
        public static void invokeTask2(List<PersonCompletions> Persons, List<string> trainingList, string FiscalYear)
        {
            List<string> trainingTagList = HelperClass.getTrainingTagList(Persons);

            /* A Dictionary with key = "Training Tag", Value = "list of people who are assigned 
             * with the training in the given Fiscal Year" */
            Dictionary<string, List<string>> trainingTagsPeopleList = new Dictionary<string, List<string>>();
            foreach (string trainingTag in trainingList)
            {
                trainingTagsPeopleList.Add(trainingTag, new List<string>());
            }

            foreach (PersonCompletions person in Persons)
            {
                Dictionary<string, Completion> visited = new Dictionary<string, Completion>();
                if (person?.completions != null)
                {
                    foreach (Completion completion in person.completions)
                    {
                        if (completion?.name != null && trainingList.Contains(completion.name) && completion?.timestamp != null)
                        {
                            if (visited.ContainsKey(completion.name) == true)
                            {
                                // To make sure only the most recent completion is considered
                                if (DateTime.Parse(completion.timestamp) > DateTime.Parse(visited[completion.name]?.timestamp))
                                {
                                    visited[completion.name] = completion;
                                }
                            }
                            else
                            {
                                visited.Add(completion.name, completion);
                            }
                        }
                    }
                    foreach ((string tag, Completion completion) in visited)
                    {
                        if (HelperClass.isPresentInSelectedFiscalYear(completion.timestamp) == true)
                        {
                            trainingTagsPeopleList.TryGetValue(completion.name, out List<string>? trainingTagPeopleList);
                            trainingTagPeopleList.Add(person.name);
                            trainingTagsPeopleList[completion.name] = trainingTagPeopleList;
                        }
                    }
                }
            }

            // Serialize trainingTagsPeopleList object to output2.json
            string jsonString = JsonSerializer.Serialize(trainingTagsPeopleList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("C:\\Users\\laxmi\\Desktop\\Interviews\\UIUC\\UIUC_FirstRound_TrainingsJSON\\Output\\output2.json", jsonString);

            // Print Output2:
            Console.WriteLine("\n\n Printing Training Tags with the People List for the Fiscal Year 2024  \n");
            foreach ((string tag, List<string> peopleList) in trainingTagsPeopleList)
            {
                Console.WriteLine("Training Tag - " + tag + ", People List - ");
                foreach (string people in peopleList)
                {
                    Console.Write(people + ", ");
                }
                Console.WriteLine("\n");
            }
        }
    }
}
