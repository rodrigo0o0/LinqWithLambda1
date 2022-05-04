﻿using LinqWithLambda1.Entities;
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

            //var r1 = products.Where(p => p.Category.Tier == 1 && p.Price < 900).ToList();
            var r1 = from p in products
                     where p.Category.Tier == 1 && p.Price < 900
                     select p;

            Print("TIER 1 AND PRICE < 900 : ", r1);


            //var r2 = products.Where(p => p.Category.Name.ToUpper() == "Tools".ToUpper()).Select(p => p.Name).ToList();
            var r2 = from p in products
                     where p.Category.Name.ToUpper() == "Tools".ToUpper()
                     select p.Name;
            Print("NAME OF PRODUCTS FROM TOOLS : ", r2);

            //var r3 = products.Where(p => p.Name.ToUpper().StartsWith("c".ToUpper())).Select(p => new { p.Name, p.Price, CategoryName = p.Category.Name});
            var r3 = from p in products
                     where p.Name.ToUpper().StartsWith("C")
                     select new { p.Name, p.Price, CategoryName = p.Category.Name };
            Print("START WITH C ", r3);
            Console.WriteLine();

            //var r4 = products.Where(p => p.Category.Tier == 1).OrderBy(p => p.Price).ThenBy(p => p.Name);
            var r4 = from p in products
                     where p.Category.Tier == 1
                     orderby p.Price,p.Name
                     select p ;
            Print("Category Tier ordened by price then by name", r4);

            //var r5 = r4.Skip(2).Take(10);
            var r5 = (from p in r4
                      select p).Skip(2).Take(4);
            Print("TIER 1 ORDER BY PRICE THEN BY NAME SKIP 2 TAKE 4", r5);

            //var r6 = products.FirstOrDefault();
            var r6 = (from p in products select p).FirstOrDefault();
            Console.WriteLine("First test : 1", r6);
            
            var r7 = products.Where(p => p.Price > 3000.0).FirstOrDefault();
            Console.WriteLine("First or default test2 ",r7);


            var r8 = products.Where(p => p.Id == 1).SingleOrDefault();
            Console.WriteLine("Single or default test 1 " + r8);

            var r9 = products.Where(p => p.Id == 30).SingleOrDefault();
            Console.WriteLine("Single or default test 2 " +  r9);
            Console.WriteLine();

            var r10 = products.Max(p => p.Price);
            Console.WriteLine("Max price " + r10);
            Console.WriteLine();

            var r11 = products.Min(p => p.Price);
            Console.WriteLine("Min Price " + r11);
            Console.WriteLine();

            var r12 = products.Where(p => p.Category.Id == 1).Sum(p => p.Price);
            Console.WriteLine("Category 1 and sum all products " +  r12); 

            var r13 = products.Where(p => p.Category.Id == 1).Average(p => p.Price);
            Console.WriteLine("Average tests 1 " + r13);
            Console.WriteLine();

            var r14 = products.Where(p => p.Category.Id == 30).Select(p => p.Price).DefaultIfEmpty(0.0).Average();
            Console.WriteLine("Average tests 2 " + r14);
            Console.WriteLine();

            //mapreduce
            var r15 = products.Where(p => p.Category.Id == 1).Select(p => p.Price).Aggregate(0.0, (a, b) => a + b);
            Console.WriteLine("Agregate Sum " + r15);
            Console.WriteLine();

            //var r16 = products.GroupBy(p => p.Category);
            var r16 =
                from p in products
                group p by p.Category;
                      
            foreach (var item in r16)
            {
                Console.WriteLine("Category " +item.Key.Name);
                foreach (var item1 in item)
                {
                    Console.WriteLine(item1);
                }
                Console.WriteLine();
            }
            


            Console.ReadKey();
         
        }
    }
}
