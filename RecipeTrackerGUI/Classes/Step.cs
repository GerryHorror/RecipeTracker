/*
 References:
    -   [Measurement Conversions for Recipes](https://www.thespruceeats.com/recipe-conversions-486768)
    -   [Food Groups](https://sweetlife.org.za/what-are-the-different-food-groups-a-simple-explanation/)
    -   [Calorie Intake Chart](https://www.webmd.com/diet/calories-chart)
    -   [Definitive Guide to WPF Colors] (https://www.codeproject.com/Articles/5296124/Definitive-guide-to-WPF-colors-color-spaces-color)
    -   [Binding declarations overview (WPF .NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/binding-declarations-overview?view=netdesktop-8.0)
    -   [Value Converters In WPF] (https://www.c-sharpcorner.com/article/value-converters-in-wpf/#:~:text=Value%20converters%20are%20used%20to%20display%20values%20in,and%20values%20that%20derive%20from%20multiple%20bound%20elements.)
    -   [Controls in WPF] (https://learn.microsoft.com/en-us/dotnet/desktop/wpf/controls/?view=netframeworkdesktop-4.8&viewFallbackFrom=netdesktop-6.0)
    -   [How to open a message box (WPF .NET)] (https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/how-to-open-message-box?view=netdesktop-6.0)
    -   [How to create a template for a control (WPF.NET)] (https://learn.microsoft.com/en-us/dotnet/desktop/wpf/controls/how-to-create-apply-template?view=netdesktop-6.0)
    -   [Data binding overview (WPF .NET)] (https://learn.microsoft.com/en-us/dotnet/desktop/wpf/data/?view=netdesktop-6.0)
    -   [XAML overview (WPF .NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/xaml/?view=netdesktop-6.0)
    -   [Overview of WPF windows (WPF .NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/windows/?view=netdesktop-6.0)
    -   [Routed events overview (WPF .NET)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/events/routed-events-overview?view=netdesktop-6.0)
    -   [Attached events overview (WPF .NET](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/events/attached-events-overview?view=netdesktop-6.0)
 */

using System.ComponentModel;

/// <summary>
///   Gérard Blankenberg
///   ST10046280
///   Module: PROG6221
///   POE Final Submission
/// </summary>

/*
    This class is used to define the properties of a step in a recipe.
    The properties include the description of the step and whether the step is completed.
    The step class is used in the Recipe class to define the steps of a recipe.
*/

namespace RecipeTrackerGUI.Classes
{
    // Step class that defines the properties of a step in a recipe and implements the INotifyPropertyChanged interface (used for data binding)
    public class Step : INotifyPropertyChanged
    {
        // Private field to store the completion status of the step
        private bool _isCompleted;

        // Public property to get and set the description of the step
        public string Description { get; set; }

        // Public property to get and set the completion status of the step
        public bool IsCompleted
        {
            get { return _isCompleted; }
            set
            {
                if (_isCompleted != value)
                {
                    _isCompleted = value;
                    OnPropertyChanged(nameof(IsCompleted));
                }
            }
        }

        // <-------------------------------------------------------------------------------------->

        // Event handler for property changed events (used for data binding)
        public event PropertyChangedEventHandler PropertyChanged;

        // Method to raise the PropertyChanged event when a property value changes (used for data binding)
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

// < -------------------------------------------END------------------------------------------- >