using System.Diagnostics;
using Spectre.Console;
using System.Globalization;

namespace CodingTracker;

internal class TimeFormatter
{
  public const string DaTeStyleShort = @"yyyy/MM/dd";
  public const string DaTeStyleDB = @"yyyy/MM/dd HH:mm";
  public const string HourStyleShort = @"HH.mm";
  public const string DateStyleFull = "ddd dd MMM yy - HH:mm";
  public const string DateStyleHour = "HH:mm";
  public const string StopWatchStyle = @"hh\:mm\:ss";

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
    return date.ToString(DaTeStyleDB, CultureInfo.InvariantCulture);
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

  internal static string[] FormatTimeInfo(string startDate, string startHour, string endHour)
  {
    string endDate = startDate + " " + endHour;
    startDate = startDate + " " + startHour;

    DateTime eh = DateTime.Parse(endHour);
    DateTime sh = DateTime.Parse(startHour);
    TimeSpan duration;

    if (eh < sh) // (add 1 day to the date)
    {
      duration = TimeSpan.FromHours(24) - (eh - sh);
      endDate = (DateTime.Parse(endDate) + TimeSpan.FromDays(1)).ToString(DaTeStyleDB);
    }
    else
      duration = eh - sh;

    string durationString = Convert.ToString(MinutesToInt(duration));

    return [startDate, endDate, durationString];
  }

  internal static DateTime GetTimeFromUser(string expectedFormat, string topic)
  {
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

  internal static DateTime GetTimeFromUser(string expectedFormat, string topic, string hour)
  {
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

    string day = $"{input} {hour}";
    return DateTime.ParseExact(day, DaTeStyleDB, CultureInfo.InvariantCulture);
  }
}