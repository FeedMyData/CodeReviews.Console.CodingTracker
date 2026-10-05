// This application has the same requirements as the previous project, except that now you'll be logging your daily coding time.
// You're required to have separate classes in different files (i.e. UserInput.cs, Validation.cs, CodingController.cs)
// ** You should tell the user the specific format you want the date and time to be logged and not allow any other format.
// You'll need to create a configuration file called appsettings.json, which will contain your database path and connection strings (and any other configs you might need).
// You'll need to create a CodingSession class in a separate file. It will contain the properties of your coding session: 
// Id, StartTime, EndTime, Duration. When reading from the database, you can't use an anonymous object, you have to read your table into a List of CodingSession.
// ** The user shouldn't input the duration of the session. It should be calculated based on the Start and End times
// The user should be able to input the start and end times manually.

// Challenges
// Add the possibility of tracking the coding time via a stopwatch so the user can track the session as it happens.
// Let the users filter their coding records per period (weeks, days, years) and/or order ascending or descending.
// If you already have a bit of experience with programming, we highly recommend you get into the habit of writing unit tests for a few methods in your project. Any method that outputs data and doesn't talk to a database (those are tested in integration tests) can be unit tested. A good example is any method that deals with validation and testing your data-retrieving methods with different filters. Here's a quick tutorial.

using Spectre.Console;
using Microsoft.Data.Sqlite;
using System.Diagnostics;
using System.Globalization;

class Program
{
  static void Main(string[] args)
  {
    bool confirmStart = false;
    GetTimeFromUser("dd.MM.yy", "Start date");
    GetTimeFromUser("hh.mm", "Start hour").ToString(@"hh\:mm");
    GetTimeFromUser("dd.MM.yy", "End date");
    GetTimeFromUser("hh.mm", "End hour").ToString(@"hh\:mm");

    while (!confirmStart)
    {
      confirmStart = AnsiConsole.Confirm("Do you want to start a coding session?");
    }

    TimeSpan duration = LaunchSession();
    string displayDuration = duration.ToString(@"hh\:mm");
    AnsiConsole.MarkupLine($"Session duration: {displayDuration}");

    AnsiConsole.Confirm("quit app");
  }

  // Get Date info from User
  private static DateTime GetTimeFromUser(string expectedFormat, string topic)
  {
    string errorMessage = $"[red]Format mismatch. Use {expectedFormat}[/]";

    string input = AnsiConsole.Prompt(
    new TextPrompt<string>($"{topic} ({expectedFormat}): ")
        .ValidationErrorMessage(errorMessage)
        .Validate(value =>
        {
          return DateTime.TryParseExact(
            value,
            expectedFormat,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out _)
            ? ValidationResult.Success()
            : ValidationResult.Error(errorMessage);
        }));

    return DateTime.ParseExact(input, expectedFormat, CultureInfo.InvariantCulture);

  }

  // Session with StopWatch & Automatic Entry
  private static TimeSpan LaunchSession()
  {
    DateTime startSession = DateTime.Now;
    Stopwatch stopwatch = Stopwatch.StartNew();

    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine($"[green]Coding session started.[/]");
    AnsiConsole.MarkupLine($"[green]{startSession}[/]");
    AnsiConsole.MarkupLine("[Grey]Toggle Spacebar to pause.[/]");
    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine($"Elapsed time: ");

    int clockRow = Console.CursorTop;

    bool cursorVisible = Console.CursorVisible;
    Console.CursorVisible = false;

    while (true)
    {
      Console.SetCursorPosition(0, clockRow);
      Console.Write(stopwatch.Elapsed.ToString(@"hh\:mm\:ss"));

      if (Console.KeyAvailable && Console.ReadKey(intercept: true).Key == ConsoleKey.Spacebar)
        break;

      Thread.Sleep(100);
    }

    AnsiConsole.WriteLine("\n");

    Console.CursorVisible = cursorVisible;

    return stopwatch.Elapsed;
  }
}


