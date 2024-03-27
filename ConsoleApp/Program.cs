using System.Collections;

namespace ConsoleApp
{
    
    public class Employee 
    {
        public int id {get ; set;}
        public string? name {get ; set;}
        public int DeptID {get ; set;}
        public Employee(int id , string name , int DeptID)
        {
            this.id = id;
            this.name = name;
            this.DeptID = DeptID;
        }
        public override string ToString()
        {
            return $"{id} - {name} - {DeptID}";
        }
    }

    internal class Program
    {
        public static List<Employee> LoadEmps ()
        {
            return new List<Employee> {
                new Employee (1 , "mohamed" , 1),
                new Employee (2 , "Alaa" , 2),
                new Employee (3 , "Samai" , 1),
                new Employee (4 , "Aoulaya" , 4),
                new Employee (5 , "Alaa" , 5),
                new Employee (6 , "Khaled" , 3),
                new Employee (7 , "Samir" , 4),
                new Employee (8 , "Samora" , 1),
            };
        }
        static void Main(string[] args)
        {
            var rand = new Random ();
            var t = Enumerable.Range (0 , rand.Next () );

            foreach (var item in t)
            {
                System.Console.WriteLine(item);
            }
        }   
    }
}
