using System.Runtime.CompilerServices;
using Dapper;
using Microsoft.Data.Sqlite;

namespace CodingTracker;

internal class CodingTrackerRepository
{
  static string connectionString = @"Data Source=CodingTracker.db";

  internal static void CreateDB()
  {
    bool tableExisted;

    using (var connection = new SqliteConnection(connectionString))
    {
      connection.Open();

      using var existsCommand = connection.CreateCommand();
      existsCommand.CommandText =
        $@"SELECT EXISTS (
        SELECT name
        FROM sqlite_master
        WHERE type = 'table'
        AND name = 'coding_tracker')";

      tableExisted = Convert.ToBoolean(existsCommand.ExecuteScalar());

      var createCommand = connection.CreateCommand();
      createCommand.CommandText =
        $@"CREATE TABLE IF NOT EXISTS coding_tracker(
        {Column.id} INTEGER PRIMARY KEY AUTOINCREMENT,
        {Column.activity} TEXT,
        {Column.startTime} TEXT,
        {Column.endTime} TEXT,
        {Column.duration} INTEGER)";

      createCommand.ExecuteNonQuery();
    }

    if (!tableExisted)
      DebugTool.SeedData();
  }

  internal static void Add(string activity, string startTime, string endTime, int duration)
  {
    string sql = @$"INSERT INTO coding_tracker 
                  ({Column.activity}, {Column.startTime}, {Column.endTime}, {Column.duration})
                  VALUES (@activity, @startTime, @endTime, @duration)";

    using var connection = new SqliteConnection(connectionString);

    connection.Execute(sql, new
    {
      activity,
      startTime,
      endTime,
      duration
    });
  }
}
