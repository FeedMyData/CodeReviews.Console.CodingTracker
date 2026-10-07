using System.Reflection.Metadata;
using CodingTracker;

namespace CodingSession;

internal class DebugTool
{
  internal static void SeedData()
  {
    string[] activities = ["Coding Tracker", "Habit Logger", "Website"];
    DateTime randomStartDate = RandomDateTime();
    Random rand = new();

    for (int i = 0; i < 100; i++)
    {
      int addDay = rand.Next(0, 4);
      int addMinutes = rand.Next(0, 580);
      randomStartDate = randomStartDate.AddDays(addDay).AddMinutes(addMinutes);

      string activity = activities[rand.Next(0, activities.Length)];

      int duration = rand.Next(5, 380);
      DateTime endDateResult = randomStartDate.AddMinutes(duration);

      string startdate = TimeFormatter.DisplayDate(randomStartDate);
      string endDate = TimeFormatter.DisplayDate(endDateResult);

      Repository.Add(activity, startdate, endDate, duration);
    }
  }

  private static DateTime RandomDateTime()
  {
    Random rand = new();

    DateTime startDay = new DateTime(2026, 8, 1);
    int maxRange = (DateTime.Today - startDay).Days + 1;
    DateTime date = startDay.AddDays(rand.Next(0, maxRange));

    return date;
  }
}