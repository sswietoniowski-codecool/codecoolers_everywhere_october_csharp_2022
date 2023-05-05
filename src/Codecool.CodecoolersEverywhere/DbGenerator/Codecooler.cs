using Codecool.CodeCoolersEverywhere.Helper;

namespace Codecool.CodeCoolersEverywhere.DbGenerator
{
    public class Codecooler
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int BirthYear { get; set; }
        public int BirthCityId { get; set; }

        public static Codecooler GenerateRandom(FirstName firstName, LastName lastName)
        {

            DbImportUtils Utils = new DbImportUtils();
            return new Codecooler()
            {
                FirstName = firstName.GetRandom(),
                LastName = lastName.GetRandom(),
                BirthYear = Utils.RandomNumber(2050, 2100),
                BirthCityId = Utils.RandomNumber(0, City.Data.Count)
            };
        }
    }
}

