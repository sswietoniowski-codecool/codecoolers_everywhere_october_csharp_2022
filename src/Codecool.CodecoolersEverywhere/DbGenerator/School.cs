using System.Collections.Generic;

namespace Codecool.CodeCoolersEverywhere.DbGenerator
{
    public class School
    {
        public string Name { get; set; }
        public string CityId { get; set; }

        public static School GenerateRandom()
        {
            var city = City.GenerateRandom();
            return new School()
            {
                Name = $"Codecool {city.Name}",
                CityId = city.Identifier
            };
        }

        public static List<School> GenerateRandoms(int quantity)
        {
            var cities = City.GenerateRandoms(quantity);
            var returnList = new List<School>();

            foreach (var city in cities)
            {
                returnList.Add(new School()
                {
                    Name = $"Codecool {city.Name}",
                    CityId = city.Identifier
                });
            }
            return returnList;
        }
    }
}