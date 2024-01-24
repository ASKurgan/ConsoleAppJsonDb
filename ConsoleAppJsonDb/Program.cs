// See https://aka.ms/new-console-template for more information
using ConsoleAppJsonDb;
using System.Text.Json;
using System.Xml.Serialization;

Console.WriteLine("Hello, World!");

List<Customer> customers = new ();
customers.Add (new Customer{ Age = 25, Name = "Jason"});
customers.Add(new Customer { Age = 20, Name = "Mike" });
string json = JsonSerializer.Serialize (customers);
Console.WriteLine(json);
File.WriteAllText(@".\customers.json", json);
string jsonFromFile = File.ReadAllText(@".\customers.json");
Console.WriteLine(jsonFromFile);
List<Customer> customersFromFile = JsonSerializer.Deserialize<List<Customer>>(jsonFromFile);

foreach (var c1 in customersFromFile)
{
    c1.Age++;
}

json = JsonSerializer.Serialize (customersFromFile);
Console.WriteLine(json);
File.WriteAllText(@".\customers.json", json);
jsonFromFile = File.ReadAllText(@".\customers.json");
Console.WriteLine(jsonFromFile);
customersFromFile = JsonSerializer.Deserialize<List<Customer>>(jsonFromFile);

foreach (var c2 in customersFromFile)
{
    if (c2.Name == "Jason")
    {
        customersFromFile.Remove(c2);
        break;
    }

}

json = JsonSerializer.Serialize (customersFromFile);
Console.WriteLine(json);
File.WriteAllText(@".\customers.json", json);
jsonFromFile = File.ReadAllText(@".\customers.json");
Console.WriteLine(jsonFromFile);
customersFromFile = JsonSerializer.Deserialize<List<Customer>>(jsonFromFile);
Customer c = new() { Age = 30,Name = "Bob" };
customersFromFile.Add(c);

json = JsonSerializer.Serialize (customersFromFile);
Console.WriteLine(json);
File.WriteAllText(@".\customers.json", json);

// Сохраняем Jason и Nikki в xml файл

Customer[] customersArr = customers.ToArray();

// Передаём в контструктор тип класса Customer []

XmlSerializer xmlSerializer = new XmlSerializer(typeof(Customer[]));


// Получаем поток, куда будем записывать сериализованный объект

using (FileStream fs = new FileStream("customers.xml", FileMode.Create))
{
    xmlSerializer.Serialize(fs, customersArr);
    Console.WriteLine("Object has been serialized");
}

// Удаляем Nikki из xml файла
// восстановление массива из файла

using (FileStream fs = new FileStream("customers.xml", FileMode.Open) )
{
    Customer[] customersArrFromFile = xmlSerializer.Deserialize(fs) as Customer[];
    if (customersArrFromFile != null)
    {
        foreach (Customer cItem in customersArrFromFile)
        {
            Console.WriteLine($"Name: {cItem.Name} --- Age: {cItem.Age}");
        }
        customers = customersArrFromFile.ToList();

        foreach (Customer cItem in customersArrFromFile)
        {
            if (cItem.Name == "Nikki") 
            {
                customers.Remove(cItem);
                break;
            }
        }

        customersArr = customers.ToArray();
    }
}

// Получаем поток, куда будем записывать сериализованный объект

using (FileStream fs = new("customers.xml", FileMode.Create))
{
     xmlSerializer.Serialize(fs, customersArr);
    Console.WriteLine("Object has been serialized");
}

// Добавляем Bob в XML - файл
// Восстановление массива из файла

using (FileStream fs = new("customers.xml",FileMode.Open))
{
    Customer[] customersArrFromFile = xmlSerializer.Deserialize(fs) as Customer[];
    if (customersArrFromFile != null) 
    {
       customers = customersArrFromFile.ToList();
       customers.Add(new Customer { Age = 30, Name = "Bob"});
       customersArr = customers.ToArray();
    }
}

// Получаем поток, куда будем записывать сериализованный объект

using (FileStream fs = new("customers.xml",FileMode.Create))
{
   xmlSerializer.Serialize(fs, customersArr);
   Console.WriteLine("Object has been serialized");
}

