using System.Diagnostics;
using Spectre.Console;
using System.Globalization;

namespace CodingTracker;

internal class TimeFormatter
{
  public const string DaTeStyleShort = @"yy/MM/dd";
  public const string DaTeStyleDB = @"yyyy/MM/dd hh:mm";
  public const string HourStyleShort = @"hh.mm";
  public const string DateStyleFull = "ddd dd MMM yy - hh:mm";
  public const string DateStyleHour = "hh:mm";
  public const string StopWatchStyle = @"hh/mm/ss";

  internal static string DisplayDate(DateTime date)
  {
    return date.ToString(DateStyleFull);
  }

  internal static string DisplayDuration(Stopwatch stopwatch)
  {
    return stopwatch.Elapsed.ToString(StopWatchStyle);
  }

  internal static string ConvertDateForDb(DateTime date)
  {
    return date.ToString(DaTeStyleDB);
  }

  internal static string ConvertDateOutDb(string dateString)
  {
    return DateTime.Parse(dateString).ToString(DateStyleFull);
  }

  internal static string ConvertHourOutDb(string dateString)
  {
    return DateTime.Parse(dateString).ToString(DateStyleHour);
  }

  internal static string ConvertDurationOutDb(int duration)
  {
    int hours = duration / 60;
    int minutes = duration % 60;

    return $"{hours:D2}:{minutes:D2}";
  }

  internal static int MinutesToInt(TimeSpan t)
  {
    return Convert.ToInt32(t.TotalMinutes);
  }

  // internal static string DisplayDurationHM(int duration)
  // {
  //   DateTime time = new(2000, 1, 1, 0, 0, 0, 0, 0, 0);
  //   time.AddMinutes(duration);
  //   return time.ToString(@"hh\:mm");
  // }

  // internal static ParseIntToHHMM(int duration)
  // {

  // }



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