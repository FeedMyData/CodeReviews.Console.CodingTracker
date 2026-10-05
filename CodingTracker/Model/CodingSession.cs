// You'll need to create a CodingSession class in a separate file. 
// It will contain the properties of your coding session: Id, StartTime, EndTime, Duration. 
// When reading from the database, you can't use an anonymous object, you have to read your table into a List of CodingSession.

namespace CodingTracker;

class CodingSession
{
  internal int Id { get; private set; }
  internal string Activity { get; private set; }
  internal string StartTime { get; private set; }
  internal string EndTime { get; private set; }
  internal int Duration { get; private set; }

  internal CodingSession(int id, string activity, DateTime startTime, DateTime endtime, TimeSpan duration)
  {
    Id = id;
    Activity = activity;
    StartTime = Convert.ToString(startTime);
    EndTime = Convert.ToString(endtime);
    Duration = Convert.ToInt32(duration);
  }
}