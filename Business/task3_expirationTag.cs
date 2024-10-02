using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using UIUC_FirstRound_TrainingsJSON.Helper;
using UIUC_FirstRound_TrainingsJSON.Models;

namespace UIUC_FirstRound_TrainingsJSON.Business
{
    internal static class task3_expirationTag
    {
        public static void invokeTask3(List<PersonCompletions> Persons)
        {
            List<string> trainingTagList = HelperClass.getTrainingTagList(Persons);

            // The Dictionary has Key = "Person Name", Value = "List of KeyValuePairs<tag, expirationStatus>"
            Dictionary<string, List<KeyValuePair<string, expiration>>> trainingExpirationList =
                new Dictionary<string, List<KeyValuePair<string, expiration>>>();

            foreach (PersonCompletions person in Persons)
            {
                Dictionary<string, Completion> visited = new Dictionary<string, Completion>();
                if (person?.completions != null)
                {
                    foreach (Completion completion in person.completions)
                    {
                        if (completion?.name != null && completion?.timestamp != null)
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
                    List<KeyValuePair<string, expiration>> expirationList = new List<KeyValuePair<string, expiration>>();
                    foreach ((string tag, Completion completion) in visited)
                    {
                        expiration expirationStatus = HelperClass.getExpirationInfo(completion.expires);
                        if (expirationStatus == expiration.notExpiredOrNoExpirationAvailable)
                            continue;

                        KeyValuePair<string, expiration> pair = new KeyValuePair<string, expiration>(tag, expirationStatus);
                        expirationList.Add(pair);
                    }
                    if(expirationList.Count > 0)
                    {
                        trainingExpirationList.Add(person.name, expirationList);
                    }
                }
            }

            // convert the KeyValuePair value to string, inorder to make the JSON readable of the expiration Info
            Dictionary<string, List<KeyValuePair<string, string>>> trainingExpirationListJSON = 
                new Dictionary<string, List<KeyValuePair<string, string>>>();
            foreach((string name, List<KeyValuePair<string, expiration>> expirationList) in trainingExpirationList)
            {
                List<KeyValuePair<string, string>> expirationListString = new List<KeyValuePair<string, string>>();
                foreach(KeyValuePair<string, expiration> pair in expirationList)
                {
                    expirationListString.Add(new KeyValuePair<string, string>(pair.Key, ((expiration)pair.Value).ToString()));
                }
                trainingExpirationListJSON.Add(name, expirationListString);
            }

            // Serialize trainingExpirationListJSON object to output3.json
            string jsonString = JsonSerializer.Serialize(trainingExpirationListJSON, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("C:\\Users\\laxmi\\Desktop\\Interviews\\UIUC\\UIUC_FirstRound_TrainingsJSON\\Output\\output3.json", jsonString);

            // Print Output3:
            Console.WriteLine("\n\n Peoples Completions with Expiration Info");
            foreach ((string name, List <KeyValuePair<string, expiration>> expirationList) in trainingExpirationList)
            {
                Console.WriteLine("Person_Name: " + name + " {");
                foreach(KeyValuePair<string, expiration> pair in expirationList)
                {
                    Console.WriteLine("\tTraining Tag : " + pair.Key + " - " + pair.Value);
                }
                Console.WriteLine("}\n");
            }
        }
    }
}
