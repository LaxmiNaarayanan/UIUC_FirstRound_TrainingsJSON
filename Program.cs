// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using UIUC_FirstRound_TrainingsJSON.Controller;
using UIUC_FirstRound_TrainingsJSON.Helper;
using UIUC_FirstRound_TrainingsJSON.Models;

namespace UIUC_FirstRound
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "C:\\Users\\laxmi\\source\\repos\\UIUC_FirstRound_TrainingsJSON\\InputFiles\\trainings.txt";
            string json = File.ReadAllText(filePath);
            List<PersonCompletions>? Persons = JsonSerializer.Deserialize<List<PersonCompletions>>(json);

            task1_trainingTags.invokeTask1(Persons);

            Console.WriteLine("Press any key to continue!");
            Console.ReadKey();

            List<string> trainingList = new List<string>() { "Electrical Safety for Labs", "X-Ray Safety", "Laboratory Safety Training" };
            task2_peopleListForFiscalYear.invokeTask2(Persons, trainingList, "2024");

            Console.WriteLine("Press any key to continue!");
            Console.ReadKey();

            task3_expirationTag.invokeTask3(Persons);

            //HelperClass.isPresentInSelectedFiscalYear("10/30/2023");

        }
    }
}