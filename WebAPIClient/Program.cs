using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebAPIClient // Note: actual namespace depends on the project name.
{
    class Pokemon
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("weight")] public string Weight { get; set; }

        [JsonProperty("height")] public string Height { get; set; }

        public List<Types> Types { get; set; }
    }

    public class Type
    {
        [JsonProperty("name")] public string Name { get; set; }
    }

    public class Types
    {
        [JsonProperty("type")] public string Type { get; set; }
    }

    public class Calendar
    {
        [JsonProperty("season")] public string Season { get; set; }
        [JsonProperty("weekday")] public string Weekday { get; set; }
        [JsonProperty("date")] public DateTime Date { get; set; }
    }

    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }

        private static async Task ProcessRepositories()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter 'today', 'yesterday', or 'tomorrow'. Press Enter without writing a name to quit the program.");

                    var day = Console.ReadLine();

                    if (string.IsNullOrEmpty(day))
                    {
                        break;
                    }

                
                    //var result = await client.GetAsync("https://pokeapi.co/api/v2/pokemon/" + day.ToLower());
                    var result = await client.GetAsync("http://calapi.inadiutorium.cz/api/v0/en/calendars/general-en/" + day.ToLower());
                    var resultRead = await result.Content.ReadAsStringAsync();

                    //var pokemon = JsonConvert.DeserializeObject<Pokemon>(resultRead);
                    var  calendar = JsonConvert.DeserializeObject<Calendar>(resultRead);

                    Console.WriteLine("---");
                    //Console.WriteLine("Pokemon #" + pokemon.Id);
                    //Console.WriteLine("Name: " + pokemon.Name);
                    //Console.WriteLine("Height: " + pokemon.Height);
                    //Console.WriteLine("Type(s):");
                    //pokemon.Types.ForEach(t => Console.Write(" " + t.Type.Name));
                    Console.WriteLine("Season: " + calendar.Season);
                    Console.WriteLine("Weekday: " + calendar.Weekday);
                    Console.WriteLine("Date: " + calendar.Date);
                    Console.WriteLine("\n---");
                }
                catch (Exception)
                {
                    //Console.WriteLine("ERROR. Please enter a valid Pokemon name!");
                    Console.WriteLine("ERROR. Something went wrong!");
                }
                
            }
        }
    }
}