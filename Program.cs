using System;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Collections.Generic; // Add this for List<T>

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

    public class UserBase
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }

    public class Admin : UserBase
    {
        public string Role { get; set; } = "Administrator";
        public string Permissions { get; set; }
    }

    public class Member : UserBase
    {
        public string Role { get; set; } = "Member";
        public string Subscription { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ReadJson();
            ReadXml();
            ReadUserTypes(); // Call ReadUserTypes() here
        }

        static void ReadJson()
        {
            Console.WriteLine("--- JSON Users ---");

            string filePath = "user.json";
            if (!File.Exists(filePath))
            {
                Console.WriteLine("JSON file not found!");
                return;
            }

            string json = File.ReadAllText(filePath);

            // Deserialize a list of users
            List<JsonUser> users = JsonConvert.DeserializeObject<List<JsonUser>>(json);

            // Loop through and print each user
            foreach (var user in users)
            {
                Console.WriteLine($"Name: {user.Name}");
                Console.WriteLine($"Age: {user.Age}");
                Console.WriteLine($"City: {user.City}");
                Console.WriteLine();
            }
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

        // Add ReadUserTypes() outside of ReadXml()
        static void ReadUserTypes()
        {
            Console.WriteLine("\n--- User Types ---");

            string filePath = "user_types.json";
            if (!File.Exists(filePath))
            {
                Console.WriteLine("user_types.json not found!");
                return;
            }

            string json = File.ReadAllText(filePath);
            var users = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);

            foreach (var user in users)
            {
                if (user.ContainsKey("Permissions"))
                {
                    Admin admin = new Admin
                    {
                        Name = user["Name"].ToString(),
                        Age = Convert.ToInt32(user["Age"]),
                        City = user["City"].ToString(),
                        Permissions = user["Permissions"].ToString()
                    };

                    Console.WriteLine($"[Admin] {admin.Name}, {admin.Age}, {admin.City}, Permissions: {admin.Permissions}");
                }
                else if (user.ContainsKey("Subscription"))
                {
                    Member member = new Member
                    {
                        Name = user["Name"].ToString(),
                        Age = Convert.ToInt32(user["Age"]),
                        City = user["City"].ToString(),
                        Subscription = user["Subscription"].ToString()
                    };

                    Console.WriteLine($"[Member] {member.Name}, {member.Age}, {member.City}, Subscription: {member.Subscription}");
                }
            }
        }
    }
}
