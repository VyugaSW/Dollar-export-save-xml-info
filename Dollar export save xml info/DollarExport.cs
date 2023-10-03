using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Net;
using System.Xml;

using Universal.Common.Collections;

namespace Dollar_export_save_xml_info
{
    internal class XmlDownloader
    {
        public string URL { get; } = @"http://finance.i.ua/";
        public string PathToFile { get; set; }

        public XmlDownloader(string some, string path)
        {
            URL = URL.Replace("CODE", some);
            PathToFile = path;
        }

        public MultiDictionary<string, string> GetDollarRate()
        {
            XmlDocument xmlDocument = new XmlDocument();
            MultiDictionary<string, string> bankRates = new MultiDictionary<string, string>();

            xmlDocument.Load(PathToFile);

            XmlNodeList xmlNodeList = xmlDocument.GetElementsByTagName("item");

            foreach (XmlNode xmlNode in xmlNodeList)
            {
                XmlNode bankName = xmlNode.SelectSingleNode("bank");
                XmlNode buyRate = xmlNode.SelectSingleNode("buyRate");
                XmlNode sellRate = xmlNode.SelectSingleNode("sellRate");

                bankRates[bankName.InnerText] = new List<string>() { buyRate.InnerText, sellRate.InnerText};
            }
            return bankRates;
        }
    }

}
