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
          //AddItem();
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
    //var date = AnsiConsole.Ask<string>(""):
  }

  internal static void ViewAll()
  {
    List<CodingSession> tableData = Query.ViewAll();
    GenerateTable(tableData, "[bold] all Sessions[/]");
  }

  internal static void GenerateTable(List<CodingSession> tableData, string title)
  {
    var table = new Table()
            .RoundedBorder()
            .Title(title);

    foreach (string column in Enum.GetNames<Column>())
      table.AddColumn(column);

    foreach (var entry in tableData)
      table.AddRow(
        Markup.Escape(entry.Id.ToString()),
        Markup.Escape(entry.Activity),
        Markup.Escape(ConvertDateOutDb(entry.StartTime)),
        Markup.Escape(ConvertHourOutDb(entry.EndTime)),
        Markup.Escape(ConvertDurationOutDb(entry.Duration)));

    AnsiConsole.Write(table);
  }

  internal static void PrintTitle()
  {
    Console.Clear();
    AnsiConsole.Write(new Panel("   Coding Tracker   ")
      .AsciiBorder());
    AnsiConsole.WriteLine();
  }
}