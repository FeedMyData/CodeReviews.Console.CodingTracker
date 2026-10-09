// Add confirmation to AddSessionFlow
// Autofill feature (Query last activity)
// Create more Query for ViewAll
// How to change the title of the column table?
// Edit 
// Delete

// BONUS: SeedData increment activity rather than randomPick
// filter coding records per period & category

// Challenges
// Let the users filter their coding records per period (weeks, days, years) and/or order ascending or descending.
// If you already have a bit of experience with programming, we highly recommend you get into the habit of writing unit tests for a few methods in your project. Any method that outputs data and doesn't talk to a database (those are tested in integration tests) can be unit tested. A good example is any method that deals with validation and testing your data-retrieving methods with different filters. Here's a quick tutorial.
namespace CodingTracker;

class Program
{
  static void Main(string[] args)
  {
    Repository.CreateDB();
    Displayer.PrintTitle();
    UserInterface.MainMenu();
  }
}
