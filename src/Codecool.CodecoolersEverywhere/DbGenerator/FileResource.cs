using System;
using System.IO;

namespace Codecool.CodeCoolersEverywhere.DbGenerator
{
    public class FileResource
    {
        public static string[] LoadFile(string filename)
        {
            string curdir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            string path = curdir + "data/" + filename;
            string[] readText = File.ReadAllLines(path);
            return readText;
        }
    }
}
