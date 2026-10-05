// You'll need to create a CodingSession class in a separate file. 
// It will contain the properties of your coding session: Id, StartTime, EndTime, Duration. 
// When reading from the database, you can't use an anonymous object, you have to read your table into a List of CodingSession.

namespace CodingTracker;

class CodingSession
{
  internal int Id { get; private set; }
  internal string StartTime { get; private set; }
  internal string EndTime { get; private set; }
  internal string Duration { get; private set; }

  internal CodingSession(DateTime dateTime, DateTime endtime, TimeSpan duration)
  {
    Id =
  }
}