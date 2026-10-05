using Dapper;
using Microsoft.Data.Sqlite;
using static CodingTracker.Enums;

namespace CodingTracker;

internal class Repository
{
  static string connectionString = @"Data Source=CodingTracker.db";

  internal static void CreateDB()
  {
    bool tableExisted;

    using (var connection = new SqliteConnection(connectionString))
    {
      const string existsSql =
        $@"SELECT EXISTS (
        SELECT name
        FROM sqlite_master
        WHERE type = 'table'
        AND name = 'coding_tracker')";

      tableExisted = connection.ExecuteScalar<bool>(existsSql);

      string createSql =
        $@"CREATE TABLE IF NOT EXISTS coding_tracker(
        {Column.id} INTEGER PRIMARY KEY AUTOINCREMENT,
        {Column.activity} TEXT,
        {Column.startTime} TEXT,
        {Column.endTime} TEXT,
        {Column.duration} INTEGER)";

      connection.Execute(createSql);
    }

    // if (!tableExisted)
    // DebugTool.SeedData();
  }

  internal static void Add(string activity, string startTime, string endTime, int duration)
  {
    string sql = @$"INSERT INTO coding_tracker 
                  ({Column.activity}, {Column.startTime}, {Column.endTime}, {Column.duration})
                  VALUES (@activity, @startTime, @endTime, @duration)";

    using var connection = new SqliteConnection(connectionString);

    connection.Execute(sql, new
    {
      activity = activity,
      startTime = startTime,
      endTime = endTime,
      duration = duration
    });
  }

  internal static void Delete(CodingSession entry)
  {
    string sql = @$"DELETE FROM coding_tracker WHERE {Column.id} = @id";

    using var connection = new SqliteConnection(connectionString);

    connection.Execute(sql, new { id = entry.Id });
  }

  internal static void Update(CodingSession entry)
  {
    string sql = @$"UPDATE coding_tracker 
                    SET activity = @activity, 
                        startTime = @startTime, 
                        endTime = @endTime,
                        duration = @duration 
                    WHERE id = @id";

    using var connection = new SqliteConnection(connectionString);

    connection.Execute(sql, new
    {
      activity = entry.Activity,
      startTime = entry.StartTime,
      endTime = entry.EndTime,
      duration = entry.Duration,
      id = entry.Id
    });
  }
}
