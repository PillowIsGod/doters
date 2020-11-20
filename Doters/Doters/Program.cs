using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Doters
{

    class Program
    {
        static int GetUserResponse(string question, bool needTocheckRange = false,
            int minRange = 0, int maxRange = 10, string errorMessage = "")
        {
            int responseNumber = 0;

            while (responseNumber == 0)
            {

                ToLog(question);
                string response = ToRead();
                if (int.TryParse(response, out responseNumber))
                {
                    if (needTocheckRange)
                    {
                        if (responseNumber < minRange || responseNumber > maxRange)
                        {
                            ToLog(errorMessage);

                            responseNumber = 0;
                            continue;
                        }
                    }

                    break;
                }
                else
                {
                    ToLog("Please, write a number.");
                }

                responseNumber = 0;
            }
            return responseNumber;
        }

        static string CasesToUsersChoice(int choice)
        {
            List<string> lines = dotersProcessing.GetAllFileLines();

            List<Doter> allCurrentDoters = DoterStorage.GetDoters();
            
            Doter doter = new Doter(lines.Count + 1);


            string caseAnswer = "";
            switch (choice)
            {
                case 1:
                    List<string> toOutput = new List<string>();
                    foreach (var item in allCurrentDoters)
                    {
                        toOutput.Add(item.DoterInfo());
                    }
                    caseAnswer = CoutLines(toOutput, 1, 0);
                    break;
                case 2:
                    int mmr = GetUserResponse("Please, enter mmr: ");
                    int hours = GetUserResponse("Please, enter hours: ");
                    ToLog("Please, enter the name of the doter");
                    string name = ToRead();
                    int games = GetUserResponse("Please, enter games: ");
                    int wins = GetUserResponse("Please, enter wins: ");

                    string[] doterData = new string[]
                    {
                        mmr.ToString(),
                        hours.ToString(),
                        name,
                        games.ToString(),
                        wins.ToString()
                    };

                    Doter doter1 = new Doter(doterData);

                    DoterStorage.AddDoter(false, doter1);
                    //DoterStorage.AddDoter(mmr, hours, name, games, wins, 0);
                    caseAnswer = "Entered doter was added to the file";
                    break;
                case 3:
                    int number ;
                    string name2;
                    int affirmation = GetUserResponse("Would you really like to delete doter? \n " +
                        "1 - to confirm, 2 - to decline");
                    if (affirmation == 1)
                    {
                        int choiceofINTorMMR = GetUserResponse("Would you like to delete a doter by: \n 1. His list number 2. His mmr 3. His name");
                        if (choiceofINTorMMR == 1)
                        {
                            number = GetUserResponse("Which doter would you like to delete? \n" +
                                "Enter his index: ") - 1;
                            DoterStorage.RemoveDoter(allCurrentDoters[number]);
                        }
                        else if (choiceofINTorMMR == 2)
                        {
                            number = GetUserResponse("Which doter would you like to delete?\n" +
                             "Enter his mmr:");

                            List<Doter> gottedDotersByMmr = DoterStorage.GetDotersByMmr(number);
                            if (gottedDotersByMmr.Count > 0)
                                DoterStorage.RemoveDoter(gottedDotersByMmr[0]);

                        }
                        else if (choiceofINTorMMR == 3)
                        {
                            ToLog("Please, enter the name of the doter you'd like to delete: ");
                            name2 = ToRead();
                            List<Doter> gotDotersByName = DoterStorage.GetDotersByName(name2);
                            DoterStorage.RemoveDoter(gotDotersByName[0]);
                        }
                    }
                    else
                    {
                        break;
                    }
                    break;
                case 4:
                    int filerow = GetUserResponse("Please, select the doter to be tested by index or mmr:");
                    caseAnswer = doter.DoterTry(filerow);
                    break;
                case 5:
                    var userresponse1 = GetUserResponse("Please, select the doter, whose mmr per hours you want to see: ") - 1;
                    caseAnswer = "MMR per hours of the doter number " + (userresponse1 + 1) + "is " + allCurrentDoters[userresponse1].MMRPH;
                    break;
                case 6:
                    int userresponse = GetUserResponse("Please, select the doter, whose winrate you want to see: ") - 1;
                    caseAnswer = "Winrate of doter " + (userresponse + 1) + ": " + allCurrentDoters[userresponse].Winrate;
                    break;
                case 7:
                    var userresponse2 = GetUserResponse("Please, select the doter, whose full stats you want to see: ") - 1;
                    caseAnswer = allCurrentDoters[userresponse2].DoterInfo(true);
                    break;
                case 8:
                    int choice3 = GetUserResponse("How many doters would you like to see? \n" +
                        "1. All doters 2. Chosen amount");
                    int amount = 0;
                    if (choice3 == 2)
                    {
                        amount = GetUserResponse("Please, enter the amount of doters to output: ");
                    }

                    List<string> toOutput1 = new List<string>();
                    foreach (var item in allCurrentDoters)
                    {
                        toOutput1.Add(item.DoterInfo(true));
                    }
                    caseAnswer = CoutLines(toOutput1, choice3, amount);
                    break;
                case 9:
                    int amount1 = GetUserResponse("Please, enter the amount of doters you want to add");
                    Doter[] randomDoters = DoterStorage.CreateRandomDomers(amount1).ToArray();
                    DoterStorage.AddDoter(false, randomDoters);
                    //DoterStorage.AddDoter(0, 0, "", 0, 0, amount1, true);
                    break;
                case 10:
                    int affirmation1 = GetUserResponse("Would you really like to delete all doters from file? \n" +
                        " 1 - to confirm, 2 - return to menu", true, 1, 2, "Please, choose one of the variants.");
                    if (affirmation1 == 1)
                    {

                        DoterStorage.RemoveDoter(allCurrentDoters.ToArray());
                    }
                    else if (affirmation1 == 2)
                    {
                        break;
                    }
                    break;
                case 11:
                    int choice1 = GetUserResponse("Please, choose: \n 1. Sort doters by MMR; 2. Sort doters by Name; 3. Sort doters by winrate;");
                    int choice2 = GetUserResponse("Would you like to sort by: \n 1. Descending 2. Ascending");
                    caseAnswer = CoutLines(doter.DotersSort(choice1, choice2), 1, 0);
                    break;
                case 12:
                    int index = GetUserResponse("Please, enter the number of doter you want to edit.");
                    ToLog("Please, enter the new name to the doter: ");
                    string name1 = ToRead();
                    int mmr1 = GetUserResponse("Please, enter the new mmr of the doter: ");
                    int wins1 = GetUserResponse("Please, enter the amount of wins of the doter: ");
                    int hours1 = GetUserResponse("Please, enter the amount of doter's hours: ");
                    caseAnswer = allCurrentDoters[index].DoterEdit(index, name1, mmr1, wins1, hours1);
                    break;
                case 13:
                    xmlProcessing.TransferDotersToXML();
                    break;
                case 14:
                    jsonProcessing.DataToJson();
                    break;
                default:
                    caseAnswer = ("Wrong choice, please try again.");
                    break;
            }
            return caseAnswer;
        }

        static string CoutLines(List<string> fileContent, int choice, int amount, string delimiter = "\r\n")
        {
            string generatedOutput = "";
            if (choice == 1)
            {
                for (int i = 0; i < fileContent.Count; i++)
                {
                    generatedOutput += ((i + 1) + "." + fileContent[i] + delimiter);
                }
                return generatedOutput;
            }
            else if (choice == 2)
            {
                for (int i = 0; i < amount; i++)
                {
                    generatedOutput += ((i + 1) + "." + fileContent[i] + delimiter);
                }
                return generatedOutput;
            }
            return generatedOutput;
        }
        static void ToLog(string text)
        {
            Console.WriteLine(text);
        }
        static string ToRead()
        {
            string inputText = Console.ReadLine();
            return inputText;
        }
        static TxtFileProcessing menuItemsPath;
        static TxtFileProcessing dotersProcessing;
        static XMLDoter xmlProcessing;
        static JsonFile jsonProcessing;
        public static void Main(string[] args)
        {
            string dotersPath = @"C:\Users\Zhenya\source\repos\Doters\doters\Doters\Doters\doters.txt";
            string pathMenu = @"C:\Users\Zhenya\source\repos\Doters\doters\Doters\Doters\Menu.txt";
            string xmlPath = @"C:\Users\Zhenya\source\repos\Doters\doters\Doters\Doters\doters.xml";
            string jsonPath = @"C:\Users\Zhenya\source\repos\Doters\doters\Doters\Doters\JsonContent.json";
            xmlProcessing = new XMLDoter(xmlPath);
            menuItemsPath = new TxtFileProcessing(pathMenu);
            dotersProcessing = new TxtFileProcessing(dotersPath);
            jsonProcessing = new JsonFile(jsonPath);
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            List<string> menuList = menuItemsPath.GetAllFileLines();
            if (!File.Exists(pathMenu))
            {
                menuItemsPath.Append(true,
                    "Посмотреть дотеров", "Добавить дотера", "Удалить дотера", "Проверить дотера(Гей или натурал)",
"Показать кол - во полученного MMR в час", "Показать винрейт",
"Показать полную статистику определенного", "Показать полную статистику всех дотеров",
"Заполнить файл указанным количеством дотеров > 0 < 1000", "Очистить дотеров", "Отсортировать всех дотеров",
"Редактировать дотера", "All data to XML", "All data to JSon");
            }
            int generalchoice = GetUserResponse("What would you like to use as a data container: \n" +
"1. Txt file 2. XML file", true, 1, 2, "Please, choose one of the options.");


            switch (generalchoice)
            {
                case 1:
                    DoterStorage.DataStorage = StorageEnum.Txt;
                    break;
                case 2:
                    DoterStorage.DataStorage = StorageEnum.XML;
                    break;
                case 3:
                    DoterStorage.DataStorage = StorageEnum.Json;
                    break;
            }


            if (menuList == null || menuList.Count == 0)
            {
                ToLog("Bratishka - file is empty or not exist");
                return;
            }
            string generatedMenu = CoutLines(menuList, 1, 0);
            ToLog(generatedMenu);
            if (!File.Exists(dotersPath))
            {
                Doter[] randomDoters = DoterStorage.CreateRandomDomers(10).ToArray();
                DoterStorage.AddDoter(false, randomDoters);
            }

            int choice = 0;
            while (choice != -1)
            {
                var usersNumberChoice = GetUserResponse("Please, choose the element of the menu.", true, 1, 14, "В меню нет такого пункта.");
                Console.Clear();
                ToLog(generatedMenu);
                var answerToUser = CasesToUsersChoice(usersNumberChoice);
                ToLog(answerToUser);
            }
        }
    }
}


