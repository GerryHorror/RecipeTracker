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

            // While loop to keep the program running (i.e appRunning is true)
            while (appRunning)
            {
                // Console.Clear() to clear the console window before displaying the menu
                Console.Clear();
                Console.WriteLine("What would you like to do today?");
                Console.WriteLine(
                    "1. Add a new recipe\n2. Display a recipe\n3. Scale a recipe\n4. Reset quantities\n5. Clear a recipe\n6. Exit");
                string userChoice = Console.ReadLine();
                Console.ResetColor();
                // Switch statement to handle the user's choice
                switch (userChoice)
                {
                    // Case 1: Add a new recipe to the Recipe object
                    case "1":
                        RecipeOperations.AddRecipe(newRecipe);
                        break;
                    // Case 2: Display the recipe details stored in the Recipe object
                    case "2":
                        RecipeOperations.DisplayRecipe(newRecipe);
                        break;
                    // Case 3: Scale the recipe by a factor of 0.5, 2, or 3
                    case "3":
                        RecipeOperations.ScaleRecipe(newRecipe);
                        break;
                    // Case 4: Reset the quantities of all ingredients in the recipe
                    case "4":
                        newRecipe.ResetQuantities();
                        Console.WriteLine("Quantities reset successfully!");
                        break;
                    // Case 5: Clear the recipe details stored in the Recipe object
                    case "5":
                        newRecipe.ClearRecipe();
                        Console.WriteLine("Recipe cleared successfully!");
                        Console.ResetColor();
                        break;
                    // Case 6: Exit the program by setting appRunning to false
                    case "6":
                        Console.WriteLine("Thank you for using Recipe Tracker! Goodbye!");
                        appRunning = false;
                        break;
                    // Default case: Display an error message for invalid choices (i.e., choices other than 1-6)
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                        break;
                }
                // Console.WriteLine() to pause the program and wait for user input before displaying the menu again
                Console.WriteLine("Press any key to continue...");
                // Console.ReadKey() to read the user's input (any key) before displaying the menu again
                Console.ReadKey();
            }
        }
    }
}

// < -------------------------------------------END------------------------------------------- >