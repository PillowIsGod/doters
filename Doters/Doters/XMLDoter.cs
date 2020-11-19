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



        public XmlElement GetFromDoter(Doter doter, XmlDocument xdoc)
        {
                XmlElement xmlDoter = xdoc.CreateElement("doter");

                XmlElement mmrElem = xdoc.CreateElement("mmr");
                xmlDoter.AppendChild(mmrElem);
                XmlElement hoursElem = xdoc.CreateElement("hours");
                xmlDoter.AppendChild(hoursElem);
                XmlElement nameElem = xdoc.CreateElement("name");
                xmlDoter.AppendChild(nameElem);

                XmlElement gamesElem = xdoc.CreateElement("games");
                xmlDoter.AppendChild(gamesElem);

                XmlElement winsElem = xdoc.CreateElement("wins");
                xmlDoter.AppendChild(winsElem);

                XmlText mmrText = xdoc.CreateTextNode(doter.MMR.ToString());
                XmlText hoursText = xdoc.CreateTextNode(doter.Hours.ToString());
                XmlText nameText = xdoc.CreateTextNode(doter.Name);
                XmlText gamesText = xdoc.CreateTextNode(doter.Games.ToString());
                XmlText winsText = xdoc.CreateTextNode(doter.Wins.ToString());
                mmrElem.AppendChild(mmrText);
                hoursElem.AppendChild(hoursText);
                nameElem.AppendChild(nameText);
                gamesElem.AppendChild(gamesText);
                winsElem.AppendChild(winsText);
            return xmlDoter;
        }
        public void DoterAddToXml(bool trunc = false, params Doter[] doters)
        {
            XmlDocument xdoc = new XmlDocument();
            if (trunc == false)
            {
                xdoc.Load(FilePath);
                XmlElement doters1 = xdoc.DocumentElement;
                foreach (var item in doters)
                {
                    var xmlDoter = GetFromDoter(item, xdoc);
                    doters1.AppendChild(xmlDoter);
                }
                xdoc.Save(FilePath);
            }
            else if (trunc)
            {
                XmlElement doters1 = xdoc.CreateElement("doters");
                xdoc.AppendChild(doters1);
                foreach (var item in doters)
                {
                    var xmlDoter = GetFromDoter(item, xdoc);
                    doters1.AppendChild(xmlDoter);
                }
                xdoc.Save(FilePath);
            }
        }
        public List<string> GetLinesFromXML()
        {
            List<string> lines = new List<string>();
            List<string> linesToOutput = new List<string>();
            string line = "";
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(FilePath);
            XmlElement root = xdoc.DocumentElement;
            foreach (XmlNode item in root)
            {
                foreach (XmlNode node in item.ChildNodes)
                {
                    if (node.Name == "mmr")
                    {
                        lines.Add(node.InnerText);
                    }
                    if (node.Name == "hours")
                    { lines.Add(node.InnerText); }
                    if (node.Name == "name")
                    {
                        lines.Add(node.InnerText);
                    }
                    if (node.Name == "games")
                    {
                        lines.Add(node.InnerText);
                    }
                    if (node.Name == "wins")
                    {
                        lines.Add(node.InnerText);
                    }
                }
                line = lines[0] + "," + lines[1] + "," + lines[2] + "," + lines[3] + "," + lines[4];
                linesToOutput.Add(line);
                lines.Clear();
            }
            return linesToOutput;
        }
        public void TransferDotersToXML()
        {
            XmlDocument xdoc = new XmlDocument();
            XmlElement doters = xdoc.CreateElement("doters");
            List<Doter> doterslist = DoterStorage.GetDoters();
            xdoc.AppendChild(doters);
            foreach (var item in doterslist)
            {
                
                var xmlDoter = GetFromDoter(item, xdoc);
                doters.AppendChild(xmlDoter);
            }
            xdoc.Save(FilePath);
        }
        public XMLDoter(string filePath)
        {
            FilePath = filePath;

        }
    }
}
