using Spectre.Console;
using static CodingTracker.Enums;

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
          TimeUtilities.CodingSession();
          break;

        case MenuChoice.viewItems:
          //ViewItems();
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

  internal static void PrintTitle()
  {
    Console.Clear();
    AnsiConsole.Write(new Panel("   Coding Tracker   ")
      .AsciiBorder());
    AnsiConsole.WriteLine();
  }
}