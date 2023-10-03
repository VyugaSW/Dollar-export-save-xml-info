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

    class Order
    {
        public List<Product> ListOfProducts { get; private set; }

        public Order(List<Product> listProducts)
        {
            ListOfProducts = listProducts;
        }
        public Order()
        {
            ListOfProducts = new List<Product>();
        }

        public void Add(Product product)
        {
            ListOfProducts.Add(product);
        }

        public void AddRange(Product[] product)
        {
            ListOfProducts.AddRange(product);
        }

        public void Remove(Product product)
        {
            ListOfProducts.Remove(product);
        }

        public void RemoveAt(int index)
        {
            ListOfProducts.RemoveAt(index);
        }

        public int Count()
        {
            return ListOfProducts.Count;
        }

        public void SortedByPrice()
        {
            ListOfProducts.Sort();
        }

        public string DisplayProducts()
        {
            string infoProducts = "";
            foreach(Product product in ListOfProducts)
            {
                infoProducts += product + "\n";
            }
            return infoProducts;
        }

    }

    internal class SaveOrderXml
    {
        Order Order { get; }
        string Path { get; }

        public SaveOrderXml(Order order, string path)
        {
            Order = order;
            Path = path;
        }


        public void SaveOrderToXml()
        {
            using (Stream stream = new FileStream(Path, FileMode.Create))
            {
                XmlTextWriter xmlTextWriter = new XmlTextWriter(stream, Encoding.Default);

                xmlTextWriter.WriteStartElement("root");
                xmlTextWriter.WriteAttributeString("xmlns:", "x", null, "order");

                for(int i = 0; i < Order.Count(); i++)
                {
                    xmlTextWriter.WriteStartElement("item", "order");


                    xmlTextWriter.WriteStartElement($"product{i+1}", "order");

                    xmlTextWriter.WriteAttributeString("Id", "order", $"{Order.ListOfProducts[i].Id}");
                    xmlTextWriter.WriteAttributeString("Name", "order", $"{Order.ListOfProducts[i].Name}");
                    xmlTextWriter.WriteAttributeString("Price", "order", $"{Order.ListOfProducts[i].Price}");

                    xmlTextWriter.WriteEndElement();


                    xmlTextWriter.WriteEndElement();

                }
                xmlTextWriter.WriteEndElement();
            }
        }


    }
}
