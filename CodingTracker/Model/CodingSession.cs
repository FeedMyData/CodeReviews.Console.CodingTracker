namespace CodingTracker;

class CodingSession
{
  internal int Id { get; private set; }
  internal string Activity { get; private set; }
  internal string StartTime { get; private set; }
  internal string EndTime { get; private set; }
  internal int Duration { get; private set; }

  internal CodingSession(int id, string activity, string startTime, string endtime, int duration)
  {
    Id = id;
    Activity = activity;
    StartTime = startTime;
    EndTime = endtime;
    Duration = duration;
  }
}