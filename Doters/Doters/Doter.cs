using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Doters
{
    public class Doter
    {
        public int MMR;
        public int Hours;
        public string Name;
        public int Games;
        public int Wins;
        public double Winrate;
        public double MMRPH;
        public int Random(int firstnumber, int secondnumber)
        {
            Random rnd = new Random();
            int num = rnd.Next(firstnumber, secondnumber);
            return num;
        }
        public string DoterTry(int fileRow)
        {
            List<Doter> allCurrentDoters = DoterStorage.GetDoters();
            string line;
            if (allCurrentDoters[fileRow].MMR > 4000)
            {
                line = "He is an absolute gay.";
            }
            else if (allCurrentDoters[fileRow].Hours > 50000)
            {
                line = "He is definitely gay.";
            }
            else
            {
                line = "He is not gay, unfortunately.";
            }
            return line;
        }
        public string DoterEdit(int index,string name, int mmr, int wins, int hours)
        {
            string filePath = @"C:\Users\Zhenya\source\repos\Doters\doters\Doters\Doters\doters.txt";
            List<Doter> allCurrentDoters = DoterStorage.GetDoters();
            allCurrentDoters[index].MMR = mmr;
            allCurrentDoters[index].Hours = hours;
            allCurrentDoters[index].Name = name;
            if (wins > allCurrentDoters[index].Games)
            {
                return "Doter can't have more wins than games";
            }
            allCurrentDoters[index].Wins = wins;
            TxtFileProcessing fp = new TxtFileProcessing(filePath);
            List<string> lines = new List<string>();
            foreach (var item in allCurrentDoters)
            {
                lines.Add(item.DoterCharacteristics());
            }
            fp.Append(true, lines.ToArray());
            return "Doter number " + index + " has been edited.";

        }
        public string DoterInfo(bool fullstat = false)
        {
            int days = (Hours / 24);
            int months = (days / 30);
            int years = (months / 12);
            string dash = new string('-', 15);
            string line = "\n" + dash + "\n" + "Name: " + Name + "\n"
              + "MMR: " + MMR + "\n"
              + "Hours: " + years + " years " + months + " months " + days + " days " + "(" + Hours + " hours )" + "\n"
              + "Games: " + Games + "\n"
              + "Wins: " + Wins + "\n"
              + dash;
            if (fullstat)
            {
                line = "\n" + dash + "\n" + "Name: " + Name + "\n"
              + "MMR: " + MMR + "\n"
              + "Hours: " + years + " years " + months + " months " + days + " days " + "(" + Hours + " hours )" + "\n"
              + "Games: " + Games + "\n"
              + "Wins: " + Wins + "\n"
              + "Loses: " + (Games - Wins) + "\n"
              + "Winrate: " + Winrate + "%" + "\n"
              + "MMR/h: " + MMRPH + "\n"
              + dash;
                return line;
            }
            return line;
        }
        public List<string> DotersSort(int choice, int choice2)
        {
            List<Doter> allCurrentDoters = DoterStorage.GetDoters();
            List<string> toOutput = new List<string>();
            if (choice2 == 1)
            {
                switch (choice)
                {
                    case 1:
                        allCurrentDoters = allCurrentDoters.OrderBy(x => x.MMR).ToList();
                        break;
                    case 2:
                        allCurrentDoters = allCurrentDoters.OrderBy(x => x.Name).ToList();
                        break;
                    case 3:
                        allCurrentDoters = allCurrentDoters.OrderBy(x => x.Winrate).ToList();
                        break;
                }
            }
            else if (choice2 == 2) 
            {
                switch (choice)
                {
                    case 1:
                        allCurrentDoters = allCurrentDoters.OrderByDescending(x => x.MMR).ToList();
                        break;
                    case 2:
                        allCurrentDoters = allCurrentDoters.OrderByDescending(x => x.Name).ToList();
                        break;
                    case 3:
                        allCurrentDoters = allCurrentDoters.OrderByDescending(x => x.Winrate).ToList();
                        break;
                }
            }
            foreach (var item in allCurrentDoters)
            {
                toOutput.Add(item.DoterInfo(true));
            }
            return toOutput;
        
        }
        public string DoterManualCharacteristics(int mmr, int hours, string name, int games, int wins)
        {
            MMR = mmr; Hours = hours; Name = name; Games = games; Wins = wins;
            if (Wins > Games)
            {
                return "Doter can not have more wins than games.";
            }
            string line = MMR + "," + Hours + "," + Name + "," + Games + "," + Wins+ ",";
            return line;
        }
        public string DoterCharacteristics()
        {
            string line = MMR + "," + Hours + "," + Name + "," + Games + "," + Wins + ",";
            return line;
        }

        public void SetFromFileRow(string row, char delimiter = ',')
        {
            string[] datas = row.Split(delimiter);
            Wins = Convert.ToInt32(datas[4]);
            Games = Convert.ToInt32(datas[3]);
            Name = datas[2];
            Hours = Convert.ToInt32(datas[1]);
            MMR = Convert.ToInt32(datas[0]);
        }

        public void SetFromArray(string[] datas)
        {
            Wins = Convert.ToInt32(datas[4]);
            Games = Convert.ToInt32(datas[3]);
            Name = datas[2];
            Hours = Convert.ToInt32(datas[1]);
            MMR = Convert.ToInt32(datas[0]);
        }

        public Doter(string[] array)
        {
            SetFromArray(array);
            Winrate = (((double)Wins / (double)Games) * 100.0);
            Winrate = Math.Round(Winrate, 2);
        }

        //public Doter(string fileRow, char delimiter = ',')
        //{
        //    SetFromFileRow(fileRow, delimiter);
        //    Winrate = (((double)Wins / (double)Games) * 100.0);
        //    Winrate = Math.Round(Winrate, 2);
        //}
        public bool CompareDoter(Doter doterToCompare)
        {
           if (MMR == doterToCompare.MMR &&
                Hours == doterToCompare.Hours &&
                Name == doterToCompare.Name &&
                Games == doterToCompare.Games &&
                Wins == doterToCompare.Wins)
            {
                return true;
            }
           else
            {
                return false;
            }
            
        }
        

        public Doter(int counter)
        //public Doter(List<string> counter)
        {
            MMR = Random(0, 10000);
            Hours = Random(0, 100000);
            Name = "DoterTestoviy " + counter;
            Games = Random(0, 10000);
            Wins = Random(0, Games);
            Winrate = (((double)Wins / (double)Games) * 100.0);
            Winrate = Math.Round(Winrate, 2);
            MMRPH = ((double)MMR / (double)Hours);
            MMRPH = Math.Round(MMRPH, 3);
        }

    }

}
