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
          EditItem();
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
    string activity = ChoicesPrompt.AskForActivity("Add", colorHighlight);

    string[] timeInfos = FormatTimeInfo(startDate, startHour, endHour);

    Repository.Add(activity, timeInfos[0], timeInfos[1], Convert.ToInt32(timeInfos[2]));

    AnsiConsole.WriteLine();
    Displayer.PreviewRow(activity, timeInfos[0], timeInfos[1], Convert.ToInt32(timeInfos[2]));
    AnsiConsole.WriteLine();
    AnsiConsole.MarkupLine($"[{colorHighlight}]The new session has been added to the database.[/]");
    AnsiConsole.WriteLine();
  }

  internal static void ViewAll()
  {
    List<CodingSession> tableData = Query.ViewAll();
    Displayer.PrintTable(tableData, "[bold] all Sessions[/]");
  }

  internal static void EditItem()
  {
    Displayer.PrintTitle();

    if (Query.CountRows() == 0)
      Displayer.NoEntriesFound();

    else
    {
      string action = "Edit";
      string colorHighlight = "Blue";

      string[] timeInfos;

      List<CodingSession> tableData = ChoicesPrompt.AskForFilter(action, colorHighlight);
      AnsiConsole.WriteLine();
      CodingSession selection = ChoicesPrompt.SelectEntry(tableData, action, colorHighlight);

      AnsiConsole.MarkupLine($"[{colorHighlight}]Selection (id {selection.Id})[/]");
      Displayer.PreviewRow(selection);
      AnsiConsole.WriteLine();

      string[] editingChoices = EnumExtensions.GetEditableColumn().ToArray();
      List<string> toEdit = ChoicesPrompt.SelectMultiString(action, colorHighlight, editingChoices);

      if (toEdit.Contains($"{Column.activity}"))
        selection.EditActivity(ChoicesPrompt.AskForActivity(action, colorHighlight));

      if (toEdit.Contains($"{Column.startTime}") && toEdit.Contains($"{Column.endTime}"))
      {
        string startDate = GetTimeFromUser(DaTeStyleShort, $"[{colorHighlight}]{action}[/] start date").ToString(DaTeStyleShort);
        string startHour = GetTimeFromUser(DateStyleHour, $"[{colorHighlight}]{action}[/] start hour").ToString(DateStyleHour);
        string endHour = GetTimeFromUser(DateStyleHour, $"[{colorHighlight}]{action}[/] end hour").ToString(DateStyleHour);
        timeInfos = FormatTimeInfo(startDate, startHour, endHour);

        selection.EditStartTime(timeInfos[0]);
        selection.EditEndTime(timeInfos[1]);
        selection.EditDuration(Convert.ToInt32(timeInfos[2]));
      }

      else if (toEdit.Contains($"{Column.startTime}"))
      {
        string startDate = GetTimeFromUser(DaTeStyleShort, $"[{colorHighlight}]{action}[/] start date").ToString(DaTeStyleShort);
        string startHour = GetTimeFromUser(DateStyleHour, $"[{colorHighlight}]{action}[/] start hour").ToString(DateStyleHour);
        string endHour = selection.EndTime;
        timeInfos = FormatTimeInfo(startDate, startHour, endHour);

        selection.EditStartTime(timeInfos[0]);
        selection.EditDuration(Convert.ToInt32(timeInfos[2]));
      }

      else if (toEdit.Contains($"{Column.endTime}"))
      {
        string startDate = TrimHour(selection.StartTime);
        string startHour = TrimDate(selection.StartTime);
        string endHour = GetTimeFromUser(DateStyleHour, $"[{colorHighlight}]{action}[/] end hour").ToString(DateStyleHour);
        timeInfos = FormatTimeInfo(startDate, startHour, endHour);

        selection.EditEndTime(timeInfos[1]);
        selection.EditDuration(Convert.ToInt32(timeInfos[2]));
      }

      Repository.UpdateRow(selection);

      AnsiConsole.WriteLine();
      AnsiConsole.MarkupLine("[green]Entry succesfully edited.[/]");
      Displayer.PreviewRow(selection);
      AnsiConsole.WriteLine();
    }
  }
}