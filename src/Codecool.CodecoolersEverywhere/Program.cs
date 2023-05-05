﻿
namespace Codecool.CodeCoolersEverywhere
{
    /// <summary>
    /// This is the main class of your program which contains Main method
    /// </summary>
    public class Program
    {
        /// <summary>
        /// This is the entry point of your program.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            DbGenerator.DbGenerator.GenerateSql();
        }

    }
}
