using Spectre.Console;
using System.Diagnostics;
using System.Globalization;

namespace CodingSession;

internal class TimeUtilities
{
  // Session with StopWatch & Automatic Entry
  private static TimeSpan LaunchSessionNow()
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