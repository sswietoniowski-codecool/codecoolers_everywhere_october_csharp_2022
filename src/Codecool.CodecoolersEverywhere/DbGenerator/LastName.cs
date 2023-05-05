using Codecool.CodeCoolersEverywhere.Helper;
using System.Collections.Generic;

namespace Codecool.CodeCoolersEverywhere.DbGenerator
{
    public class LastName
    {
        private static List<LastName> Data = new List<LastName>();
        public static string FILENAME = "last_names.txt";
        public string LastNameValue { get; set; }
        public void LoadData()
        {
            var FileData = FileResource.LoadFile(FILENAME);
            for (int i = 0; i < FileData.Length; i++)
            {
                var lineData = FileData[i].Split(",");
                Data.Add(new LastName()
                {
                    LastNameValue = lineData[0].Replace("'", "")
                });
            }
        }
        internal string GetRandom()
        {
            DbImportUtils Utils = new DbImportUtils();
            int maxdatalen = Data.Count;
            return Data[Utils.RandomNumber(0, maxdatalen)].LastNameValue;
        }
    }
}
