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


namespace Dollar_export_save_xml_info
{

    class Product : IComparable<Product>
    {
        public string Id { get; }
        public string Name { get; }
        public int Price { get; }

        public Product(int price, string id, string name)
        {
            Price = price;
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Id}\n{Name}\n{Price}";
        }

        public int CompareTo(Product obj)
        {
            return Price > obj.Price ? 1 : -1;
            throw new Exception();
        }
    }


    internal class XmlWriterOrder
    {
        string Path { get; }
        Encoding Encoding { get; }
        List<Product> Products { get; }

        public XmlWriterOrder(List<Product> listOfProducts, string path, Encoding encod)
        {
            Products = listOfProducts;
            Path = path;
        }

        public void SaveToXml()
        {
            using (Stream stream = new FileStream(Path, FileMode.Create))
            {
                XmlTextWriter writerXml = new XmlTextWriter(stream, Encoding);

                writerXml.Formatting = Formatting.Indented; // For readability

                writerXml.WriteStartElement("order"); // Write root element

                for(int i = 0; i < Products.Count(); i++)
                {
                    writerXml.WriteStartElement("Product"); // Start product element
                    writerXml.WriteAttributeString("number", $"{i+1}"); // Add attribute for element product

                    // Write subelements
                    writerXml.WriteElementString("Id", $"{ Products[i].Id}");
                    writerXml.WriteElementString("Name", $"{ Products[i].Name}");
                    writerXml.WriteElementString("Price", $"{ Products[i].Price}");

                    writerXml.WriteEndElement(); // End product element
                }

                writerXml.WriteFullEndElement(); // End the root element
                writerXml.Close();
            }

        }


    }






}
