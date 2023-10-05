using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dollar_export_save_xml_info
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Product> order = new List<Product>()
                {
                new Product(157, "00154", "Banana"),
                new Product(15000, "01073", "Fridge"),
                new Product(5, "70180", "Rabbit")
                };

            XmlWriterOrder sOX = new XmlWriterOrder(order, @"D:\Test\TestXml.xml", Encoding.Default);
            sOX.SaveToXml();
        }
    }
}
