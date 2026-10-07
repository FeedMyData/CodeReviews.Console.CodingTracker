using Spectre.Console;
using System.Diagnostics;
using static CodingTracker.TimeFormatter;

namespace CodingTracker;

internal class LiveSession
{
  internal static void Launch()
  {
    DateTime startDate = DateTime.Now;
    Stopwatch stopwatch = Stopwatch.StartNew();

    ConsoleKeyInfo input = new();

    while (input.Key != ConsoleKey.Enter)
    {
      SessionActiveStatus(startDate, stopwatch);
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

    DateTime endDate = DateTime.Now;

    SessionOverStatus(startDate, stopwatch);

    AnsiConsole.MarkupLine("[grey]Press 'Enter' to autofill with the last activity registered.[/]");
    var prompt = new TextPrompt<string>("[yellow]What were you working on?[/]").AllowEmpty();
    string activity = AnsiConsole.Prompt(prompt);

    //add autoFill feature, query the DB for Last activity, catch empty DB

    Repository.Add(
            activity.ToString(),
            DisplayDate(startDate),
            DisplayDate(endDate),
            MinutesToInt(stopwatch.Elapsed));

    AnsiConsole.MarkupLine("[green]The session has been added to the DB[/]");
    //Add a query that display the last DB entry.

    AnsiConsole.WriteLine();
  }

  private static void SessionActiveStatus(DateTime date, Stopwatch stopwatch)
  {
    string colorHighlight;
    string state;
    string action;

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

    AnsiConsole.MarkupLine($"Coding session [{colorHighlight}]{state}.[/]");
    AnsiConsole.MarkupLine($"[Grey]Toggle 'Spacebar' to {action} session, 'Enter' to terminate.[/]");
    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine(DisplayDate(date));
  }

  private static void SessionOverStatus(DateTime date, Stopwatch stopwatch)
  {
    Console.Clear();
    UserInterface.PrintTitle();

    AnsiConsole.MarkupLine($"Coding session [yellow]done.[/]");
    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine(DisplayDate(date));
    AnsiConsole.MarkupLine(DisplayDuration(stopwatch));
  }

  private static void LiveTimer(Stopwatch stopwatch)
  {
    int clockRow = Console.CursorTop;

    bool cursorVisible = Console.CursorVisible;
    Console.CursorVisible = false;

    while (!Console.KeyAvailable)
    {
      Console.SetCursorPosition(0, clockRow);
      Console.Write(DisplayDuration(stopwatch));

      Thread.Sleep(100);
    }
    AnsiConsole.WriteLine("\n");

    Console.CursorVisible = cursorVisible;
  }
}