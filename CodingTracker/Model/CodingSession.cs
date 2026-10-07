namespace CodingTracker;

class CodingSession
{
  internal int Id { get; private set; }
  internal string Activity { get; private set; }
  internal string StartTime { get; private set; }
  internal string EndTime { get; private set; }
  internal int Duration { get; private set; }

  internal CodingSession(Int64 id, string activity, string startTime, string endTime, Int64 duration)
  {
    Id = (int)id;
    Activity = activity;
    StartTime = startTime;
    EndTime = endTime;
    Duration = (int)duration;
  }
}