using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static string results;
        static char key;
        static Tuple<string, string> names;
        

        static void Main(string[] args)
        {
            int n = 0;

            Console.WriteLine("Press ? to get instructions.\nPress any other key to exit.");

            if (Console.ReadLine() == "?")
            {
                while (true)
                {
                    Console.WriteLine("Press c to get categories");
                    Console.WriteLine("Press r to get random jokes");
                    Console.WriteLine("Press any other key to exit");

                    GetEnteredKey(Console.ReadKey());
                    
                  
                    if (key == 'c')
                    {
                        getCategories();
                        JObject categories = JObject.Parse(results);
                        Console.WriteLine("\n--------Categories--------");
                        foreach (var category in categories["value"])
                        {
                            Console.WriteLine(category);
                        }
                        Console.WriteLine("--------------------------");
                    }
                    else if (key == 'r')
                    {
                        Console.WriteLine("\nWant to use a random name? y/n");
                        GetEnteredKey(Console.ReadKey());

                        if (key == 'y')
                            GetNames();

                        Console.WriteLine("\nWant to specify a category? y/n");
                        GetEnteredKey(Console.ReadKey());

                        while (n < 1 || n > 9)
                        {
                            Console.WriteLine("\nHow many jokes do you want? (1-9)");
                            n = Int32.Parse(Console.ReadLine());
                            if (n < 1 || n > 9)
                            {
                                Console.WriteLine("Please enter a valid number.");
                            }
                        }

                        if (key == 'y')
                        {
                            Console.WriteLine("\nEnter a category:");
                            GetRandomJokes(Console.ReadLine());
                        }
                        else
                        {
                            GetRandomJokes(null);
                        }

                        JObject jokes = JObject.Parse(results);
                        Console.WriteLine("--------Jokes---------");
                        for(int i = 0; i < n; i++)
                        {
                            Console.WriteLine("{0}: " + jokes["value"][i]["joke"], i+1);
                        }
                        Console.WriteLine("----------------------");
                    }
                    else
                    {
                        return;
                    }
                    names = null;
                    n = 0;
                }
            }
        }


        private static void GetEnteredKey(ConsoleKeyInfo consoleKeyInfo)
        {
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.C:
                    key = 'c';
                    break;
                case ConsoleKey.D0:
                    key = '0';
                    break;
                case ConsoleKey.D1:
                    key = '1';
                    break;
                case ConsoleKey.D3:
                    key = '3';
                    break;
                case ConsoleKey.D4:
                    key = '4';
                    break;
                case ConsoleKey.D5:
                    key = '5';
                    break;
                case ConsoleKey.D6:
                    key = '6';
                    break;
                case ConsoleKey.D7:
                    key = '7';
                    break;
                case ConsoleKey.D8:
                    key = '8';
                    break;
                case ConsoleKey.D9:
                    key = '9';
                    break;
                case ConsoleKey.R:
                    key = 'r';
                    break;
                case ConsoleKey.Y:
                    key = 'y';
                    break;
                default:
                    key = '\0';
                    break;
            }
        }

        private static void GetRandomJokes(string category)
        {
            new JsonFeed("http://api.icndb.com");
            results = JsonFeed.GetRandomJokes(names?.Item1, names?.Item2, category);
        }

        private static void getCategories()
        {
            new JsonFeed("http://api.icndb.com");
            results = JsonFeed.GetCategories();
        }

        private static void GetNames()
        {
            new JsonFeed("http://uinames.com/api");
            dynamic result = JsonFeed.Getnames();
            names = Tuple.Create(result.name.ToString(), result.surname.ToString());
        }
    }
}
