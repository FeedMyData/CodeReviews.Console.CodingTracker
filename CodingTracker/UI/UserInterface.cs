using Spectre.Console;
using static CodingTracker.Enums;

namespace CodingSession;

internal class UserInterface
{
  internal static void MainMenu()
  {
    bool exitApp = false;

    while (!exitApp)
    {
      var selection = AnsiConsole.Prompt(
          new SelectionPrompt<MenuItem>()
          .Title("What do you want to do?")
          .AddChoices(Enum.GetValues<MenuItem>()));

      switch (selection)
      {
        case MenuItem.viewItems:
          //ViewItems();
          break;

        case MenuItem.addItem:
          //AddItem();
          break;

        case MenuItem.deleteItem:
          //DeleteItem();
          break;

        case MenuItem.editItem:
          //EditItem();
          break;

        case MenuItem.exit:
          exitApp = true;
          break;
      }
    }
  }
}