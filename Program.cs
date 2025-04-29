using System;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace ConsoleApp
{
    // JSON User class
    public class JsonUser
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }

    // XML User class MUST be public!
    [XmlRoot("User")]
    public class XmlUser
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ReadJson();
            ReadXml();
        }

        static void ReadJson()
        {
            Console.WriteLine("--- JSON User Data ---");

            string filePath = "user.json";
            if (!File.Exists(filePath))
            {
                Console.WriteLine("JSON file not found!");
                return;
            }

            string json = File.ReadAllText(filePath);
            JsonUser user = JsonConvert.DeserializeObject<JsonUser>(json);

            Console.WriteLine($"Name: {user.Name}");
            Console.WriteLine($"Age: {user.Age}");
            Console.WriteLine($"City: {user.City}");
        }

        static void ReadXml()
        {
            Console.WriteLine("\n--- XML User Data ---");

            string filePath = "example.xml";
            if (!File.Exists(filePath))
            {
                Console.WriteLine("XML file not found!");
                return;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(XmlUser));
            using (StreamReader reader = new StreamReader(filePath))
            {
                XmlUser user = (XmlUser)serializer.Deserialize(reader);
                Console.WriteLine($"Name: {user.Name}");
                Console.WriteLine($"Age: {user.Age}");
                Console.WriteLine($"City: {user.City}");
            }
        }
    }
}
