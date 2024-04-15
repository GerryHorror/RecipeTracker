using System;
using RecipeTracker.Classes;

namespace RecipeTracker
{
    // This is the entry point of the program (i.e., where the program starts).
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Create a new Recipe object to store the recipe details.
            var newRecipe = new Recipe();
            // Boolean to check if the user hasn't exited the program
            var appRunning = true;
            // Welcome message to the user
            Console.WriteLine("Welcome to Recipe Tracker!");
        }
    }
}