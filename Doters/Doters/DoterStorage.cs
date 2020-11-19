using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Doters
{
    public static class DoterStorage
    {
        private static string _storageFilePath { get; set; }
        private static TxtFileProcessing _txtfileprocessing;
        private static XMLDoter _xmlfileProcessing;
        public static bool XmlOrTXT = false;
        private static string _xmlfilepath { get; set; }

        static DoterStorage()
        {
            _storageFilePath = @"C:\Users\Zhenya\source\repos\Doters\doters\Doters\Doters\doters.txt";

            _txtfileprocessing = new TxtFileProcessing(_storageFilePath);
            _xmlfilepath = @"C:\Users\Zhenya\source\repos\Doters\doters\Doters\Doters\doters.xml";
            _xmlfileProcessing = new XMLDoter(_xmlfilepath);
        }


        public static List<Doter> CreateRandomDomers(int amount = 1)
        {
            if (amount <= 0)
                return null;

            List<Doter> doters = new List<Doter>();

            for (int i = 0; i < amount; i++)
            {
                doters.Add(new Doter(i));
            }

            return doters;
        }


        public static void AddDoter(bool rewrite = false, params Doter[] doters)
        {

            List<string> lines = new List<string>();
            if (XmlOrTXT == false)
            {
                foreach (var doter in doters)
                {
                    lines.Add(doter.DoterCharacteristics());

                }
                _txtfileprocessing.AppendFile(rewrite, lines.ToArray());
            }
            if (XmlOrTXT)
            {
                List<Doter> lines1 = new List<Doter>();
                foreach (var doter in doters)
                {
                    lines1.Add(doter);                  
                }
                _xmlfileProcessing.DoterAddToXml(rewrite, lines1.ToArray());
            }
        }

        public static void RemoveDoter(params Doter[] doters)
        {
            List<Doter> allDoters = GetDoters();

            for (int i = allDoters.Count-1; i != -1; i--)
            {
                foreach (var item in doters)
                {
                    if (allDoters[i].CompareDoter(item))
                    {
                        allDoters.RemoveAt(i);
                        break;
                    }
                }
               
                   
                //foreach (var item2 in allDoters)
                //{
                //    if (item == item2)
                //    {
                //        allDoters.Remove(item2);
                //    }
                //}                
            }

            AddDoter(true, allDoters.ToArray());
        }
        public static List<Doter> GetDotersByMmr(int mmr)
        {
            List<Doter> allDoter = GetDoters();

            for (int i = allDoter.Count-1; i != -1; i--)
            {
                if(allDoter[i].MMR != mmr)
                    allDoter.Remove(allDoter[i]);
            }

            return allDoter;
        }
        public static List<Doter> GetDotersByHours(int hours)
        {
            List<Doter> allDoter = GetDoters();

            foreach (var item in allDoter)
            {
                if (item.Hours == hours)
                    allDoter.Remove(item);
            }

            return allDoter;
        }
        public static List<Doter> GetDotersByName(string name)
        {
            List<Doter> allDoter = GetDoters();

            foreach (var item in allDoter)
            {
                if (item.Name == name)
                    allDoter.Remove(item);
            }

            return allDoter;
        }
        public static List<Doter> GetDotersByGames(int games)
        {
            List<Doter> allDoter = GetDoters();

            foreach (var item in allDoter)
            {
                if (item.Games == games)
                    allDoter.Remove(item);
            }

            return allDoter;
        }
        public static List<Doter> GetDotersByWins(int wins)
        {
            List<Doter> allDoter = GetDoters();

            foreach (var item in allDoter)
            {
                if (item.Wins == wins)
                    allDoter.Remove(item);
            }

            return allDoter;
        }

        //public static void AddDoter(int mmr, int hours, string name, int games, int wins, int amount, bool addAmountDoters = false)
        //{
           
        //    if (addAmountDoters)
        //    {
        //        List<string> lines = new List<string>();
        //        for (int i = 0; i < amount; i++)
        //        {
        //            Doter doter = new Doter(i + 1);
        //            lines.Add(doter.DoterCharacteristics());

        //        }
        //        _txtfileprocessing.AppendFile(false, lines.ToArray());
        //    }
        //    else
        //    {
        //        List<Doter> doters1 = GetDoters();
        //        string line = mmr + "," + hours + "," + name + "," + games + "," + wins;
        //        var array = line.Split(',');
        //        doters1.Add(new Doter(array));
        //        List<string> toFile = new List<string>();
        //        foreach (var item in doters1)
        //        {
        //            toFile.Add(item.DoterCharacteristics());
        //        }
        //        _txtfileprocessing.AppendFile(true, toFile.ToArray());
        //    }

        //}



        //public static string RemoveDoter(int affirmation, int choice, int number, string name, bool fullremoval = false)
        //{
        //    string line = "";
        //    if (affirmation == 1)
        //    {
        //        List<Doter> doterslist = GetDoters();
        //        List<string> toFile = new List<string>();
        //        if (fullremoval)
        //        {
        //            List<string> lines = new List<string>();
        //            _txtfileprocessing.AppendFile(true, lines.ToArray());
        //            return "All doters were removed from the file.";
        //        }

        //        if (choice == 1)
        //        {
        //            doterslist.RemoveAt(number);
        //            foreach (var item in doterslist)
        //            {
        //                toFile.Add(item.DoterCharacteristics());
        //            }
        //            _txtfileprocessing.AppendFile(true, toFile.ToArray());
        //            return "Doter number " + (number + 1) + " has been deleted from the file.";
        //        }
        //        else if (choice == 2)
        //        {

        //            foreach (var item in doterslist)
        //            {
        //                toFile.Add(item.DoterCharacteristics());
        //            }
        //            foreach (var item in toFile)
        //            {
        //                if (item.Contains(number.ToString()))
        //                {
        //                    toFile.Remove(item);
        //                    break;
        //                }
        //            }
        //            _txtfileprocessing.AppendFile(true, toFile.ToArray());
        //            return "Doter with mmr = " + number + " has been removed from the file.";
        //        }
        //        else if (choice == 3)
        //        {

        //            foreach (var item in doterslist)
        //            {
        //                toFile.Add(item.DoterCharacteristics());
        //            }
        //            foreach (var item in toFile)
        //            {
        //                if (item.Contains(name))
        //                {
        //                    toFile.Remove(item);
        //                    break;
        //                }
        //            }
        //            _txtfileprocessing.AppendFile(true, toFile.ToArray());
        //            return "Doter with name " + name + " has been removed from the file.";
        //        }           
        //    }
        //    else
        //    {
        //        return line;
        //    }
        //    return line;
        //}

        public static List<Doter> GetDoters()
        {
            List<Doter> doters = new List<Doter>();
            List<string> rows = new List<string>();
            if (XmlOrTXT == false)
            {
                TxtFileProcessing txtfileprocessing = new TxtFileProcessing(_storageFilePath);
                rows = txtfileprocessing.GetAllFileLines();
            }
            else if (XmlOrTXT)
            {
                XMLDoter xmldoter = new XMLDoter(_xmlfilepath);
                rows = xmldoter.GetLinesFromXML();
            }
            foreach (var item in rows)

            {
                var arrayvalues = item.Split(',');
                doters.Add(new Doter(arrayvalues));
            }
            return doters;
        }
    }

}
