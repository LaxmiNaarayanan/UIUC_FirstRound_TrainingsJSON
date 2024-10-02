// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using UIUC_FirstRound_TrainingsJSON.Business;
using UIUC_FirstRound_TrainingsJSON.Helper;
using UIUC_FirstRound_TrainingsJSON.Models;

namespace UIUC_FirstRound
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HelperClass.setFilePaths();
            // Set the Configuration Values: Default Values - { FY = "2024", Expiration Date = "Oct 1st, 2023" } 
            HelperClass.setConfigValue();
            
            // Read all data from training.txt
            string json = File.ReadAllText(HelperClass.trainingTextPath);
            List<PersonCompletions>? Persons = JsonSerializer.Deserialize<List<PersonCompletions>>(json);

            task1_trainingTags.invokeTask1(Persons);

            Console.WriteLine("Press any key to continue!");
            Console.ReadKey();

            List<string> trainingList = new List<string>() { "Electrical Safety for Labs", "X-Ray Safety", "Laboratory Safety Training" };
            task2_peopleListForFiscalYear.invokeTask2(Persons);

            Console.WriteLine("Press any key to continue!");
            Console.ReadKey();

            task3_expirationTag.invokeTask3(Persons);
        }
    }
}