using Spectre.Console;

namespace CodingSession;

internal class ChoicesPrompt
{
  internal static string AskForActivity()
  {
    AnsiConsole.MarkupLine("[grey]Press 'Enter' to autofill with the last activity registered.[/]");
    var prompt = new TextPrompt<string>("[yellow]What were you working on?[/]").AllowEmpty();
    return AnsiConsole.Prompt(prompt);
  }
}