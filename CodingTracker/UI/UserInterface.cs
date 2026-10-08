using CodingSession;
using Spectre.Console;
using static CodingTracker.Enums;
using static CodingTracker.TimeFormatter;

namespace CodingTracker;

internal class UserInterface
{
  internal static void MainMenu()
  {
    bool exitApp = false;

    while (!exitApp)
    {
      var selection = AnsiConsole.Prompt(
          new SelectionPrompt<MenuChoice>()
          .Title("What do you want to do?")
          .HighlightStyle(Color.Grey)
          .UseConverter(choice => choice.GetDisplayName())
          .AddChoices(Enum.GetValues<MenuChoice>()));

      switch (selection)
      {
        case MenuChoice.addItemStartNow:
          LiveSession.Launch();
          break;

        case MenuChoice.viewItems:
          ViewAll();
          break;

        case MenuChoice.addItem:
          AddItem();
          break;

        case MenuChoice.editItem:
          //EditItem();
          break;

        case MenuChoice.deleteItem:
          //DeleteItem();
          break;

        case MenuChoice.exit:
          exitApp = true;
          break;
      }
    }
  }

  internal static void AddItem()
  {
    string colorHighlight = "green";

    AnsiConsole.MarkupLine($"[{colorHighlight}]Add Session[/]");

    string startDate = GetTimeFromUser(DaTeStyleShort, "Day").ToString(DaTeStyleShort);
    string startHour = GetTimeFromUser(DateStyleHour, "start hour").ToString(DateStyleHour);
    string endHour = GetTimeFromUser(DateStyleHour, "end hour").ToString(DateStyleHour);
    string activity = ChoicesPrompt.AskForActivity();

    string[] timeInfos = FormatTimeInfo(startDate, startHour, endHour);

    AnsiConsole.WriteLine();
    Displayer.PreviewNewRow(activity, timeInfos[0], timeInfos[1], Convert.ToInt32(timeInfos[2]));
    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine($"[{colorHighlight}]The new session has been added to the database.[/]");
    AnsiConsole.WriteLine();
  }

  internal static void ViewAll()
  {
    List<CodingSession> tableData = Query.ViewAll();
    Displayer.PrintTable(tableData, "[bold] all Sessions[/]");
  }
}