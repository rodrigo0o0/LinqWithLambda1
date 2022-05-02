using LinqWithLambda1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqWithLambda1
{
    internal class Program
    {
        static void Print<T>(string message, IEnumerable<T> collection)
        {
            Console.WriteLine(message);
            foreach (T obj in collection)
            {
                Console.WriteLine(obj);
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            Category c1 = new Category() { Id = 1, Name = "Tools", Tier = 2 };
            Category c2 = new Category() { Id = 2, Name = "Computers", Tier = 1 };
            Category c3 = new Category() { Id = 3, Name = "Electronics", Tier = 1 };

            List<Product> products = new List<Product>() {
            new Product() { Id = 1, Name = "Computer", Price = 1100.0, Category = c2 },
                new Product() { Id = 2, Name = "Hammer", Price = 90.0, Category = c1 },
                new Product() { Id = 3, Name = "TV", Price = 1700.0, Category = c3 },
                new Product() { Id = 4, Name = "Notebook", Price = 1300.0, Category = c2 },
                new Product() { Id = 5, Name = "Saw", Price = 80.0, Category = c1 },
                new Product() { Id = 6, Name = "Tablet", Price = 700.0, Category = c2 },
                new Product() { Id = 7, Name = "Camera", Price = 700.0, Category = c3 },
                new Product() { Id = 8, Name = "Printer", Price = 350.0, Category = c3 },
                new Product() { Id = 9, Name = "MacBook", Price = 1800.0, Category = c2 },
                new Product() { Id = 10, Name = "Sound Bar", Price = 700.0, Category = c3 },
                new Product() { Id = 11, Name = "Level", Price = 70.0, Category = c1 }

            };

            var r1 = products.Where(p => p.Category.Tier == 1 && p.Price <= 900).ToList();
            Print("TIER 1 AND PRICE < 900 : ", r1);

     
            var r2 = products.Where(p => p.Category.Name.ToUpper() == "Tools".ToUpper()).Select(p => p.Name).ToList();
            Print("NAME OF PRODUCTS FROM TOOLS : ", r2);

            var r3 = products.Where(p => p.Name.ToUpper().StartsWith("c".ToUpper())).Select(p => new { p.Name, p.Price, CategoryName = p.Category.Name});
            Print("START WITH C ", r3);

            var r4 = products.Where(p => p.Category.Tier == 1).OrderBy(p => p.Price).ThenBy(p => p.Name);
            Print("Category Tier ordened by price then by name",r4);

            var r5 = r4.Skip(2).Take(10);
            Print("TIER 1 ORDER BY PRICE THEN BY NAME SKIP 2 TAKE 4",r5);

            var r6 = products.FirstOrDefault();
            Console.WriteLine("First test : 1", r6);
            
            var r7 = products.Where(p => p.Price > 3000.0).FirstOrDefault();
            Console.WriteLine("First or default test2 ",r7);


            var r8 = products.Where(p => p.Id == 1).SingleOrDefault();
            Console.WriteLine("Single or default test 1 ", r8);
            Console.ReadKey();
         
        }
    }
}
