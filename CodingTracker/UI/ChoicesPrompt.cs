using System.Runtime.CompilerServices;
using Spectre.Console;
using static CodingTracker.TimeFormatter;

namespace CodingTracker;

internal class ChoicesPrompt
{
  internal static string AskForActivity()
  {
    AnsiConsole.MarkupLine("[grey]Press 'Enter' to autofill with the last activity registered.[/]");
    var prompt = new TextPrompt<string>("[yellow]Activity?[/]").AllowEmpty();
    return AnsiConsole.Prompt(prompt);
  }

  public static List<CodingSession> AskForFilter(string action, string colorHighlight)
  {
    List<CodingSession> tableData = new();
    string[] choices = [
      $"{action} from all",
      $"{action} from sepecific {Enums.Column.activity.GetDisplayName()}",
      $"{action} from sepecific Time Range"];

    var selection = AnsiConsole.Prompt(
      new SelectionPrompt<string>()
      .Title($"What do you want to [BOLD {colorHighlight}]{action}[/]")
      .HighlightStyle(colorHighlight)
      .PageSize(100)
      .Mode(SelectionMode.Leaf)
      .AddChoices(choices));

    if (selection == choices[0])
      tableData = Query.ViewAll();
    else if (selection == choices[1])
    {
      string startDate = GetTimeFromUser(DaTeStyleShort, "Day", "00:00").ToString();
      string endDate = GetTimeFromUser(DaTeStyleShort, "Day", "23:59").ToString();
      tableData = Query.FilterByDate(startDate, endDate);
    }
    else
    {
      string activity = AskForActivity();
      tableData = Query.FilterByActivity(activity);
    }

    return tableData;
  }

  public static CodingSession SelectEntry(List<CodingSession> tableData, string action, string colorHighlight)
  {
    var selection = AnsiConsole.Prompt(
    new SelectionPrompt<CodingSession>()
    .Title($"[BOLD {colorHighlight}]Select[/] the entry you want to [BOLD {colorHighlight}]{action}[/]:")
    .HighlightStyle(colorHighlight)
    .UseConverter(entry => entry.DisplayData())
    .PageSize(100)
    .AddChoices(tableData));

    return selection;
  }
}