# UIUC_FirstRound_TrainingsJSON

Business folder - has three classes containing the functionaties for the tasks
1) task1_trainingTags.cs - List each training Tags from the completions List with a count of 
   how many people have been assigned with that training tag. 
   -> Store the result in output1.json
2) task2_peopleListForFiscalYear.cs - For a list of mentioned training tags 
   {Trainings DEFAULT_VALUE = "Electrical Safety for Labs", "X-Ray Safety", "Laboratory Safety Training"} and 
    a given Fiscal Year { Fiscal Year DEFAULT_VALUE = 2024 }
   -> list all people that are assigned with these training tags in the specified fiscal year.
   -> Store the result in output2.json
3) task3_expirationTag.cs - find all people that have any assingned training completions that have been already 
   expired, or will expire within one month of the specified date
   {expiry date DEFAULT_VALUE =  Oct 1st, 2023}
   -> Store the result in output3.json
	
Models - model class to store the trainings.txt data
       - 1) Completion - Contains all the information related to a single completion
	   - 2) PersonCompletions - Contains all the Completions assigned to a given person
 
HelperClass - Contains all the helper funtions needed to execute the different tasks

Output Folfer - Has output files in JSON formate for each task.

InputConfig.XML - Has all the config data for the tasks 
				- 1) FY - Fiscal Year for Task2
				- 2) TrainingTags - Training Tags List for Task2
				- 3) expiryDateLimit - expiry Date Limit for Task3

Program.cs - Main Driver Class

trainings.txt - Input Data File containing the Persons Completions data