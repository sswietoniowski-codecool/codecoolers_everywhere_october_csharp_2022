using Codecool.CodeCoolersEverywhere.Helper;
using System.Collections.Generic;


namespace Codecool.CodeCoolersEverywhere.DbGenerator
{
    public class FirstName
    {
        private static List<FirstName> Data = new List<FirstName>();
        public static string FILENAME = "first_names.txt";
        public string FirstNameValue { get; set; }
        public void LoadData()
        {
            var FileData = FileResource.LoadFile(FILENAME);
            for (int i = 0; i < FileData.Length; i++)
            {
                var lineData = FileData[i].Split(",");
                Data.Add(new FirstName()
                {
                    FirstNameValue = lineData[0].Replace("'", "")
                });
            }
        }

        internal string GetRandom()
        {
            DbImportUtils Utils = new DbImportUtils();
            int maxdatalen = Data.Count;
            return Data[Utils.RandomNumber(0, maxdatalen)].FirstNameValue;

        }
    }
}
