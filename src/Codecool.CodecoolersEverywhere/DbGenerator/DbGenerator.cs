using Codecool.CodeCoolersEverywhere.Helper;
using System;
using System.IO;
using System.Linq;


namespace Codecool.CodeCoolersEverywhere.DbGenerator
{
    public static class DbGenerator
    {
        /// <summary>
        /// CODECOOLERNUM can be limited in case of performance issues. In case you still run in
        /// performance problems you can limit the number of schools per Codecooler, reducing the 
        /// size of switch table.
        /// </summary>
        public const int CODECOOLERNUM = 1000000;
        public const int PERCENTAGETOLOG = 100;
        public const int SCHOOLNUM = 100;
        public const int MAXNUMBEROFSCHOOLSPERCODECOOLER = 5;
        public const string DATABASEFILENAME = "database.sql";
        public static string CURDIR = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
        public static string WRITEPATH = CURDIR + DATABASEFILENAME;
        public const int BATCHSIZE = 1000;
        public static string MySqlString { get; set; } = "";

        public static void LogPercentage(double actual, double all, string message)
        {
            double percent;
            if (actual % (all / PERCENTAGETOLOG) == 0)
            {
                percent = (actual / all) * 100;
                Console.WriteLine($"{percent}% {message}");
            }
        }

        public static void GenerateSql()
        {
            GenerateSchema();
            GenerateCities();
            GenerateSchools(SCHOOLNUM);
            GenerateCodecoolers(CODECOOLERNUM);
            GenerateCodecoolersSchools();
            GenerateConstraints();
        }

        public static void GenerateSchema()
        {
            string path = CURDIR + "data/schema.sql";

            if (WRITEPATH != null)
            {
                File.WriteAllText(WRITEPATH, string.Empty);
            }

            MySqlString += File.ReadAllText(path);
            using StreamWriter sw = File.AppendText(WRITEPATH);
            sw.WriteLine(MySqlString);
            sw.WriteLine(GenerateNewLine());
        }

        public static void GenerateCities()
        {
            City.LoadData();
            var cities = City.Data;
            var NumberOfCities = cities.Count;
            using StreamWriter sw = File.AppendText(WRITEPATH);
            string endchar = ";";

            foreach (var city in cities)
            {
                if (Convert.ToInt32(city.Identifier) % BATCHSIZE == 0)
                { endchar = ";\n GO \n"; }
                else { endchar = ";"; }

                sw.WriteLine($"INSERT INTO cities(id, name, country) VALUES " +
                $"({city.Identifier}, '{city.Name}', '{city.Country}'){endchar}");
                LogPercentage(Convert.ToDouble(city.Identifier), NumberOfCities, "of the Cities are generated");
            }
            sw.WriteLine(GenerateNewLine());
        }

        public static void GenerateSchools(int number)
        {
            var schools = School.GenerateRandoms(number);
            using StreamWriter sw = File.AppendText(WRITEPATH);
            sw.WriteLine("INSERT INTO schools (id, name, city_id) VALUES \n");

            for (int i = 0; i < schools.Count; i++)
            {
                if (i + 1 == schools.Count)
                {
                    sw.WriteLine($"({i + 1}, '{schools[i].Name}', {schools[i].CityId});");
                }
                else
                {
                    sw.WriteLine($"({i + 1}, '{schools[i].Name}', {schools[i].CityId}),");
                }
                LogPercentage(i + 1, schools.Count, "of the Schools are generated");
            }
            sw.WriteLine("GO");
            sw.WriteLine(GenerateNewLine());
        }

        public static void GenerateCodecoolers(int number)
        {
            FirstName FirstName = new FirstName();
            FirstName.LoadData();
            LastName LastName = new LastName();
            LastName.LoadData();
            string endchar;

            using StreamWriter sw = File.AppendText(WRITEPATH);

            for (int i = 0; i < number; i++)
            {
                if (Convert.ToInt32(number) % BATCHSIZE == 0)
                { endchar = ";\n GO \n"; }
                else { endchar = ";"; }

                var codecooler = Codecooler.GenerateRandom(FirstName, LastName);
                sw.WriteLine($"INSERT INTO codecoolers(id, last_name, first_name, birth_year, birth_city_id) VALUES" +
                $"({i + 1}, " +
                $"'{codecooler.LastName}', " +
                $"'{codecooler.FirstName}', " +
                $"{codecooler.BirthYear}, " +
                $"{codecooler.BirthCityId}){endchar}");
                LogPercentage(i + 1, number, "of the Codecoolers are generated");
            }
            sw.WriteLine("GO");
            sw.WriteLine(GenerateNewLine());
        }
        public static void GenerateCodecoolersSchools()
        {
            DbImportUtils Utils = new DbImportUtils();
            using StreamWriter sw = File.AppendText(WRITEPATH);
            var CodecoolerIds = Enumerable.Range(1, CODECOOLERNUM).ToList();
            var SchoolIds = Enumerable.Range(1, SCHOOLNUM).ToList();
            string endchar = ";";

            for (int i = 1; i < CODECOOLERNUM; i++)
            {
                var RandomSchoolIds = SchoolIds.OrderBy(x => Utils.RandomNumber(1, SCHOOLNUM)).Take(MAXNUMBEROFSCHOOLSPERCODECOOLER).ToList();
                foreach (var schoolId in RandomSchoolIds)
                {
                    if (Convert.ToInt32(CODECOOLERNUM) % BATCHSIZE == 0) { endchar = ";\n GO \n"; } else { endchar = ";"; }
                    sw.WriteLine($"INSERT INTO codecoolers_schools(codecooler_id, school_id) VALUES" +
                                            $"({i}, {schoolId}){endchar}");
                    LogPercentage(i, CODECOOLERNUM, "of the connections are generated");
                }
            }
            sw.WriteLine("GO");
            sw.WriteLine(GenerateNewLine());
        }

        public static void GenerateConstraints()
        {
            string path = CURDIR + "data/constraints.sql";
            var ConstraintsString = File.ReadAllText(path);
            using StreamWriter sw = File.AppendText(WRITEPATH);
            sw.WriteLine(ConstraintsString);
            sw.WriteLine(GenerateNewLine());

        }
        public static void SaveSql()
        {
            string path = CURDIR + DATABASEFILENAME;
            File.WriteAllText(path, MySqlString);
            MySqlString = "";
        }
        public static string GenerateNewLine()
        {
            return "\n";
        }
    }
}
