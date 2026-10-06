using Spectre.Console;
using System.Diagnostics;
using System.Globalization;

namespace CodingTracker;

internal class TimeUtilities
{
  internal static void CodingSession()
  {
    DateTime currentDate = DateTime.Now;
    Stopwatch stopwatch = Stopwatch.StartNew();

    ConsoleKeyInfo input = new();

    while (input.Key != ConsoleKey.Escape)
    {
      SessionStatut(currentDate, stopwatch);
      LiveTimer(stopwatch);

      if (Console.KeyAvailable)
      {
        input = Console.ReadKey();

        if (stopwatch.IsRunning && input.Key == ConsoleKey.Spacebar)
          stopwatch.Stop();

        else if (!stopwatch.IsRunning && input.Key == ConsoleKey.Spacebar)
          stopwatch.Start();
      }
    }
  }

  internal static void SessionStatut(DateTime date, Stopwatch stopwatch)
  {
    string colorHighlight;
    string state;
    string action;

    string dateDisplay = date.ToString("ddd d MMMM yyyy");

    if (stopwatch.IsRunning)
    {
      colorHighlight = "green";
      state = "active";
      action = "pause";
    }

    else
    {
      colorHighlight = "blue";
      state = "paused";
      action = "resume";
    }
    Console.Clear();
    UserInterface.PrintTitle();

    AnsiConsole.MarkupLine($"[{colorHighlight}]Coding session {state}.[/]");
    AnsiConsole.MarkupLine($"[Grey]Toggle Spacebar to {action} session.[/]");
    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine(dateDisplay);
    AnsiConsole.MarkupLine($"Elapsed time: ");
  }

  internal static void LiveTimer(Stopwatch stopwatch)
  {
    int clockRow = Console.CursorTop;

    bool cursorVisible = Console.CursorVisible;
    Console.CursorVisible = false;

    while (!Console.KeyAvailable)
    {
      string stopwatchDisplay = stopwatch.Elapsed.ToString(@"hh\:mm\:ss");
      Console.SetCursorPosition(0, clockRow);
      Console.Write(stopwatchDisplay);

      Thread.Sleep(100);
    }
    AnsiConsole.WriteLine("\n");

    Console.CursorVisible = cursorVisible;
  }

  // static void test()
  // {
  //   bool confirmStart = false;
  //   GetTimeFromUser("dd.MM.yy", "Start date");
  //   GetTimeFromUser("hh.mm", "Start hour").ToString(@"hh\:mm");
  //   GetTimeFromUser("dd.MM.yy", "End date");
  //   GetTimeFromUser("hh.mm", "End hour").ToString(@"hh\:mm");

  //   while (!confirmStart)
  //   {
  //     confirmStart = AnsiConsole.Confirm("Do you want to start a coding session?");
  //   }

  //   TimeSpan duration = LaunchSession();
  //   string displayDuration = duration.ToString(@"hh\:mm");
  //   AnsiConsole.MarkupLine($"Session duration: {displayDuration}");

  //   AnsiConsole.Confirm("quit app");
  // }

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
}