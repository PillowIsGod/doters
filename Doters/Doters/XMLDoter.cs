using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Doters
{
    public class XMLDoter
    {
        public readonly string FilePath;
        public XMLDoter(string filePath)
        {
            FilePath = filePath;
        }

        public void GenerateDoters(List<Doter> doterList)
        {
            XmlDocument xdoc = new XmlDocument();
            XmlElement doters = xdoc.CreateElement("doters");
            xdoc.AppendChild(doters);

            foreach (var item in doterList)
            {
                XmlElement doter = xdoc.CreateElement("doter");
                doters.AppendChild(doter);

                XmlElement mmrElem = xdoc.CreateElement("mmr");
                doter.AppendChild(mmrElem);
                XmlElement hoursElem = xdoc.CreateElement("hours");
                doter.AppendChild(hoursElem);
                XmlElement nameElem = xdoc.CreateElement("name");
                doter.AppendChild(nameElem);

                XmlElement gamesElem = xdoc.CreateElement("games");
                doter.AppendChild(gamesElem);

                XmlElement winsElem = xdoc.CreateElement("wins");
                doter.AppendChild(winsElem);

                XmlText mmrText = xdoc.CreateTextNode(item.MMR.ToString());
                XmlText hoursText = xdoc.CreateTextNode(item.Hours.ToString());
                XmlText nameText = xdoc.CreateTextNode(item.Name);
                XmlText gamesText = xdoc.CreateTextNode(item.Games.ToString());
                XmlText winsText = xdoc.CreateTextNode(item.Wins.ToString());
                mmrElem.AppendChild(mmrText);
                hoursElem.AppendChild(hoursText);
                nameElem.AppendChild(nameText);
                gamesElem.AppendChild(gamesText);
                winsElem.AppendChild(winsText);
            }
            xdoc.Save(FilePath);
        }

        public void AddDoterToXml()
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(FilePath);

        }
        public void TransferDataFromTXTtoXML(List<string> fileContent)
        {
            XmlDocument xdoc = new XmlDocument();
            XmlElement doters = xdoc.CreateElement("doters");
            List<Doter> doterslist = DoterStorage.GetDoters();
            xdoc.AppendChild(doters);
            foreach (var item in doterslist)
            {
                XmlElement doter = xdoc.CreateElement("doter");
                doters.AppendChild(doter);

                XmlElement mmrElem = xdoc.CreateElement("mmr");
                doter.AppendChild(mmrElem);
                XmlElement hoursElem = xdoc.CreateElement("hours");
                doter.AppendChild(hoursElem);
                XmlElement nameElem = xdoc.CreateElement("name");
                doter.AppendChild(nameElem);

                XmlElement gamesElem = xdoc.CreateElement("games");
                doter.AppendChild(gamesElem);

                XmlElement winsElem = xdoc.CreateElement("wins");
                doter.AppendChild(winsElem);

                XmlText mmrText = xdoc.CreateTextNode(item.MMR.ToString());
                XmlText hoursText = xdoc.CreateTextNode(item.Hours.ToString());
                XmlText nameText = xdoc.CreateTextNode(item.Name);
                XmlText gamesText = xdoc.CreateTextNode(item.Games.ToString());
                XmlText winsText = xdoc.CreateTextNode(item.Wins.ToString());
                mmrElem.AppendChild(mmrText);
                hoursElem.AppendChild(hoursText);
                nameElem.AppendChild(nameText);
                gamesElem.AppendChild(gamesText);
                winsElem.AppendChild(winsText);
            }
            xdoc.Save(FilePath);
        }
    }
}
