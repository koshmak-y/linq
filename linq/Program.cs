using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linq
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Consumer> consumersList = new List<Consumer>()
            {
                new Consumer(){CodeConsumer = 1, Street = "Science Prospect", BirthYear = 1990 },
                new Consumer(){CodeConsumer = 2, Street = "Science Prospect", BirthYear = 1992 },
                new Consumer(){CodeConsumer = 3, Street = "Science Prospect", BirthYear = 1993 }
            };

            List<Product> productsList = new List<Product>()
            {
                new Product(){ProductArticle = 1, Category = "Техника", ManufactureCountry = "China"},
                new Product(){ProductArticle = 2, Category = "Техника", ManufactureCountry = "America"}
            };

            List<Store> storesList = new List<Store>()
            {
                new Store(){ProductArticle = 1, CodeConsumer =1, StoreName = "Rozetka" },
                new Store(){ProductArticle = 1, CodeConsumer =1, StoreName = "АТБ" },
                new Store(){ProductArticle = 2, CodeConsumer =1, StoreName = "Rozetka" },
                new Store(){ProductArticle = 2, CodeConsumer =1, StoreName = "АТБ" },
                new Store(){ProductArticle = 2, CodeConsumer =3, StoreName = "АТБ" },
            };

            var temp = from pd in productsList
                       join sl in storesList on pd.ProductArticle equals sl.ProductArticle
                       join cl in consumersList on sl.CodeConsumer equals cl.CodeConsumer
            select new { Year = cl.BirthYear, pd.ManufactureCountry, sl.ProductArticle };

            foreach (var item in temp)
            {
                Console.WriteLine($"Year: {item.Year}\tCountry:{item.ManufactureCountry}\tProduct:{item.ProductArticle}");
            }
            Console.WriteLine(new string('-',50));

            var temp2 = from tmp in temp
                        group tmp by new { tmp.Year, tmp.ManufactureCountry } into g
                        select new {g.Key.Year, g.Key.ManufactureCountry, Count = g.Count() };

            var result = temp2.OrderBy(u => u.Year).ThenBy(u => u.ManufactureCountry);
            
            foreach (var item in result)
            {
                Console.WriteLine($"Year: {item.Year}\tCountry:{item.ManufactureCountry}\t\tКоличество покупок:{item.Count}");
            }

            Console.ReadKey();
        }
    }

    class Consumer
    {
        public int CodeConsumer { get; set; }
        public string Street { get; set; }
        public int BirthYear { get; set; }
    }

    class Product
    {
        public string ManufactureCountry { get; set; }
        public string Category { get; set; }
        public int ProductArticle { get; set; }
    }

    class Store
    {
        public int ProductArticle { get; set; }
        public int CodeConsumer { get; set; }
        public string StoreName { get; set; }
    }

}
