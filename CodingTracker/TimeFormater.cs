using System.Diagnostics;
using Spectre.Console;
using System.Globalization;

namespace CodingTracker;

internal class TimeFormatter
{
  internal static string DisplayDate(DateTime date)
  {
    return date.ToString("ddd d MMMM yyyy - hh:mm");
  }

  internal static string DisplayDuration(Stopwatch stopwatch)
  {
    return stopwatch.Elapsed.ToString(@"hh\:mm\:ss");
  }

  internal static int MinutesToInt(TimeSpan t)
  {
    return Convert.ToInt32(t.TotalMinutes);
  }

  internal static DateTime GetTimeFromUser(string expectedFormat, string topic)
  {
    //   examples for reference
    //   GetTimeFromUser("dd.MM.yy", "Start date");
    //   GetTimeFromUser("hh.mm", "Start hour").ToString(@"hh\:mm");
    //   GetTimeFromUser("dd.MM.yy", "End date");
    //   GetTimeFromUser("hh.mm", "End hour").ToString(@"hh\:mm");

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