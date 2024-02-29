using System.Text.Json;


namespace StoreApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string file = "store.json";


            //Showing all the people
            Console.WriteLine($"\n\nAll people");
            Store.Show();


            //Showing the Employeee with salary over 20_000
            double amount = 20_000;
            Console.WriteLine($"\n\nEmployee with salary over ${amount:n}");
            Store.Show(amount);


            //Showing a person with matching name
            string name = "Kassie";
            Console.WriteLine($"\nPerson with name {name}:");
            Store.Show(name);


            //Showing all the name longer than 5 letters
            int length = 5;
            Console.WriteLine($"\n\nPeople with name longer than {length} characters:");
            Store.Show(length);


            //To saving all transaction to a json file Console.WriteLine($"\n\nSaving everyone to file {file}");
            Store.Save(file);
        }
    }


    public abstract class StorePerson
    {
        protected static int ID = 100000;
        public string Cell { get; }
        public string Name { get; }
        public string Id { get; }

        public StorePerson(string name, string cell)
        {
            Name = name;
            Cell = cell;
            Id = ID.ToString();
            ID++;
        }
    }


    public class Customer : StorePerson
    {
        public double Credit { get; }

        public Customer(string name, string cell, double credit = 500) : base(name, cell)
        {
            Credit = credit;
        }

        public override string ToString()
        {
            return $"{Id} {Name} {Cell} Credit: ${Credit}";
        }
    }


    public class Employee : StorePerson
    {
        public double Salary { get; }

        public Employee(string name, string cell, double salary = 2500) : base(name, cell)
        {
            Salary = salary;
        }

        public override string ToString()
        {
            return $"{Id} {Name} {Cell} Salary: ${Salary:n0}";
        }
    }


    public class Store
    {
        private static List<StorePerson> people;

        static Store()
        {
            people =
       [
           new Customer("Gokulraj", "123-4567"),
           new Employee("Amy", "123-6789"),
           new Employee("Kassie", "234-5678", 19000),
           new Customer("Yao", "345-6789", 750),
           new Employee("Zhiyang", "456-7890", 29000),
           new Employee("Piyush", "678-9012", 17000),
           new Customer("Hitesh", "890-1234", 400),
           new Employee("Ahsan", "901-2345", 34000),
           new Customer("Wahiba", "123-9012", 750),
           new Employee("Rowel", "456-8901", 24800)
       ];
        }

        public static void Show()
        {
            foreach (var person in people)
            {
                Console.WriteLine(person.ToString());
            }
        }

        public static void Show(string name)
        {
            foreach (var person in people)
            {
                if (person.Name == name)
                    Console.WriteLine(person.ToString());
            }
        }

        public static void Show(int length)
        {
            foreach (var person in people)
            {
                if (person.Name.Length > length)
                    Console.WriteLine(person.ToString());
            }
        }

        public static void Show(double amount)
        {
            foreach (var person in people)
            {
                if (person is Employee employee && employee.Salary > amount)
                    Console.WriteLine(employee.ToString());
            }
        }

        public static void Save(string filename)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(people, options);
            File.WriteAllText(filename, jsonString);
        }
    }
}
