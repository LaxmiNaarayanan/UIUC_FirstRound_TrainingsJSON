using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Dictionary<string, List<string>> trainingTagsPeopleList = new Dictionary<string, List<string>>();
            foreach (string trainingTag in trainingList) 
            {
                trainingTagsPeopleList.Add(trainingTag, new List<string>()); 
            }

            foreach (PersonCompletions person in Persons)
            {
                List<string> visited = new List<string>();
                if (person?.completions != null)
                {
                    foreach (Completion completion in person.completions)
                    {
                        if (completion?.name != null && trainingList.Contains(completion.name) && visited.Contains(completion.name) == false && completion?.timestamp != null)
                        {
                            visited.Add(completion.name);
                            if(HelperClass.isPresentInSelectedFiscalYear(completion.timestamp) == true)
                            {
                                trainingTagsPeopleList.TryGetValue(completion.name, out List<string>? trainingTagPeopleList);
                                trainingTagPeopleList.Add(person.name);
                                trainingTagsPeopleList[completion.name] = trainingTagPeopleList;
                            }
                        }
                    }
                }
            }

            Console.WriteLine(" Printing Training Tags with the People List for the Fiscal Year 2024  \n");
            foreach( (string tag, List<string> peopleList ) in trainingTagsPeopleList)
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
