using Codecool.CodeCoolersEverywhere.Helper;
using System.Collections.Generic;
using System.Linq;

namespace Codecool.CodeCoolersEverywhere.DbGenerator
{
    public class City
    {
        public static string FILENAME = "cities.txt";
        public static List<City> Data = new List<City>();
        public string Identifier { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public static City GenerateRandom()
        {
            DbImportUtils Utils = new DbImportUtils();
            int maxdatalen = Data.Count;
            return Data[Utils.RandomNumber(0, maxdatalen)];
        }
        public static List<City> GenerateRandoms(int quantity)
        {
            DbImportUtils Utils = new DbImportUtils();
            int maxdatalen = Data.Count;
            return Data.OrderBy(x => Utils.RandomNumber(0, maxdatalen)).Take(quantity).ToList();
        }
        public static void LoadData()
        {
            var FileData = FileResource.LoadFile(FILENAME);
            for (int i = 0; i < FileData.Length; i++)
            {
                var lineData = FileData[i].Split(",");
                Data.Add(new City()
                {
                    Identifier = (i + 1).ToString(),
                    Name = lineData[0].Replace("'", ""),
                    Country = lineData[1].Replace("'", "")
                });
            }
        }
    }
}
